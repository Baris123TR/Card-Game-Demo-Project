using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelDesignerScript : MonoBehaviour
{
    PublicValuesAndFunctions _publicValues;

    public ScriptableItemIcons _iconData;

    [HideInInspector] public int _levelCount;

    [Header("Level Design Settings")]
    public int _numberOfLevels = 20;
    public int _safeLevelDivider = 5;
    public bool _firstLevelSafeBool = true;
    public float _levelPassDuration = 0.2f;
    public float _wheelAppearDuration = 0.5f;
    public float _itemsTweeningDuration = 0.5f;

    [Space(10)]
    [Header("Background Level Stages")]
    public float _backgroundLevelSquareWidthSize = 150;
    public float _extraGapAmount;

    private void Awake()
    {
        _levelCount = 1;
        _publicValues = GameObject.FindWithTag("PublicValues").GetComponent<PublicValuesAndFunctions>();
        InstantiateLevels(_publicValues._zoomedAndMaskedLevelPrefab,
            _publicValues._levelSlider.transform, _publicValues._activeLevelIndicator.sizeDelta.x);
        InstantiateLevels(_publicValues._zoomedAndMaskedLevelPrefab,
            _publicValues._levelStagePrefab.transform, _backgroundLevelSquareWidthSize, _extraGapAmount, true);
    }
    private void Start()
    {
        
    }
    private void InstantiateLevels(GameObject ObjectToInstantiate, Transform ParentObject, 
        float RectWidthSize, float ExtraGapAmount = 0, bool ForBackground = false)
    {
        for (int i = 0; i < _numberOfLevels; i++)
        {
            int LevelID = i + 1;

            GameObject LevelObject = Instantiate(ObjectToInstantiate, ParentObject);
            Image LevelImageBackground = LevelObject.transform.GetChild(0).GetComponent<Image>();
            TextMeshProUGUI LevelIDText = LevelObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            RectTransform _rectTransform = LevelImageBackground.GetComponent<RectTransform>();
            if (!ForBackground)
            {
                _rectTransform.sizeDelta = new Vector2(RectWidthSize, _rectTransform.sizeDelta.y);
            }
            else
            {
                _rectTransform.sizeDelta = new Vector2(RectWidthSize, 70);
            }

            LevelObject.transform.localPosition = new Vector3(RectWidthSize, 0, 0) * i;
            _rectTransform.sizeDelta -= new Vector2(ExtraGapAmount, 0);
            LevelObject.name = "Level " + LevelID;

            LevelIDText.text = LevelID.ToString();

            if (!ForBackground)
            {
                if (LevelID == 1)
                {
                    if (_firstLevelSafeBool)
                    {
                        /*LevelImageBackground.sprite = _iconData._zoomedSuper;*/
                        LevelImageBackground.sprite = _iconData._normal;
                        LevelImageBackground.color = _iconData._superLevelColor;
                    }
                    else
                    {
                        print("Bool is " + _firstLevelSafeBool);
                        LevelImageBackground.sprite = _iconData._zoomedNormal;
                    }
                }
                else if (LevelID % _safeLevelDivider == 0 && LevelID != _numberOfLevels)
                {
                    /*LevelImageBackground.sprite = _iconData._zoomedSuper;*/
                    LevelImageBackground.sprite = _iconData._normal;
                    LevelImageBackground.color = _iconData._superLevelColor;
                }
                else if (LevelID == _numberOfLevels)
                {
                    LevelImageBackground.sprite = _iconData._zoomedGold;
                    LevelImageBackground.color = _iconData._goldLevelColor;
                }
                else
                {
                    LevelImageBackground.sprite = _iconData._zoomedNormal;
                }
            }
            else
            {
                if (LevelID == 1)
                {
                    if (_firstLevelSafeBool)
                    {
                        LevelImageBackground.sprite = _iconData._normal;
                        LevelImageBackground.color = _iconData._superLevelColor;
                    }
                    else
                    {
                        print("Bool is " + _firstLevelSafeBool);
                        LevelImageBackground.sprite = _iconData._normal;
                        LevelImageBackground.color = _iconData._normalLevelColor;
                    }
                }
                else if (LevelID % _safeLevelDivider == 0 && LevelID != _numberOfLevels)
                {
                    LevelImageBackground.sprite = _iconData._normal;
                    LevelImageBackground.color = _iconData._superLevelColor;
                }
                else if (LevelID == _numberOfLevels)
                {
                    LevelImageBackground.sprite = _iconData._normal;
                    LevelImageBackground.color = _iconData._goldLevelColor;
                }
                else
                {
                    LevelImageBackground.sprite = _iconData._normal;
                    LevelImageBackground.color = _iconData._normalLevelColor;
                }
            }
        }
    }
}