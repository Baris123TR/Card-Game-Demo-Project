using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelAssignScript : MonoBehaviour
{
    [HideInInspector] public ScriptableWheel _scriptableWheelData;

    PublicValuesAndFunctions _publicValues;

    [Header("Hierarchy")]
    public Image _wheel;
    public Image _wheelIndicator;
    public Transform _slotsParentTransform;
    [HideInInspector] public List<SlotContentDistributer> _slotDistributerList;

    private bool _ifEditor;

    private void Awake()
    {
        _ifEditor = false;
        _publicValues = GameObject.FindWithTag("PublicValues").GetComponent<PublicValuesAndFunctions>();
    }
    private void Start()
    {
        
    }
    public void GetDistributersAndFillLists()
    {
        _slotDistributerList.Clear();

        SlotContentDistributer[] ListArray = GetComponentsInChildren<SlotContentDistributer>();
        int ListAmount = ListArray.Length;
        for (int i = 0; i < ListAmount; i++)
        {
            _slotDistributerList.Add(ListArray[i]);
        }
    }
    public void FillTheSlotsWithContent()
    {
        if (_scriptableWheelData._type == ScriptableWheel.Type.Bronze)
        {
            
            int RandomMemberIDForBomb = Random.Range(0, _slotDistributerList.Count);
            _slotDistributerList[RandomMemberIDForBomb]._scriptableWheelItems = _scriptableWheelData._bomb;
            _slotDistributerList.RemoveAt(RandomMemberIDForBomb);

            _wheel.sprite = _scriptableWheelData._wheelTypeData._bronzeWheel;
            _wheelIndicator.sprite = _scriptableWheelData._wheelTypeData._bronzeIndicator;
        }
        else if (_scriptableWheelData._type == ScriptableWheel.Type.Silver)
        {
            _wheel.sprite = _scriptableWheelData._wheelTypeData._silverWheel;
            _wheelIndicator.sprite = _scriptableWheelData._wheelTypeData._silverIndicator;
        }
        else
        {
            _wheel.sprite = _scriptableWheelData._wheelTypeData._goldWheel;
            _wheelIndicator.sprite = _scriptableWheelData._wheelTypeData._goldIndicator;
        }

#if UNITY_EDITOR
        _ifEditor = true;
#endif
        if (_ifEditor)
        {
            if (_publicValues._bombEverySlot)
            {
                for (int i = 0; i < _slotDistributerList.Count; i++)
                {
                    _slotDistributerList[i]._scriptableWheelItems =
                        _scriptableWheelData._bomb;
                }
            }
            else
            {
                if (_scriptableWheelData._fillContentRandomly)
                {
                    for (int i = 0; i < _slotDistributerList.Count; i++)
                    {
                        _slotDistributerList[i]._scriptableWheelItems =
                            _scriptableWheelData._scriptableContents[Random.Range(0, _scriptableWheelData._scriptableContents.Length)];
                    }
                }
                else
                {
                    for (int i = 0; i < _slotDistributerList.Count; i++)
                    {
                        if (i < _scriptableWheelData._scriptableContentsWithQueue.Length)
                        {
                            _slotDistributerList[i]._scriptableWheelItems =
                                _scriptableWheelData._scriptableContentsWithQueue[i];
                        }
                    }
                }
            }
        }
        else
        {
            if (_scriptableWheelData._fillContentRandomly)
            {
                for (int i = 0; i < _slotDistributerList.Count; i++)
                {
                    _slotDistributerList[i]._scriptableWheelItems =
                        _scriptableWheelData._scriptableContents[Random.Range(0, _scriptableWheelData._scriptableContents.Length)];
                }
            }
            else
            {
                for (int i = 0; i < _slotDistributerList.Count; i++)
                {
                    if (i < _scriptableWheelData._scriptableContentsWithQueue.Length)
                    {
                        _slotDistributerList[i]._scriptableWheelItems =
                            _scriptableWheelData._scriptableContentsWithQueue[i];
                    }
                }
            }
        }
    }
    private void FillTheListsAccordingly()
    {
        GetDistributersAndFillLists();
        FillTheSlotsWithContent();
        GetDistributersAndFillLists();
    }
    public void UpdateWheel()
    {
        FillTheListsAccordingly();
        if (_scriptableWheelData._fillContentRandomly)
        {
            for (int i = 0; i < _slotDistributerList.Count; i++)
            {
                _slotDistributerList[i].UpdateContent();
            }
        }
        else
        {
            for (int i = 0; i < _scriptableWheelData._scriptableContentsWithQueue.Length; i++)
            {
                _slotDistributerList[i].UpdateContent();
            }
        }
    }
}