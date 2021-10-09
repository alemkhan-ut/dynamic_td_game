using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Windows : MonoBehaviour
{
    [Header("Окно главного меню")]
    [SerializeField] private GameObject _mainMenuWindow;
    [Space]
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _tutorialButton;

    [Header("Окно проигрыша")]
    [SerializeField] private GameObject _loseWindow;
    [Space]
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _backToMenuButton;
    [Space]
    [SerializeField] private TMP_Text _currentWaveTimeValueTMP_Text;
    [SerializeField] private TMP_Text _bestWaveTimeValueTMP_Text;

    public static Windows Instance;

    private void Awake()
    {
        Instance = this;

        if (_loseWindow != null)
        {
            _restartButton.onClick.AddListener(Restart);
            _backToMenuButton.onClick.AddListener(BackToMenu);
            CloseAllWindow();
        }

        if (_mainMenuWindow != null)
        {
            _startGameButton.onClick.AddListener(StartGame);
            _tutorialButton.onClick.AddListener(StartTutorial);
        }

    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void StartTutorial()
    {
    }

    private void Restart()
    {
        SceneManager.LoadScene(1);
    }

    private void BackToMenu()
    {

    }
    public void CloseAllWindow()
    {
        _loseWindow.SetActive(false);
    }

    public void OpenLoseWindow()
    {
        CloseAllWindow();

        _loseWindow.SetActive(true);

        _currentWaveTimeValueTMP_Text.text = Math.Round(SaveData.GetCurrentWaveTime(), 2).ToString() + " секунд";
        _bestWaveTimeValueTMP_Text.text = Math.Round(SaveData.GetBestWaveTime(), 2).ToString() + " секунд";
    }

}
