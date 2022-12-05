using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Game Objects/Wheel/New Wheel Type Data", fileName = "Wheel Type Data")]
public class ScriptableWheelIconData
    : ScriptableObject
{
    [Header("Bronze")]
    public Sprite _bronzeWheel;
    public Sprite _bronzeIndicator;
    [Space(10)]
    [Header("Silver")]
    public Sprite _silverWheel;
    public Sprite _silverIndicator;
    [Space(10)]
    [Header("Gold")]
    public Sprite _goldWheel;
    public Sprite _goldIndicator;
}