using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI _fpsCounter;
    private void Update()
    {
        _fpsCounter.text = (1f / Time.deltaTime).ToString("#");
    }
}