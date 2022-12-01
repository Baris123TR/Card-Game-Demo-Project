using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PublicValuesAndFunctions : MonoBehaviour
{
    [Header("Wheel")]
    public float _flashSpinSpeed = 1;
    [Space(10)]
    [Header("Background")]
    public Color _backgroundColor;
    public Image _background;
    [Space(10)]
    [Header("Notification Panel")]
    public GameObject _notificationPanel;
    public Button[] AllButtons;
    public Button[] PanelButtons;
    private void Awake()
    {
        _notificationPanel.SetActive(false);
    }
    private void Start()
    {
        
    }
    private void OnValidate()
    {
        if (_background)
        {
            _background.color = _backgroundColor;
        }
    }
    public void ActivateNotificationPanel(bool ExitButtonFunction = true)
    {
        _notificationPanel.SetActive(ExitButtonFunction);
        AllButtons = FindObjectsOfType<Button>();
        for (int i = 0; i < AllButtons.Length; i++)
        {
            AllButtons[i].enabled = !ExitButtonFunction;
        }
        PanelButtons = _notificationPanel.GetComponentsInChildren<Button>();
        for (int i = 0; i < PanelButtons.Length; i++)
        {
            PanelButtons[i].enabled = ExitButtonFunction;
        }
    }
}