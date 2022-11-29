using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotRotationFixer : MonoBehaviour
{
    SpinScript _spinScript;
    public Toggle _updateToggle;
    private void Awake()
    {
        _spinScript = transform.parent.GetComponent<SpinScript>();
        if (!_updateToggle)
        {
            _updateToggle = GameObject.FindWithTag("UpdateToggle").GetComponent<Toggle>();
        }
    }
    private void Update()
    {
        if (_updateToggle.isOn)
        {
            if (_spinScript.LockSlotRotationsBool)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}