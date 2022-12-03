using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveAndExitButton : MonoBehaviour
{
    [HideInInspector] public Button _exitButton;
    public RestartSceneButtonScript _restartSceneScript;
    private void Awake()
    {
        _exitButton = GetComponent<Button>();
        _exitButton.onClick.AddListener(SaveAndExit);
    }
    private void Start()
    {
        
    }
    public void SaveTheEarnings()
    {
        print("Save method called.");
    }
    public void SaveAndExit()
    {
        SaveTheEarnings();
        _restartSceneScript.RestartScene();
    }
}