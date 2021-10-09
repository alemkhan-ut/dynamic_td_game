using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitVisual : MonoBehaviour
{
    [SerializeField] private ParticleSystem _unitHealParticle;

    public void StartHealEffect()
    {
        _unitHealParticle.Play();
    }
}
