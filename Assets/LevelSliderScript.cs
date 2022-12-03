using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LevelSliderScript : MonoBehaviour
{
    PublicValuesAndFunctions _publicValues;
    public Button _testButton;
    public float _duration
    {
        get
        {
            return _publicValues._levelDesignerScript._levelPassDuration;
        }
    }
    Tween _slideBetweenLevels(GameObject ObjectToMove, Vector3 PositionToMove)
    {
        return ObjectToMove.transform.DOLocalMove(PositionToMove, _duration);
    }
    Tween _slideBetweenLevels(GameObject ObjectToMove, float AmountToMove)
    {
        float MoveAmount = ObjectToMove.transform.localPosition.x - AmountToMove;
        return ObjectToMove.transform.DOLocalMoveX(MoveAmount, _duration);
    }
    private void Awake()
    {
        _publicValues = GameObject.FindWithTag("PublicValues").GetComponent<PublicValuesAndFunctions>();
        _testButton.onClick.AddListener(NextLevel);
    }
    private void Start()
    {
        
    }
    public void StartLevel()
    {

    }
    public void NextLevel()
    {
        if (_publicValues._levelCount < _publicValues._levelDesignerScript._numberOfLevels)
        {
            _publicValues._levelCount++;

            _slideBetweenLevels(_publicValues._levelSlider,
                _publicValues._activeLevelIndicator.sizeDelta.x).ForceInit();
            _slideBetweenLevels(_publicValues._levelStagePrefab,
                _publicValues._levelDesignerScript._backgroundLevelSquareWidthSize).ForceInit();
            _slideBetweenLevels(_publicValues._levelSlider,
                _publicValues._activeLevelIndicator.sizeDelta.x).
                OnStart(() =>
                { _testButton.interactable = false; }).OnComplete(() =>
                {
                    _testButton.interactable = true;
                });
        }
        else
        {
            _publicValues._levelCount = 1;

            _slideBetweenLevels(_publicValues._levelSlider,
                Vector3.zero).ForceInit();
            _slideBetweenLevels(_publicValues._levelStagePrefab,
                Vector3.zero).ForceInit();
            _slideBetweenLevels(_publicValues._levelSlider,
                Vector3.zero).
                OnStart(() =>
                { _testButton.interactable = false; }).OnComplete(() =>
                {
                    _testButton.interactable = true;
                });
        }
    }
}