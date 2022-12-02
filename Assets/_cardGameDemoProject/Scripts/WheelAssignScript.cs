using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelAssignScript : MonoBehaviour
{
    [Header("Scriptable Data")]
    public ScriptableWheel _scriptableWheel;
    [Header("Hierarchy")]
    public Image _wheel;
    public Image _wheelIndicator;
    public Transform _slotsParentTransform;
    [HideInInspector] public List<SlotContentDistributer> _slotDistributerList;

    private void Awake()
    {
        UpdateWheel();
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
        int RandomMemberIDForBomb = Random.Range(0,_slotDistributerList.Count);
        _slotDistributerList[RandomMemberIDForBomb]._scriptableWheelItems = _scriptableWheel._bomb;
        _slotDistributerList.RemoveAt(RandomMemberIDForBomb);
        if (_scriptableWheel._fillContentRandomly)
        {
            for (int i = 0; i < _slotDistributerList.Count; i++)
            {
                _slotDistributerList[i]._scriptableWheelItems =
                    _scriptableWheel._scriptableContents[Random.Range(0, _scriptableWheel._scriptableContents.Length)];
            }
        }
        else
        {
            for (int i = 0; i < _slotDistributerList.Count; i++)
            {
                if (i < _scriptableWheel._scriptableContentsWithQueue.Length)
                {
                    _slotDistributerList[i]._scriptableWheelItems =
                        _scriptableWheel._scriptableContentsWithQueue[i];
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
        if (_scriptableWheel._fillContentRandomly)
        {
            for (int i = 0; i < _slotDistributerList.Count; i++)
            {
                _slotDistributerList[i].UpdateContent();
            }
        }
        else
        {
            for (int i = 0; i < _scriptableWheel._scriptableContentsWithQueue.Length; i++)
            {
                _slotDistributerList[i].UpdateContent();
            }
        }
    }
}