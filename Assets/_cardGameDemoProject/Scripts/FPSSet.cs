using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSSet : MonoBehaviour
{
    public int _targetFps = 144;
    private void Awake()
    {
        Application.targetFrameRate = _targetFps;
    }
    private void Start()
    {
        
    }
}