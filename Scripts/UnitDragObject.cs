using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDragObject : MonoBehaviour
{
    [SerializeField] private Unit _sourceUnit;
    [SerializeField] private GameObject _mergeEffectObject;

    [SerializeField] private Image _unitAvatarImage;
    [SerializeField] private Image _unitAvatarBGImage;
    public void SetupObject(UnitParameters unitParameter, Unit sourceUnit)
    {
        _unitAvatarBGImage.sprite = unitParameter.AvatarBGSprite;
        _unitAvatarImage.sprite = unitParameter.AvatarSprite;
        _sourceUnit = sourceUnit;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Unit>())
        {
            if (collision.GetComponent<Unit>() != _sourceUnit)
            {
                if (collision.GetComponent<Unit>().UnitParameters.UnitType == _sourceUnit.UnitParameters.UnitType)
                {
                    _mergeEffectObject.SetActive(true);
                    _sourceUnit.GetComponent<UnitDrag>().MergeTarget = collision.GetComponent<Unit>();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Unit>())
        {
            if (collision.GetComponent<Unit>() != _sourceUnit)
            {
                _mergeEffectObject.SetActive(false);
                _sourceUnit.GetComponent<UnitDrag>().MergeTarget = null;
            }
        }
    }
}
