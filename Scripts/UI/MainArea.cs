using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainArea : MonoBehaviour
{
    [SerializeField] private List<UnitParameters> _defenderUnitParameters;
    [SerializeField] private List<UnitParameters> _attackerUnitParameters;
    [SerializeField] private List<UnitParameters> _bossesUnitParameters;
    [SerializeField] private List<UnitPlace> _spawnUnitPlaces;

    [SerializeField] private List<Unit> _allUnitsNow;
    [SerializeField] private List<Unit> _defenderUnitsNow;
    [SerializeField] private List<Unit> _attackerUnitsNow;
    [Space]
    [Header("Волны")]
    [SerializeField] private WaveState _waveState;
    [SerializeField] private int _currentWave;
    [SerializeField] private bool _isBossWave;
    [SerializeField] private int _bossesCompleteAmount;
    [SerializeField] private TMP_Text _currentWaveTMP_Text;
    [SerializeField] private int _basicEnemyInWaveValue;
    [SerializeField] private int _currentEnemyInWaveValue;
    [SerializeField] private int _leftEnemyInWaveValue;
    [SerializeField] private float _timePassedInWaveAsSeconds;
    [SerializeField] private DateTime _startTime;
    [SerializeField] private DateTime _endTime;
    [SerializeField] private float _waveWaitTime;
    [SerializeField] private TMP_Text _waveWaitTimeValueTMP_Text;
    [Space]
    [SerializeField] private float _basicSpawnDelayValue;
    [SerializeField] private float _currentSpawnDelayValue;
    [SerializeField] private Button _startWaveButton;

    private enum WaveState
    {
        Waiting,
        Prepare,
        Run,
        Ending
    }

    public static MainArea Instance;

    public List<Unit> AllUnitsNow { get => _allUnitsNow; }
    public List<Unit> DefenderUnitsNow { get => _defenderUnitsNow; }
    public List<Unit> AttackerUnitsNow { get => _attackerUnitsNow; }
    public int LeftEnemyInWaveValue { get => _leftEnemyInWaveValue; }
    public List<UnitParameters> DefenderUnitParameters { get => _defenderUnitParameters; }
    public List<UnitParameters> AttackerUnitParameters { get => _attackerUnitParameters; }
    public int CurrentWave { get => _currentWave; }

    private void Awake()
    {
        Instance = this;

        _currentEnemyInWaveValue = _basicEnemyInWaveValue + (_currentWave * 5);
        _currentSpawnDelayValue = _basicSpawnDelayValue;
        _leftEnemyInWaveValue = _currentEnemyInWaveValue;

        _startWaveButton.onClick.AddListener(StartWave);
    }
    private void Start()
    {
        ResetUnitParametersToDefault();
    }

    public void ResetUnitParametersToDefault()
    {
        List<UnitParameters> allUnitParameters = new List<UnitParameters>();

        allUnitParameters.AddRange(_defenderUnitParameters);
        allUnitParameters.AddRange(_attackerUnitParameters);

        foreach (UnitParameters unitParameter in allUnitParameters)
        {
            unitParameter.ResetToDefault();
        }
    }

    public void StartWave()
    {
        if (_waveState == WaveState.Prepare)
        {
            StopAllCoroutines();

            TextAnimation.IntNumberAnimation(_waveWaitTimeValueTMP_Text, _waveWaitTime, 0, 0);

            _waveState = WaveState.Run;

            StartCoroutine(WaveCoroutine());
        }

        if (_waveState == WaveState.Waiting)
        {
            StartCoroutine(WaveBetweenDelayCoroutine());
        }
    }

    private IEnumerator WaveCoroutine()
    {
        if (_currentWave % 10 == 0)
        {
            _isBossWave = true;
        }
        else
        {
            _isBossWave = false;
        }

        _startWaveButton.interactable = false;

        if (!_isBossWave)
        {
            if (_leftEnemyInWaveValue > 0)
            {
                SimpleUnitPlacesMonitoring simpleUnitPlacesMonitoring = SimpleUnitPlacesMonitoring.Instance;

                if (simpleUnitPlacesMonitoring.UnBusySimpleUnitPlaces.Count > 0)
                {
                    yield return new WaitForSeconds(_currentSpawnDelayValue);

                    _startTime = DateTime.Now;

                    UnitPlace randomUnitPlace = _spawnUnitPlaces[UnityEngine.Random.Range(0, _spawnUnitPlaces.Count)];

                    randomUnitPlace.InstantiateRandomUnit(Unit.UnitSideType.Attacker);

                    _leftEnemyInWaveValue -= 1;

                    StartCoroutine(WaveCoroutine());
                }
            }

            if (_currentWave > 1)
            {
                foreach (Unit unit in _attackerUnitsNow)
                {
                    unit.UnitParameters.ChangeAttackDamage(1.5f);
                    unit.UnitParameters.ChangeMoveSpeed(1.5f);
                    unit.UnitParameters.ChangeHPMax(1.5f);
                    unit.UnitParameters.ChangeAttackSpeed(1.5f);

                    unit.SetupUnitParameters(unit.UnitParameters);
                }
            }
        }
    }


    public void EndWave(bool isWaveWin)
    {
        _waveState = WaveState.Ending;

        if (isWaveWin)
        {
            _currentWave += 1;

            _currentWaveTMP_Text.text = _currentWave.ToString();
            _currentEnemyInWaveValue = _basicEnemyInWaveValue += 1;
            _currentSpawnDelayValue = _basicSpawnDelayValue;

            _leftEnemyInWaveValue = _currentEnemyInWaveValue;
        }
        else
        {
            Windows.Instance.OpenLoseWindow();
        }

        _endTime = DateTime.Now;

        _timePassedInWaveAsSeconds = (float)(_endTime - _startTime).TotalSeconds;

        SaveData.SetCurrentWaveTime(_timePassedInWaveAsSeconds);

        if (SaveData.GetBestWaveTime() < SaveData.GetCurrentWaveTime())
        {
            SaveData.SetBestWaveTime(_timePassedInWaveAsSeconds);
        }
        _startWaveButton.interactable = true;

        StartCoroutine(WaveBetweenDelayCoroutine());
    }

    public IEnumerator WaveBetweenDelayCoroutine()
    {
        _startWaveButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "ПРОПУСТИТЬ ОЖИДАНИЕ";

        _waveState = WaveState.Prepare;

        TextAnimation.IntNumberAnimation(_waveWaitTimeValueTMP_Text, _waveWaitTime, 0, _waveWaitTime);

        yield return new WaitForSeconds(_waveWaitTime);

        _startWaveButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "НАЧАТЬ ВОЛНУ";

        _waveState = WaveState.Run;

        StartCoroutine(WaveCoroutine());
    }

    public void AddUnit(Unit unit)
    {
        if (!_allUnitsNow.Contains(unit))
        {
            _allUnitsNow.Add(unit);
        }

        switch (unit.UnitSide)
        {
            case Unit.UnitSideType.Defender:
                if (!_defenderUnitsNow.Contains(unit))
                {
                    _defenderUnitsNow.Add(unit);
                }
                break;
            case Unit.UnitSideType.Attacker:
                if (!_attackerUnitsNow.Contains(unit))
                {
                    _attackerUnitsNow.Add(unit);
                }
                break;
            default:
                break;
        }
    }

    public void RemoveUnit(Unit unit)
    {
        if (_allUnitsNow.Contains(unit))
        {
            _allUnitsNow.Remove(unit);
        }

        switch (unit.UnitSide)
        {
            case Unit.UnitSideType.Defender:
                if (_defenderUnitsNow.Contains(unit))
                {
                    _defenderUnitsNow.Remove(unit);
                }
                break;
            case Unit.UnitSideType.Attacker:
                if (_attackerUnitsNow.Contains(unit))
                {
                    _attackerUnitsNow.Remove(unit);
                }
                break;
            default:
                break;
        }

        if (LeftEnemyInWaveValue <= 0 && _attackerUnitsNow.Count <= 0)
        {
            EndWave(true);
        }
        if (_defenderUnitsNow.Count <= 0)
        {
            EndWave(false);
        }
    }

}