using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlreadyPossessed : MonoBehaviour
{
    PublicValuesAndFunctions _publicValues;
    public CollectedAreasTransformAndItemAmountScript _relatedCollectedAreaScript;
    [HideInInspector] public int _alreadyPossessedAmountOfItems;
    TextMeshProUGUI _alreadyPossessedAmountOfItemsText;

    private void Awake()
    {
        _publicValues = GameObject.FindWithTag("PublicValues").GetComponent<PublicValuesAndFunctions>();
        _alreadyPossessedAmountOfItemsText = GetComponentInChildren<TextMeshProUGUI>();
        _alreadyPossessedAmountOfItemsText.text = _alreadyPossessedAmountOfItems.ToString();

        //TODO: Write a save and load system that loads the info of amount of items already possessed.
    }
    void Start()
    {
        
    }
}