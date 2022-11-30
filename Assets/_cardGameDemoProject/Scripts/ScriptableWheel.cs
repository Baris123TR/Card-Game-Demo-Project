using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Wheel Objects/Wheels", fileName = "New Wheel")]
public class ScriptableWheel : ScriptableObject
{
    public string _type;
    public Sprite _wheel;
    public Sprite _indicator;
    [Space(10)]
    [Header("For Random Fill")]
    public bool _fillContentRandomly;
    public ScriptableWheelContent _bomb;
    public ScriptableWheelContent[] _scriptableContents;
    [Space(10)]
    [Header("For Filling Content Correspondingly")]
    public ScriptableWheelContent[] _scriptableContentsWithQueue;
}