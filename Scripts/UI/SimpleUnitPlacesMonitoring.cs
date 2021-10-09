using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleUnitPlacesMonitoring : MonoBehaviour
{
    [SerializeField] private List<UnitPlace> _simpleUnitPlaces;
    [SerializeField] private List<UnitPlace> _unBusySimpleUnitPlaces;
    [SerializeField] private List<UnitPlace> _busySimpleUnitPlaces;

    public static SimpleUnitPlacesMonitoring Instance;

    public List<UnitPlace> SimpleUnitPlaces { get => _simpleUnitPlaces; }
    public List<UnitPlace> UnBusySimpleUnitPlaces { get => _unBusySimpleUnitPlaces; }
    public List<UnitPlace> BusySimpleUnitPlaces { get => _busySimpleUnitPlaces; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateAllSimpleUnitPlace();
    }

    public void AddUnBusySimpleUnitPlace(UnitPlace unitPlace)
    {
        if (!_unBusySimpleUnitPlaces.Contains(unitPlace))
        {
            _unBusySimpleUnitPlaces.Add(unitPlace);
        }

        RemoveBusySimpleUnitPlace(unitPlace);
    }
    public void RemoveUnBusyUnitPlace(UnitPlace unitPlace)
    {
        if (_unBusySimpleUnitPlaces.Contains(unitPlace))
        {
            _unBusySimpleUnitPlaces.Remove(unitPlace);
        }
    }
    public UnitPlace GetRandomUnBusySimpleUnitPlace()
    {
        return _unBusySimpleUnitPlaces[Random.Range(0, _unBusySimpleUnitPlaces.Count)];
    }
    public void AddBusySimpleUnitPlace(UnitPlace unitPlace)
    {
        if (!_busySimpleUnitPlaces.Contains(unitPlace))
        {
            _busySimpleUnitPlaces.Add(unitPlace);
        }

        RemoveUnBusyUnitPlace(unitPlace);
    }
    public void RemoveBusySimpleUnitPlace(UnitPlace unitPlace)
    {
        if (_busySimpleUnitPlaces.Contains(unitPlace))
        {
            _busySimpleUnitPlaces.Remove(unitPlace);
        }
    }
    public UnitPlace GetRandomBusySimpleUnitPlace()
    {
        return _busySimpleUnitPlaces[Random.Range(0, _busySimpleUnitPlaces.Count)];
    }

    public void UpdateAllSimpleUnitPlace()
    {
        foreach (UnitPlace unitPlace in _simpleUnitPlaces)
        {
            if (unitPlace.IsBusy)
            {
                AddBusySimpleUnitPlace(unitPlace);
            }
            else
            {
                AddUnBusySimpleUnitPlace(unitPlace);
            }
        }
    }
}
