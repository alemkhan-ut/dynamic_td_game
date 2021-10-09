using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerUnitSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _spawnWarning;
    [SerializeField] private float _spawnWarningDuration = 3f;

    private void Awake()
    {
        _spawnWarning = transform.GetChild(0).gameObject;
        _spawnWarning.SetActive(false);
    }
    public IEnumerator StartWarning(float duration)
    {
        _spawnWarning.SetActive(true);
        yield return new WaitForSeconds(duration);
        _spawnWarning.SetActive(false);
    }
}
