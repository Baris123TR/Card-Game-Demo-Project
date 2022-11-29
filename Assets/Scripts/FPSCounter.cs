using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public Slider _fpsSlider;
    public TextMeshProUGUI _fpsCounter;
    public TextMeshProUGUI _SetFps;
    private void Update()
    {
        Application.targetFrameRate = (int)_fpsSlider.value;
        _fpsCounter.text = (1f / Time.deltaTime).ToString("#");
    }
}