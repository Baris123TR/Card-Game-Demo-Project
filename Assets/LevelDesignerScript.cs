using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class LevelDesignerScript : MonoBehaviour
{
    PublicValuesAndFunctions _publicValues;

    public ScriptableItemIcons _collectionIconData;

    [Header("Level Design Settings")]
    [Header("Automatic Level Load")]

    public bool _designLevelContentRandomly;
    public int _numberOfLevels = 20;
    public int _safeLevelDivider = 5;
    public bool _firstLevelSafeBool = true;
    public ScriptableWheel _normalLevel;
    public ScriptableWheel _safeLevel;
    public ScriptableWheel _goldLevel;
    [Header("Manuel Level Design")]
    public ScriptableWheel[] _wheelLevels;

    [Space(20)]
    [Header("Background Level Stages")]
    public float _backgroundLevelSquareWidthSize = 150;
    public float _extraGapAmount;
    [Space(20)]
    [Header("Tweening Durations")]
    public float _levelPassDuration = 0.2f;
    public float _wheelAppearDuration = 0.5f;
    public float _itemsTweeningDuration = 0.5f;

    private void Awake()
    {
        _publicValues = GameObject.FindWithTag("PublicValues").GetComponent<PublicValuesAndFunctions>();
        _publicValues._levelCount = 1;


        if (_designLevelContentRandomly)
        {
            InstantiateLevels(_publicValues._zoomedAndMaskedLevelPrefab,
               _publicValues._levelSlider.transform, _publicValues._activeLevelIndicator.sizeDelta.x);
            InstantiateLevels(_publicValues._zoomedAndMaskedLevelPrefab,
                _publicValues._levelStagePrefab.transform, _backgroundLevelSquareWidthSize, _extraGapAmount, true);
        }    }
    private void Start()
    {
        
    }
    private void SetScalesForGameStart()
    {
        _publicValues._spinScript._wheelParent
            .transform.localScale = Vector3.zero;
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
                        LevelImageBackground.sprite = _collectionIconData._normal;
                        LevelImageBackground.color = _collectionIconData._superLevelColor;
                    }
                    else
                    {
                        LevelImageBackground.sprite = _collectionIconData._zoomedNormal;
                    }
                }
                else if (LevelID % _safeLevelDivider == 0 && LevelID != _numberOfLevels)
                {
                    /*LevelImageBackground.sprite = _iconData._zoomedSuper;*/
                    LevelImageBackground.sprite = _collectionIconData._normal;
                    LevelImageBackground.color = _collectionIconData._superLevelColor;
                }
                else if (LevelID == _numberOfLevels)
                {
                    LevelImageBackground.sprite = _collectionIconData._zoomedGold;
                    LevelImageBackground.color = _collectionIconData._goldLevelColor;
                }
                else
                {
                    LevelImageBackground.sprite = _collectionIconData._zoomedNormal;
                }
            }
            else
            {
                if (LevelID == 1)
                {
                    if (_firstLevelSafeBool)
                    {
                        LevelImageBackground.sprite = _collectionIconData._normal;
                        LevelImageBackground.color = _collectionIconData._superLevelColor;
                    }
                    else
                    {
                        LevelImageBackground.sprite = _collectionIconData._normal;
                        LevelImageBackground.color = _collectionIconData._normalLevelColor;
                    }
                }
                else if (LevelID % _safeLevelDivider == 0 && LevelID != _numberOfLevels)
                {
                    LevelImageBackground.sprite = _collectionIconData._normal;
                    LevelImageBackground.color = _collectionIconData._superLevelColor;
                }
                else if (LevelID == _numberOfLevels)
                {
                    LevelImageBackground.sprite = _collectionIconData._normal;
                    LevelImageBackground.color = _collectionIconData._goldLevelColor;
                }
                else
                {
                    LevelImageBackground.sprite = _collectionIconData._normal;
                    LevelImageBackground.color = _collectionIconData._normalLevelColor;
                }
            }
        }
    }

    public void WheelLoader()
    {
        if (_publicValues._levelCount == 1)
        {
            if (_firstLevelSafeBool)
            {
                _publicValues._wheelAssignScript._scriptableWheelData = _publicValues._levelDesignerScript._safeLevel;
            }
            else
            {
                _publicValues._wheelAssignScript._scriptableWheelData = _publicValues._levelDesignerScript._normalLevel;
            }
        }
        else if (_publicValues._levelCount % _safeLevelDivider == 0 && _publicValues._levelCount != _numberOfLevels)
        {
            _publicValues._wheelAssignScript._scriptableWheelData = _publicValues._levelDesignerScript._safeLevel;
        }
        else if (_publicValues._levelCount == _numberOfLevels)
        {
            _publicValues._wheelAssignScript._scriptableWheelData = _publicValues._levelDesignerScript._goldLevel;
        }
        else
        {
            _publicValues._wheelAssignScript._scriptableWheelData = _publicValues._levelDesignerScript._normalLevel;
        }
        _publicValues._wheelAssignScript.UpdateWheel();
        Tween WheelLoadTween = _publicValues.ObjectAppearAndDissappearTween(_publicValues._spinScript._wheelParent,
            _publicValues._spinScript._wheelScaleAtStart);
        WheelLoadTween.ForceInit();
        WheelLoadTween.OnComplete(()=> { _publicValues._spinScript._spinButton.interactable = true; });
    }
}