using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitParameters _unitParameters;
    [Space]
    private string _name;
    private UnitParameters.UnitTypes _unitType;
    private Sprite _avatarSprite;
    private Sprite _avatarBGSprite;
    private Color _hpBarColor;
    private Color _hpBarFillColor;
    private Sprite _bulletSprite;
    private float _bulletSize;
    private Color _bulletSpriteColor;
    private float _hpMaxValue;
    private bool _isMoveable;
    private bool _isSpellable;
    private float _moveSpeed;
    private bool _isAttackable;
    private float _attackDamage;
    private float _attackDelay;
    private float _attackSpeed;
    private float _attackRange;
    private AttackType _attackType;
    private UnitSideType _unitSide;
    private int _destructionReward;

    [Header("Состояние")]
    [SerializeField] private float _HPValue;
    [SerializeField] private float _spawnTime;
    [SerializeField] private bool _isSpawned;
    [SerializeField] private Unit _target;
    [SerializeField] private float _targetDistance;
    [SerializeField] private UnitPlace _spawnPlace;
    [Space]
    [SerializeField] private bool _canAttack;
    [SerializeField] private bool _inAttack;
    [SerializeField] private bool _inSpell;
    [Header("Cсылки для параметров")]
    [SerializeField] private Image _HPBarFillImage;
    [SerializeField] private Image _HPBarImage;
    [SerializeField] private Image _avatarImage;
    [SerializeField] private Image _avatarBGImage;
    [SerializeField] private TMP_Text _levelTMP_Text;
    [SerializeField] private int _levelValue;

    public UnitSideType UnitSide { get => _unitParameters.UnitSide; }
    public bool IsAttackable { get => _unitParameters.IsAttackable; }
    public Sprite BulletSprite { get => _unitParameters.BulletSprite; }
    public UnitPlace SpawnPlace { get => _spawnPlace; set => _spawnPlace = value; }
    public UnitParameters UnitParameters { get => _unitParameters; }
    public TMP_Text LevelTMP_Text { get => _levelTMP_Text; set => _levelTMP_Text = value; }

    public enum AttackType
    {
        Melee,
        Range
    }

    public enum UnitSideType
    {
        Defender,
        Attacker
    }

    public void TakeDamage(float damage)
    {
        if (_HPValue > 0)
        {
            _HPValue -= damage;

            if (_HPValue <= 0)
            {
                UnitDestruction();
            }

            _HPBarFillImage.fillAmount = _HPValue / _unitParameters.HPMaxValue;
        }
    }

    public bool FindTarget()
    {
        float _nearestDistanceValue = Int32.MaxValue;

        switch (UnitSide)
        {
            case UnitSideType.Defender:
                foreach (Unit unit in MainArea.Instance.AttackerUnitsNow)
                {
                    float distance = Vector3.Distance(transform.position, unit.transform.position);

                    if (distance < _nearestDistanceValue)
                    {
                        _nearestDistanceValue = distance;
                        _target = unit;
                    }
                }
                break;
            case UnitSideType.Attacker:
                foreach (Unit unit in MainArea.Instance.DefenderUnitsNow)
                {
                    float distance = Vector3.Distance(transform.position, unit.transform.position);

                    if (distance < _nearestDistanceValue)
                    {
                        _nearestDistanceValue = distance;
                        _target = unit;
                    }
                }
                break;
            default:
                break;
        }

        if (_target != null)
        {
            Debug.DrawLine(transform.position, _target.transform.position, Color.red, 10f);
            return true;
        }
        else
        {
            return false;
        }
    }
    private void Update()
    {
        _levelTMP_Text.text = _levelValue.ToString();

        if (FindTarget())
        {
            float targetDistance = Vector3.Distance(transform.position, _target.transform.position);

            _targetDistance = targetDistance;

            if (targetDistance > _unitParameters.AttackRange)
            {
                if (_isMoveable)
                {
                    transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _unitParameters.MoveSpeed * Time.deltaTime);
                }

                _canAttack = false;
            }
            else
            {
                _canAttack = true;
            }
        }
        else
        {
            _canAttack = false;
        }

        if (!_inAttack && _isAttackable)
        {
            StartCoroutine(AttackCoroutine());

            switch (_unitParameters.AttackType)
            {
                case AttackType.Melee:
                    break;
                case AttackType.Range:
                    break;
                default:
                    break;
            }
        }

        if (!_inSpell && _isSpellable)
        {
            StartCoroutine(SpellCoroutine());
        }
    }


    public IEnumerator AttackCoroutine()
    {
        _inAttack = true;

        yield return new WaitForSeconds(_unitParameters.AttackDelay);

        if (_canAttack && _target._isSpawned)
        {
            GameObject bulletObject = Instantiate(HashedResources.Instance.BulletPrefab, transform.position, Quaternion.identity, HashedResources.Instance.MainCanvas.transform);
            Bullet bullet = bulletObject.GetComponent<Bullet>();

            bullet.SetupBulletParameters(gameObject, _target.gameObject, _unitParameters);

            _inAttack = false;
        }

        StartCoroutine(AttackCoroutine());
    }

    public IEnumerator SpellCoroutine()
    {
        _inSpell = true;
        yield return new WaitForSeconds(_unitParameters.SpellDelay);

        GetComponent<UnitSpell>().ActiveSpell();

        _inSpell = false;

        StartCoroutine(SpellCoroutine());
    }

    public void UnitDestruction()
    {
        if (UnitSide == UnitSideType.Attacker)
        {
            GameEconomycs.Instance.ChangeGoldAmountOn(_unitParameters.DestructionReward);
        }

        MainArea.Instance.RemoveUnit(this);

        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bullet>())
        {
            Bullet bullet = collision.GetComponent<Bullet>();

            if (bullet.BulletTeam1 == Bullet.BulletTeam.Attacker && _unitParameters.UnitSide == UnitSideType.Defender)
            {
                TakeDamage(bullet.GetAttackDamage());

                bullet.DestroyBullet();
            }

            if (bullet.BulletTeam1 == Bullet.BulletTeam.Defender && _unitParameters.UnitSide == UnitSideType.Attacker)
            {
                TakeDamage(bullet.GetAttackDamage());

                bullet.DestroyBullet();
            }
        }
    }

    public void SetupUnitParameters(UnitParameters unitParameters)
    {
        _unitParameters = unitParameters;

        if (GetComponent<UnitDrag>())
        {
            GetComponent<UnitDrag>().UnitParameter = unitParameters;

            if (_unitParameters.UnitSide == UnitSideType.Defender)
            {
                GetComponent<UnitDrag>().IsDragable = true;
            }
        }

        name = _unitParameters.Name;

        _levelValue = _unitParameters.CurrentLevel;

        _levelTMP_Text.text = _levelValue.ToString();

        _isMoveable = _unitParameters.IsMoveable;
        _isAttackable = _unitParameters.IsAttackable;
        _isSpellable = _unitParameters.IsSpellable;

        _destructionReward = _unitParameters.DestructionReward;

        int currentWave = MainArea.Instance.CurrentWave;

        if (currentWave >= 1 && currentWave < 10)
        {
            _destructionReward *= 1;
        }
        else if (currentWave >= 10 && currentWave < 20)
        {
            _destructionReward *= 2;
        }
        else if (currentWave >= 20 && currentWave < 30)
        {
            _destructionReward *= 3;
        }
        else if (currentWave >= 30 && currentWave < 40)
        {
            _destructionReward *= 4;
        }
        else if (currentWave >= 40)
        {
            _destructionReward *= 5;
        }

        _HPValue = _unitParameters.HPMaxValue;
        _HPBarFillImage.fillAmount = _HPValue / _unitParameters.HPMaxValue;

        _avatarImage.sprite = _unitParameters.AvatarSprite;
        _avatarBGImage.sprite = _unitParameters.AvatarBGSprite;

        _HPBarImage.color = _unitParameters.HPBarColor;
        _HPBarFillImage.color = _unitParameters.HPBarFillColor;

        _isMoveable = false;

        StartCoroutine(SpawnDelayCoroutine());
    }

    public void Merge(Unit unit) // TO DO: Отвязать UnitParameters от Локальных значений
    {
        float newHPMaxValue = _unitParameters.HPMaxValue + unit.UnitParameters.HPMaxValue;
        float newHPValue = _HPValue + unit._HPValue;

        _HPBarFillImage.fillAmount = _HPValue / _hpMaxValue;

        MainArea.Instance.RemoveUnit(unit);
        Destroy(unit.gameObject);

        SetupUnitParameters(MainArea.Instance.DefenderUnitParameters[UnityEngine.Random.Range(0, MainArea.Instance.DefenderUnitParameters.Count)]);

        _levelValue += 1;

        _levelTMP_Text.text = _levelValue.ToString();

        _HPValue = newHPValue;
        _HPBarFillImage.fillAmount = _HPValue / newHPMaxValue;

        print("Произошло слияние - " + "Старое макс. здоровье: " + _unitParameters.HPMaxValue + ". Новое макс. здоровье: " + newHPMaxValue);
        print("Произошло слияние - " + "Старое тек. здоровье: " + (_HPValue - unit._HPValue) + ". Новое тек. здоровье: " + newHPValue);
    }

    public void Heal(float value)
    {
        if ((_HPValue + value) <= _unitParameters.HPMaxValue)
        {
            print("Юнит пополняет здоровье - Было: " + _HPValue + ". Стало: " + (_HPValue += value) + ". Макс. здоровье: " + _unitParameters.HPMaxValue);

            _HPValue += value;
            _HPBarFillImage.fillAmount = _HPValue / _unitParameters.HPMaxValue;

            GetComponent<UnitVisual>().StartHealEffect();
        }
    }

    private IEnumerator SpawnDelayCoroutine()
    {
        if (_unitParameters.UnitSide == UnitSideType.Attacker)
        {
            StartCoroutine(_spawnPlace.gameObject.GetComponent<AttackerUnitSpawn>().StartWarning(_spawnTime));

            yield return new WaitForSeconds(_spawnTime);

            _isMoveable = true;
        }

        if (_unitParameters.UnitSide == UnitSideType.Defender)
        {

        }

        _isSpawned = true;
    }
}