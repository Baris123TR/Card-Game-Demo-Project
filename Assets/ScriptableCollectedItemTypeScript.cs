using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemTypeIncludedMonoBehaviour : ScriptableObject
{
    public enum ItemType
    {
        Gold,
        Cash,
        WeaponPoint,
        UpgradePoint,
        Bomb
    }
}

[CreateAssetMenu(menuName = "Card Game Objects/Collected Item Type Setter Object")]
public class ScriptableCollectedItemTypeScript : ItemTypeIncludedMonoBehaviour
{
    public ItemType _itemType;
}