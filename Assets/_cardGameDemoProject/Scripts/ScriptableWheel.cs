using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Wheel Objects/Wheels", fileName = "New Wheel")]
public class ScriptableWheel : ScriptableObject
{
    public string _type;
    public WheelContent[] _scriptableContents;
    public Sprite _wheel;
    public Sprite _indicator;
}