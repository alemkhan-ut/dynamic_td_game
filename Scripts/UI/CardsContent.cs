using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsContent : MonoBehaviour
{
    [SerializeField] private Card _currentCard;
    [Space]
    [SerializeField] private List<Card> _cards;
    [SerializeField] private List<UnitParameters> _unitPool;

    public static CardsContent Instance;

    public List<UnitParameters> UnitPool { get => _unitPool; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetupUnitPool();
        ResetUnitPoolToDefault();
    }

    public void SetupUnitPool() // TO DO: Рассмотреть правильно данного варианта
    {
        int unitAddedAmount = 0;

        while (true)
        {
            UnitParameters randomUnitParameter = MainArea.Instance.DefenderUnitParameters[Random.Range(0, MainArea.Instance.DefenderUnitParameters.Count)];

            if (!_unitPool.Contains(randomUnitParameter))
            {
                _unitPool.Add(randomUnitParameter);
                _cards[unitAddedAmount].SetCard(randomUnitParameter);

                unitAddedAmount++;

                if (_unitPool.Count >= _cards.Count)
                {
                    break;
                }
            }
        }
    }

    public void ResetUnitPoolToDefault()
    {
        if (_unitPool.Count != 0)
        {
            foreach (UnitParameters unitParameter in _unitPool)
            {
                unitParameter.ResetToDefault();
            }
        }

        ResetAllCards();
    }
    public void ResetAllCards()
    {
        if (_cards.Count != 0)
        {
            foreach (Card card in _cards)
            {
                card.ResetToDefault();
            }
        }
    }

    public void SetCurrentCard(Card card)
    {
        _currentCard = card;

        foreach (Card cardInList in _cards)
        {
            cardInList.UpdateCardColor();
        }
    }
    public Card GetCurrentCard()
    {
        return _currentCard;
    }

    public Card GetRandomCard()
    {
        return _cards[Random.Range(0, _cards.Count)];
    }


}
