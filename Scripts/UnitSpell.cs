using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpell : MonoBehaviour
{
    [SerializeField] private Unit _sourceUnit;
    [SerializeField] private UnitParameters.UnitTypes _unitType;
    [SerializeField] private float _spellValue;
    [SerializeField] private List<Unit> _unitsOnSpell;

    private UnitVisual _unitVisual;


    private void Start()
    {
        _sourceUnit = GetComponent<Unit>();
        _unitVisual = GetComponent<UnitVisual>();
    }

    public void UseSpell()
    {
        print("Начало способности у " + gameObject.name);

        _unitType = _sourceUnit.UnitParameters.UnitType;

        foreach (Unit unit in _unitsOnSpell)
        {
            switch (_unitType)
            {
                case UnitParameters.UnitTypes.ShieldBearer:
                    break;
                case UnitParameters.UnitTypes.Archer:
                    break;
                case UnitParameters.UnitTypes.Healer:
                    if (unit.UnitParameters.UnitSide == Unit.UnitSideType.Defender)
                    {
                        unit.Heal(_spellValue);
                    }
                    break;
                case UnitParameters.UnitTypes.Swordsman:
                    break;
                case UnitParameters.UnitTypes.Jester:
                    break;
                case UnitParameters.UnitTypes.CoalThrower:
                    break;
                case UnitParameters.UnitTypes.Skelet:
                    break;
                case UnitParameters.UnitTypes.Hammer:
                    break;
                default:
                    break;
            }
        }

        print("Конец способности у " + gameObject.name);
    }

    public void ActiveSpell()
    {

        print("Начало активации способности у " + gameObject.name);
        _spellValue = _sourceUnit.UnitParameters.SpellValue;

        RaycastHit2D[] raycastHits = Physics2D.CircleCastAll(transform.position, _sourceUnit.UnitParameters.SpellRange, Vector2.zero);
        _unitsOnSpell.Clear();

        for (int i = 0; i < raycastHits.Length; i++)
        {
            Unit raycastUnit = raycastHits[i].collider.GetComponent<Unit>();

            if (raycastUnit)
            {
                if (raycastUnit != _sourceUnit)
                {
                    if (!_unitsOnSpell.Contains(raycastUnit))
                    {
                        _unitsOnSpell.Add(raycastUnit);
                    }
                }
            }
        }

        print("Конец активации способности у " + gameObject.name);
        UseSpell();
    }
}
