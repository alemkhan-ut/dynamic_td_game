using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleController : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Update()
    {
        Time.timeScale = _slider.value;
    }
}
