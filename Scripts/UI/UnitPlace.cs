using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UnitPlace : MonoBehaviour
{
    [SerializeField] private int _index;
    [Space]
    [SerializeField] private GameObject _unitOnPlace;
    private Transform _transform;

    private Button _attachedButton;
    private CardsContent _cardsContent;

    [SerializeField] private bool _isBusy;

    public bool IsBusy { get => _isBusy; }

    private void Awake()
    {
        _transform = transform;
        _attachedButton = GetComponent<Button>();

        _index = _transform.GetSiblingIndex();
        string name = gameObject.name;
        gameObject.name = "[" + _index + "] " + name;
    }

    private void Start()
    {
        _cardsContent = CardsContent.Instance;
    }

    private void InstantiateUnit()
    {
        if (!_isBusy)
        {
            if (_cardsContent.GetCurrentCard() != null)
            {
                GameObject unitObject = Instantiate(HashedResources.Instance.UnitPrefab, _transform);

                MainArea.Instance.AddUnit(unitObject.GetComponent<Unit>());
            }
        }
    }
    public void InstantiateRandomUnit(Unit.UnitSideType unitSideType)
    {
        if (!_isBusy)
        {
            GameObject unitObject = Instantiate(HashedResources.Instance.UnitPrefab, HashedResources.Instance.UnitContent.transform);
            Unit unit = unitObject.GetComponent<Unit>();

            unit.transform.position = transform.position;
            unit.SpawnPlace = this;

            switch (unitSideType)
            {
                case Unit.UnitSideType.Defender:
                    unit.SetupUnitParameters(MainArea.Instance.DefenderUnitParameters[Random.Range(0, MainArea.Instance.DefenderUnitParameters.Count)]);
                    break;
                case Unit.UnitSideType.Attacker:
                    unit.SetupUnitParameters(MainArea.Instance.AttackerUnitParameters[Random.Range(0, MainArea.Instance.AttackerUnitParameters.Count)]);
                    break;
                default:
                    break;
            }

            MainArea.Instance.AddUnit(unit);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Unit>())
        {
            //print("Unit: " + collision.GetComponent<Unit>().name + " на клетке: " + gameObject.name);
            _unitOnPlace = collision.gameObject;

            _isBusy = true;

            MainUnitPlacesMonitoring.Instance.UpdateAllMainUnitPlace();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Unit>())
        {
            //print("Unit: " + collision.GetComponent<Unit>().name + " покинул клетку: " + gameObject.name);
            _unitOnPlace = null;
            _isBusy = false;

            MainUnitPlacesMonitoring.Instance.UpdateAllMainUnitPlace();
        }
    }
}
