using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Card Game Objects/Wheel/New Wheel", fileName = "New Wheel")]
public class ScriptableWheel : ScriptableObject
{
    public WheelTypeNameAndImages _wheelTypeData;

    public enum Type
    {
        Bronze,
        Silver,
        Gold
    }
    public Type _type;
    [Space(10)]
    [Header("For Random Fill")]
    public bool _fillContentRandomly;
    public ScriptableWheelContent _bomb;
    public ScriptableWheelContent[] _scriptableContents;
    [Space(10)]
    [Header("For Filling Content Correspondingly")]
    public ScriptableWheelContent[] _scriptableContentsWithQueue;
}