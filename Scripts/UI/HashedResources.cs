using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashedResources : MonoBehaviour
{
    [SerializeField] private List<Sprite> _enemyUnitSprites;

    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _unitContent;
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private Sprite _defenderBulletSprite;

    public GameObject UnitPrefab { get => _unitPrefab; }
    public List<Sprite> EnemyUnitSprites { get => _enemyUnitSprites; }
    public GameObject BulletPrefab { get => _bulletPrefab; }
    public GameObject UnitContent { get => _unitContent; }
    public GameObject MainCanvas { get => _mainCanvas; }
    public Sprite DefenderBulletSprite { get => _defenderBulletSprite; }

    public static HashedResources Instance;

    private void Awake()
    {
        Instance = this;
    }

}
