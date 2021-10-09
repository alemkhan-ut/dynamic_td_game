using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewUnitParameter", menuName = "Unit Parameter", order = 1)]
[System.Serializable]
public class UnitParameters : ScriptableObject
{
    [SerializeField] private Preset _preset;

    [SerializeField] private string _name;
    [SerializeField] private UnitTypes _unitType;
    [Header("Визуальные параметры")]
    [SerializeField] private Sprite _avatarSprite;
    [SerializeField] private Sprite _avatarBGSprite;
    [Space]
    [SerializeField] private Color _hpBarColor;
    [SerializeField] private Color _hpBarFillColor;
    [Space]
    [SerializeField] private Sprite _bulletSprite;
    [Tooltip("Small - 25, Medium - 35, Large - 45")]
    [SerializeField] private float _bulletSize;
    [SerializeField] private Color _bulletSpriteColor;
    [Header("Характеристики")]
    [SerializeField] private int _currentLevel;
    [Tooltip("Small - 50, Medium - 100, Large - 200")]
    [SerializeField] private float _hpMaxValue;
    [SerializeField] private bool _isMoveable;
    [Tooltip("Small - 1, Medium - 2.5, Large - 5")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private bool _isAttackable;
    [Tooltip("Small - 1, Medium - 5, Large - 10")]
    [SerializeField] private float _attackDamage;
    [Tooltip("Slow - 1, Normal - .5, Fast - .25")]
    [SerializeField] private float _attackDelay;
    [Tooltip("Small - 10, Medium - 15, Large - 20")]
    [SerializeField] private float _attackSpeed;
    [Tooltip("Small - 3, Medium - 6, Large - 12")]
    [SerializeField] private float _attackRange;
    [SerializeField] private Unit.AttackType _attackType;
    [Header("Способность")]
    [SerializeField] private bool _isSpellable;
    [SerializeField] private float _spellValue;
    [SerializeField] private float _spellRange;
    [SerializeField] private float _spellDelay;
    [Space]
    [SerializeField] private Unit.UnitSideType _unitSide;
    [SerializeField] private int _destructionReward;

    public string Name { get => _name; }
    public UnitTypes UnitType { get => _unitType; }
    public Sprite AvatarSprite { get => _avatarSprite; }
    public Sprite AvatarBGSprite { get => _avatarBGSprite; }
    public Color HPBarColor { get => _hpBarColor; }
    public Color HPBarFillColor { get => _hpBarFillColor; }
    public Sprite BulletSprite { get => _bulletSprite; }
    public float BulletSize { get => _bulletSize; }
    public Color BulletSpriteColor { get => _bulletSpriteColor; }
    public float HPMaxValue { get => _hpMaxValue; }
    public bool IsMoveable { get => _isMoveable; }
    public float MoveSpeed { get => _moveSpeed; }
    public bool IsAttackable { get => _isAttackable; }
    public float AttackDamage { get => _attackDamage; }
    public float AttackDelay { get => _attackDelay; }
    public float AttackSpeed { get => _attackSpeed; }
    public float AttackRange { get => _attackRange; }
    public Unit.AttackType AttackType { get => _attackType; }
    public Unit.UnitSideType UnitSide { get => _unitSide; }
    public int DestructionReward { get => _destructionReward; }
    public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }
    public float SpellValue { get => _spellValue; set => _spellValue = value; }
    public float SpellRange { get => _spellRange; set => _spellRange = value; }
    public float SpellDelay { get => _spellDelay; set => _spellDelay = value; }
    public bool IsSpellable { get => _isSpellable; set => _isSpellable = value; }

    public void ResetToDefault()
    {
        _preset.ApplyTo(this);
    }

    public bool LevelUp(int levelUpPrice)
    {
        if (_currentLevel < 4)
        {
            if (GameEconomycs.Instance.GetGoldValue() >= levelUpPrice)
            {
                GameEconomycs.Instance.ChangeGoldAmountOn(-levelUpPrice);

                _currentLevel += 1;

                ChangeAttackDamage(3);
                ChangeAttackDelay(-3);

                return true;
            }
        }

        return false;
    }

    public void ChangeAttackDamage(float changeProcent) { _attackDamage += (_attackDamage * (changeProcent / 100)); }
    public void ChangeAttackDelay(float changeProcent) { _attackDelay += (_attackDelay * (changeProcent / 100)); }
    public void ChangeAttackSpeed(float changeProcent) { _attackSpeed += (_attackSpeed * (changeProcent / 100)); }
    public void ChangeAttackRange(float changeProcent) { _attackRange += (_attackRange * (changeProcent / 100)); }
    public void ChangeSpellValue(float changeProcent) { _spellValue += (_spellValue * (changeProcent / 100)); }
    public void ChangeSpellRange(float changeProcent) { _spellRange += (_spellRange * (changeProcent / 100)); }
    public void ChangeSpellDelay(float changeProcent) { _spellDelay += (_spellDelay * (changeProcent / 100)); }
    public void ChangeMoveSpeed(float changeProcent) { _moveSpeed += (_moveSpeed * (changeProcent / 100)); }
    public void ChangeHPMax(float changeProcent) { _hpMaxValue += (_hpMaxValue * (changeProcent / 100)); }

    public enum UnitTypes
    {
        ShieldBearer,
        Archer,
        Healer,
        Swordsman,
        Jester,
        CoalThrower,
        Skelet,
        Hammer,
        TankBoss
    }
}
