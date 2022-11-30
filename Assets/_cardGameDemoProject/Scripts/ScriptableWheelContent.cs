using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Wheel Objects/Wheel Content", fileName = "New Wheel Content")]
public class ScriptableWheelContent : ScriptableObject
{
    public enum ItemType
    {
        Gold,
        Cash,
        WeaponPoint,
        UpgradePoint,
        Bomb
    }
    public ItemType _itemType;

    public Sprite _contentImage;

    public Vector2 _bonusAmountRandomBetween;

    [HideInInspector]
    public int _bonusAmountCalculated
    {
        get
        {
            return Mathf.RoundToInt(Random.Range(_bonusAmountRandomBetween.x, _bonusAmountRandomBetween.y));
        }
    }
    public enum Rarity
    {
        Null,
        Common,
        Rare,
        Epic,
        Legendary
    }
    public Rarity _rarity;
    
    [Header("Weapon Name")]
    public string _weaponTypeName;
    [Header("Item Specs")]
    public bool _offerShine;
    public bool _particles;
}