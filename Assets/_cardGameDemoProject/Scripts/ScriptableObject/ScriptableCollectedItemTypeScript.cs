using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemTypeIncludedScriptableObject : ScriptableObject
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
public class ScriptableCollectedItemTypeScript : ItemTypeIncludedScriptableObject
{
    public ItemType _itemType;
}