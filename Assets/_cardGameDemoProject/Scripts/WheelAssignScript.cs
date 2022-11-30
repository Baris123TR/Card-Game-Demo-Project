using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelAssignScript : MonoBehaviour
{
    [Header("Scriptable Data")]
    public ScriptableWheel _scriptableWheel;
    [Header("Hierarchy")]
    public Image _wheel;
    public Image _wheelIndicator;
    public Transform _slotsParentTransform;
    /*[HideInInspector]*/ public GameObject[] _slots;
    /*[HideInInspector]*/ public GameObject[] _slotContent;
    private void Awake()
    {
        _slots = new GameObject[_slotsParentTransform.childCount];
        for (int i = 0; i < _slotsParentTransform.childCount; i++)
        {
            _slots[i] = _slotsParentTransform.GetChild(i).gameObject;
        }
    }
    public void UpdateWheel()
    {
        
    }
}