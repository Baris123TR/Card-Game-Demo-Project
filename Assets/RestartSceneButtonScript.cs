using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartSceneButtonScript : MonoBehaviour
{
    Button _restartButton;
    private void Awake()
    {
        _restartButton = GetComponent<Button>();
        _restartButton.onClick.AddListener(RestartScene);
    }
    private void Start()
    {
        
    }
    public void RestartScene()
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
}