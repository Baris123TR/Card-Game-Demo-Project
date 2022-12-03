using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Card Game Objects/Wheel/New Wheel Content", fileName = "Wheel Content")]
public class ScriptableWheelContent : ItemTypeIncludedMonoBehaviour
{
    public string _newNameString;
    public GameObject _iconPrefabToInstantiate;
    public string _name
    {
        get
        {
            if (_newNameString == "")
            {
                return name;
            }
            else
            {
                return _newNameString;
            }
        }
    }
    
    public ItemType _itemType;

    public Sprite _contentImage;

    public Vector2 _bonusAmountRandomBetween;
    public bool _bonusAmountTextAndIconEnabled;

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

    [Header("Colors")]
    public Color _generalColor;
    
    [Header("Weapon Name")]
    public string _weaponTypeName;
    public bool _weaponTypeNameTexAndBackgroundEnabled;
    [Header("Item Specs")]
    public bool _offerShineEnabled;
    public bool _activateParticles;
}