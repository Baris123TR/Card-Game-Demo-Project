using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotRotationFixer : MonoBehaviour
{
    SpinScript _spinScript;
    private void Awake()
    {
        _spinScript = transform.GetComponentInParent<SpinScript>();
    }
    private void Update()
    {
        if (_spinScript.LockSlotRotationsBool)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}