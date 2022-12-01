using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotContentDistributer : MonoBehaviour
{
    PublicValuesAndFunctions _publicValues;

    [Header("Scriptable Data")]
    public ScriptableWheelContent _scriptableWheelItems;

    [Header("Slot Hierarchy")]
    public Image _content;
    public Image _backgroundImage;
    public Image _backgroundBorder;
    public Image _flash;
    public Image _offerShine;
    public Image _weaponTypeBackgroundImage;
    public TextMeshProUGUI _weaponTypeName;
    public TextMeshProUGUI _itemType;
    public TextMeshProUGUI _name;
    [HideInInspector] public Color _generalColor;
    public ParticleSystem _particleSystem;
    public int _bonusAmount;
    private void Awake()
    {
        _publicValues = FindObjectOfType<PublicValuesAndFunctions>();
    }
    
    private void BombFunction()
    {
        print("Bomb selected.");
    }

    public void ClearContent()
    {
        _content.sprite = null;
        _backgroundImage.sprite = null;
        _backgroundBorder.sprite = null;
        _offerShine.enabled = false;
        _weaponTypeBackgroundImage.sprite = null;
        _weaponTypeName.text = null;
        _name.text = null;
        _particleSystem.Stop();
        _bonusAmount = 0;
        _flash.color = Color.white;
    }

    public void UpdateContent()
    {
        ClearContent();

        _content.sprite = _scriptableWheelItems._contentImage;
        _content.SetNativeSize();
        _itemType.text = _scriptableWheelItems._itemType.ToString();

        if (_scriptableWheelItems._itemType == ScriptableWheelContent.ItemType.Bomb)
        {
            _generalColor = Color.red;
            _offerShine.enabled = true;
            _offerShine.color = _generalColor;
            _name.text = _scriptableWheelItems._itemType.ToString();
            _backgroundImage.color = _generalColor;
            _flash.color = _generalColor;
        }
        else
        {
            if (_scriptableWheelItems._itemType == ScriptableWheelContent.ItemType.Gold)
            {
                _generalColor = Color.yellow;
                _name.text = _scriptableWheelItems._itemType.ToString();
            }

            if (_scriptableWheelItems._itemType != ScriptableWheelContent.ItemType.Cash)
            {
                _generalColor = Color.green;
                _name.text = _scriptableWheelItems._itemType.ToString();
            }

            if (_scriptableWheelItems._particles)
            {
                _particleSystem.Play();
            }
            if (_scriptableWheelItems._itemType == ScriptableWheelContent.ItemType.WeaponPoint)
            {
                if (_scriptableWheelItems._rarity == ScriptableWheelContent.Rarity.Common)
                {
                    _generalColor = Color.grey;
                }
                else if (_scriptableWheelItems._rarity == ScriptableWheelContent.Rarity.Rare)
                {
                    _generalColor = Color.blue;
                }
                else if (_scriptableWheelItems._rarity == ScriptableWheelContent.Rarity.Epic)
                {
                    _generalColor = Color.magenta;
                }
                else if (_scriptableWheelItems._rarity == ScriptableWheelContent.Rarity.Legendary)
                {
                    _generalColor = new Vector4(1, 0.4f, 0, 1);
                }
                else
                {
                    _generalColor.a = 0;
                }
                _name.text = _scriptableWheelItems.name;

                if (_scriptableWheelItems._offerShine)
                {
                    _offerShine.enabled = true;
                    _offerShine.color = _generalColor;
                }
                _weaponTypeBackgroundImage.color = _generalColor;
                _weaponTypeName.text = _scriptableWheelItems._weaponTypeName;
                if (_scriptableWheelItems._particles)
                {
                    _particleSystem.Play();
                }
            }

            _backgroundBorder.color = _generalColor;

            _bonusAmount = _scriptableWheelItems._bonusAmountCalculated;
        }
    }
}