using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using TMPro;

public class TextAnimation : MonoBehaviour
{

    [SerializeField] private bool _isAwakeAnimation;

    [Header("NUMBER ANIMATION")]
    [SerializeField] private bool _isNumberAnimation;
    [SerializeField] private float _fromValue;
    [SerializeField] private float _toValue;
    [Header("STRING ANIMATION")]
    [SerializeField] private bool _isStringAnimation;
    [SerializeField] private bool isAwakeAnimation;
    [SerializeField] private string _textTo;

    [SerializeField] private float _aniamtionDuration;

    public static void IntNumberAnimation(TMP_Text textComponent, float from, float to, float duration)
    {
        textComponent.text = from.ToString();
        DOVirtual.Float(from, to, duration, (x) => textComponent.text = Mathf.Floor(x).ToString());
    }
    public static void FloatNumberAnimation(TMP_Text textComponent, float from, float to, float duration)
    {
        textComponent.text = from.ToString();
        DOVirtual.Float(from, to, duration, (x) => textComponent.text = Math.Round(x, 2).ToString());
    }
}
