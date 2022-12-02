using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[CreateAssetMenu(menuName = "Card Game Objects/New Global Sprite Data", fileName = "Global Sprite Data")]

public class ScriptableItemIcons : ScriptableObject
{
    [Header("Sprite Atlas For All Sprites")]
    public SpriteAtlas _allSpritesAtlas;

    [Header("Inventory Icon Sprites")]
    public Sprite _gold;
    public Sprite _cash;
    public Sprite _weaponUpgrade;
    public Sprite _gearUpgrade;

    [Header("Level Sprites")]
    public Sprite _zoomedNormal;
    public Sprite _zoomedSuper;
    public Sprite _zoomedGold;

    public Sprite _normal;

    public Color _normalLevelColor;
    public Color _superLevelColor;
    public Color _goldLevelColor;
}