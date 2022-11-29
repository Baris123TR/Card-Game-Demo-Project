using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Wheel Objects/Items", fileName = "Wheel Items")]
public class ScriptableWheelItems : ScriptableObject
{
    public List<Sprite> _upgradeIcons;
    public List<Sprite> _gunIcons;
    public Sprite _bomb;
}