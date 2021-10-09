using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _bulletOwner;
    [SerializeField] private string _bulletOwnerName;
    [SerializeField] private BulletTeam _bulletTeam;
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _target;
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackSpeed;

    private Transform _transform;

    public BulletTeam BulletTeam1 { get => _bulletTeam; set => _bulletTeam = value; }

    public enum BulletTeam
    {
        Defender,
        Attacker
    }

    private void Awake()
    {
        _transform = transform;
    }

    public void DestroyBullet()
    {
        print("Выстрел уничтожен. Владелец выстрела: " + _bulletOwnerName);
        Destroy(gameObject);
    }

    public void SetupBulletParameters(GameObject owner, GameObject target, UnitParameters parameters)
    {
        _bulletOwner = owner.gameObject;
        _bulletOwnerName = _bulletOwner.name;

        _image.sprite = parameters.BulletSprite;
        _image.color = parameters.BulletSpriteColor;

        _target = target;

        _attackSpeed = parameters.AttackSpeed;
        _attackDamage = parameters.AttackDamage;

        switch (parameters.UnitSide)
        {
            case Unit.UnitSideType.Defender:
                _bulletTeam = BulletTeam.Defender;
                break;
            case Unit.UnitSideType.Attacker:
                _bulletTeam = BulletTeam.Attacker;
                break;
            default:
                break;
        }
    }

    public float GetAttackDamage()
    {
        return _attackDamage;
    }

    private void Update()
    {
        if (_target != null)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _target.transform.position, _attackSpeed * Time.deltaTime);
        }
        else
        {
            DestroyBullet();
        }
    }
}
