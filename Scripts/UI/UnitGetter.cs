using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UnitGetter : MonoBehaviour
{
    [SerializeField] private bool _isRandomUnit;
    [SerializeField] private TMP_Text _unitPriceTMP_Text;
    [SerializeField] private int _unitPrice;
    [Space]
    [SerializeField] private int _unitsBuyed;

    private Button _attachedButton;
    private MainUnitPlacesMonitoring _mainUnitPlacesMonitoring;
    private GameEconomycs _gameEconomycs;

    private void Awake()
    {
        _attachedButton = GetComponent<Button>();
        _attachedButton.onClick.AddListener(GetUnit);
    }

    private void Start()
    {
        _gameEconomycs = GameEconomycs.Instance;
        _mainUnitPlacesMonitoring = MainUnitPlacesMonitoring.Instance;
        SetUnitPrice();
    }

    private void Update()
    {
        if (_mainUnitPlacesMonitoring.UnBusyMainUnitPlaces.Count > 0 &&
            _gameEconomycs.GetGoldValue() >= _unitPrice)
        {
            _attachedButton.interactable = true;
        }
        else
        {
            _attachedButton.interactable = false;
        }
    }

    private void GetUnit()
    {
        if (_mainUnitPlacesMonitoring.UnBusyMainUnitPlaces.Count > 0)
        {
            UnitPlace randomUnitPlace = _mainUnitPlacesMonitoring.GetRandomUnBusyMainUnitPlace();

            if (_isRandomUnit)
            {
                _gameEconomycs.ChangeGoldAmountOn(-_unitPrice);
                randomUnitPlace.InstantiateRandomUnit(Unit.UnitSideType.Defender);
                _unitsBuyed += 1;
            }
        }

        SetUnitPrice();
    }

    public void SetUnitPrice()
    {
        if (_unitsBuyed <= 0)
        {
            _unitPrice = 10;
        }
        else
        {
            _unitPrice = _unitsBuyed * 10;
        }

        _unitPriceTMP_Text.text = _unitPrice.ToString();
    }
}
