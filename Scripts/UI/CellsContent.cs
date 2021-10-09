using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsContent : MonoBehaviour
{
    [SerializeField] GameObject _cellContentBG;
    [SerializeField] GameObject _generalCellsContent;

    public void SwitchActiveGeneralCells()
    {
        _generalCellsContent.SetActive(!_generalCellsContent.activeSelf);
    }
}
