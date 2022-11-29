using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraToggleScript : MonoBehaviour
{
    public Toggle _cameraToggle;
    public Camera _mainCam;
    private void Awake()
    {
        if (!_cameraToggle)
        {
            _cameraToggle = GameObject.FindWithTag("CameraToggle").GetComponent<Toggle>();
        }
        _mainCam = GetComponent<Camera>();
    }
    private void Update()
    {
        if (_cameraToggle.isOn)
        {
            _mainCam.enabled = true;
        }
        else
        {
            _mainCam.enabled = false;
        }
    }
}