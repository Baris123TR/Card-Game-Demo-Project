using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CollectedAreasTransformAndItemAmountScript : MonoBehaviour
{
    PublicValuesAndFunctions _publicValues;

    public ScriptableCollectedItemTypeScript _itemTypeScript;
    public int _itemCounter;
    TextMeshProUGUI _itemCounterText;

    private void Awake()
    {
        DOTween.Init();
        _publicValues = GameObject.FindWithTag("PublicValues").GetComponent<PublicValuesAndFunctions>();
        _itemCounterText = GetComponentInChildren<TextMeshProUGUI>();
        _itemCounterText.text = "";
        gameObject.SetActive(false);
    }

    void Start()
    {
        
    }
    IEnumerator TestCoroutine(int TargetItemAmount)
    {
        yield return TweenStarter(500);
    }
    public IEnumerator TweenStarter(int TargetItemAmount)
    {
        Tween DoFloatTween = DOTween.To(() => _itemCounter, x => _itemCounter = x, _itemCounter + TargetItemAmount,
            _publicValues._collectedAmountIncreaseTweeningDuration);
        DoFloatTween.OnUpdate(()=>
        {
            _itemCounterText.text = _itemCounter.ToString();
        });
        yield return DoFloatTween.AsyncWaitForCompletion();
    }
}