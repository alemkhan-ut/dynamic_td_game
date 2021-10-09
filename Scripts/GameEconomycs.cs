using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameEconomycs : MonoBehaviour
{
    [SerializeField] private const int DEFAULT_GOLD_AMOUNT = 100;

    [SerializeField] private TMP_Text _goldAmountTMP_Text;
    [SerializeField] private int _goldAmount;

    public static GameEconomycs Instance;

    private void Awake()
    {
        Instance = this;

        ChangeGoldAmountOn(DEFAULT_GOLD_AMOUNT);
    }

    public void ChangeGoldAmountOn(int value) // Корректировка золота, изменение значения
    {
        int oldGoldAmount = _goldAmount;

        _goldAmount += value;

        print("Золото изменено, было: " + oldGoldAmount + ". Стало: " + _goldAmount);

        TextAnimation.IntNumberAnimation(_goldAmountTMP_Text, oldGoldAmount, _goldAmount, 1f);
    }

    public int GetGoldValue()
    {
        return _goldAmount;
    }
}