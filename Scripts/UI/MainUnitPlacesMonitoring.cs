using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUnitPlacesMonitoring : MonoBehaviour
{
    [SerializeField] private List<UnitPlace> _mainUnitPlaces;
    [SerializeField] private List<UnitPlace> _unBusyMainUnitPlaces;
    [SerializeField] private List<UnitPlace> _busyMainUnitPlaces;

    public static MainUnitPlacesMonitoring Instance;

    public List<UnitPlace> MainUnitPlaces { get => _mainUnitPlaces; }
    public List<UnitPlace> UnBusyMainUnitPlaces { get => _unBusyMainUnitPlaces; }
    public List<UnitPlace> BusyMainUnitPlaces { get => _busyMainUnitPlaces; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateAllMainUnitPlace();
    }

    public void AddUnBusyMainUnitPlace(UnitPlace unitPlace)
    {
        if (!_unBusyMainUnitPlaces.Contains(unitPlace))
        {
            _unBusyMainUnitPlaces.Add(unitPlace);
        }

        RemoveBusyMainUnitPlace(unitPlace);
    }
    public void RemoveUnBusyUnitPlace(UnitPlace unitPlace)
    {
        if (_unBusyMainUnitPlaces.Contains(unitPlace))
        {
            _unBusyMainUnitPlaces.Remove(unitPlace);
        }
    }
    public UnitPlace GetRandomUnBusyMainUnitPlace()
    {
        return _unBusyMainUnitPlaces[Random.Range(0, _unBusyMainUnitPlaces.Count)];
    }
    public void AddBusyMainUnitPlace(UnitPlace unitPlace)
    {
        if (!_busyMainUnitPlaces.Contains(unitPlace))
        {
            _busyMainUnitPlaces.Add(unitPlace);
        }

        RemoveUnBusyUnitPlace(unitPlace);
    }
    public void RemoveBusyMainUnitPlace(UnitPlace unitPlace)
    {
        if (_busyMainUnitPlaces.Contains(unitPlace))
        {
            _busyMainUnitPlaces.Remove(unitPlace);
        }
    }
    public UnitPlace GetRandomBusyMainUnitPlace()
    {
        return _busyMainUnitPlaces[Random.Range(0, _busyMainUnitPlaces.Count)];
    }

    public void UpdateAllMainUnitPlace()
    {
        foreach (UnitPlace unitPlace in _mainUnitPlaces)
        {
            if (unitPlace.IsBusy)
            {
                AddBusyMainUnitPlace(unitPlace);
            }
            else
            {
                AddUnBusyMainUnitPlace(unitPlace);
            }
        }
    }
}
