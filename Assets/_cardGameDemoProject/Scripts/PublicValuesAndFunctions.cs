using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;

public class PublicValuesAndFunctions : MonoBehaviour
{
    [HideInInspector] public LevelDesignerScript _levelDesignerScript;

    [Space(10)]
    [Header("Wheel")]
    public float _flashSpinSpeed = 1;
    [Header("Background")]
    public Color _backgroundColor;
    public Image _background;
    [Header("Notification Panel")]
    public GameObject _notificationPanel;
    public Button[] AllButtons;
    public Button[] PanelButtons;

    [Space(10)]
    [Header("Transforms")]
    public GameObject _levelSlider;
    public GameObject _levelStagePrefab;

    [Space(10)]
    [Header("Prefab Data")]
    public GameObject _zoomedAndMaskedLevelPrefab;
    public GameObject _levelBarLevelPrefab;

    [Space(10)]
    [Header("Rect Transform Data")]
    public RectTransform _activeLevelIndicator;
    public RectTransform _backgroundLevelStages;

    [Space(10)]
    [Header("Icon Data")]
    public ScriptableItemIcons _scriptableIconData;
    public SpriteAtlas _iconAtlas;
    public Sprite[] _icons;
    [Space(0)]
    [Header("Icon Distribution")]
    public bool _distributeFromScriptableData;
    public GameObject[] _goldObjects;
    public GameObject[] _cashObjects;
    public GameObject[] _weaponUpgradeObjects;
    public GameObject[] _gearUpgradeObjects;
    private void PublicDistributer(GameObject[] ObjectsToDistribute, Sprite IconToDistribute = null)
    {
        if (IconToDistribute)
        {
            if (ObjectsToDistribute.Length != 0)
            {
                for (int i = 0; i < ObjectsToDistribute.Length; i++)
                {
                    if (ObjectsToDistribute[i])
                    {
                        Image ObjectsImage = ObjectsToDistribute[i].GetComponent<Image>();
                        ObjectsImage.sprite = IconToDistribute;
                        ObjectsImage.SetNativeSize();
                    }
                }
            }
        }
    }
    private void IconDistributer()
    {
        PublicDistributer(_goldObjects, _scriptableIconData._gold);
        PublicDistributer(_cashObjects, _scriptableIconData._cash);
        PublicDistributer(_weaponUpgradeObjects, _scriptableIconData._weaponUpgrade);
        PublicDistributer(_gearUpgradeObjects, _scriptableIconData._gearUpgrade);
    }
    private void Awake()
    {
        _levelDesignerScript = GetComponent<LevelDesignerScript>();
        IconDistributer();
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