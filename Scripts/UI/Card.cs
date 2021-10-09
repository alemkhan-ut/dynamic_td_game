using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Card : MonoBehaviour
{
    [SerializeField] private UnitParameters _unitParameters;

    [SerializeField] private string _name;
    [SerializeField] private TMP_Text _levelTMP_Text;
    [SerializeField] private int _defaultLevel = 1;
    [SerializeField] private int _levelValue;
    [SerializeField] private Image _cardBGImage;
    [SerializeField] private Image _avatarImage;
    [SerializeField] private Sprite _avatarSprite;
    [SerializeField] private int _defaultPrice = 100;
    [SerializeField] private int _priceValue;
    [SerializeField] private TMP_Text _priceTMP_Text;
    [Space]
    [SerializeField] private Outline _outline;
    [SerializeField] private Color _highlightColor;
    [SerializeField] private Color _defaultColor;

    private Button _attachedButton;
    private CardsContent _cardsContent;

    public string Name { get => _name; }
    public Sprite AvatarSprite { get => _avatarSprite; }
    public int PriceValue { get => _priceValue; }

    private void Awake()
    {
        _attachedButton = GetComponent<Button>();
    }

    private void Start()
    {
        _attachedButton.onClick.AddListener(UpgradeSelectedUnit);
        _cardsContent = CardsContent.Instance;
    }

    private void Update()
    {
        if (GameEconomycs.Instance.GetGoldValue() >= _priceValue)
        {
            _attachedButton.interactable = true;
        }
        else
        {
            _attachedButton.interactable = false;
        }
    }

    public void SetCard(UnitParameters unitParameter)
    {
        _unitParameters = unitParameter;

        name = "Карточка: " + _unitParameters.Name;
        _avatarImage.sprite = _unitParameters.AvatarSprite;

        _levelValue = _unitParameters.CurrentLevel;


        switch (_levelValue)
        {
            case 1:
                _priceValue = 100;
                break;
            case 2:
                _priceValue = 200;
                break;
            case 3:
                _priceValue = 400;
                break;
            case 4:
                _priceValue = 700;
                break;
            default:
                break;
        }

        _levelTMP_Text.text = _levelValue.ToString();
        _priceTMP_Text.text = _priceValue.ToString();
    }

    public void SelectCard()
    {
        _cardsContent.SetCurrentCard(this);

        UpdateCardColor();
    }

    public void UpgradeSelectedUnit()
    {
        SelectCard();

        if (_unitParameters.LevelUp(_priceValue))
        {
            SetCard(_unitParameters);
        }
    }

    public void ResetToDefault()
    {
        _levelTMP_Text.text = _defaultLevel.ToString();
        _priceTMP_Text.text = _defaultPrice.ToString();
    }

    public void UpdateCardColor()
    {
        if (_cardsContent.GetCurrentCard() == this)
        {
            //_outline.effectColor = _highlightColor;
            _cardBGImage.color = _highlightColor;
        }
        else
        {
            //_outline.effectColor = _defaultColor;
            _cardBGImage.color = _defaultColor;
        }
    }
}
