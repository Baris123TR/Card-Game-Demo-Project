using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotContentDistributer : MonoBehaviour
{
    PublicValues _publicValues;

    [Header("Scriptable Data")]
    public WheelContent _wheelItems;

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
        _publicValues = FindObjectOfType<PublicValues>();

        UpdateContent();
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
    }

    public void UpdateContent()
    {
        ClearContent();

        _content.sprite = _wheelItems._contentImage;
        _content.SetNativeSize();
        _itemType.text = _wheelItems._itemType.ToString();

        if (_wheelItems._itemType == WheelContent.ItemType.Bomb)
        {
            _generalColor = Color.red;
            _offerShine.enabled = true;
            _offerShine.color = _generalColor;
            _name.text = _wheelItems._itemType.ToString();
            _backgroundImage.color = _generalColor;
            _flash.color = _generalColor;
        }
        else
        {
            if (_wheelItems._itemType == WheelContent.ItemType.Gold)
            {
                _generalColor = Color.yellow;
                _name.text = _wheelItems._itemType.ToString();
            }

            if (_wheelItems._itemType != WheelContent.ItemType.Cash)
            {
                _generalColor = Color.green;
                _name.text = _wheelItems._itemType.ToString();
            }

            if (_wheelItems._particles)
            {
                _particleSystem.Play();
            }
            if (_wheelItems._itemType == WheelContent.ItemType.WeaponPoint)
            {
                if (_wheelItems._rarity == WheelContent.Rarity.Common)
                {
                    _generalColor = Color.grey;
                }
                else if (_wheelItems._rarity == WheelContent.Rarity.Rare)
                {
                    _generalColor = Color.blue;
                }
                else if (_wheelItems._rarity == WheelContent.Rarity.Epic)
                {
                    _generalColor = Color.magenta;
                }
                else if (_wheelItems._rarity == WheelContent.Rarity.Legendary)
                {
                    _generalColor = Color.HSVToRGB(1, 0, 0);
                }
                else
                {
                    _generalColor.a = 0;
                }
                _name.text = _wheelItems.name;

                if (_wheelItems._offerShine)
                {
                    _offerShine.enabled = true;
                    _offerShine.color = _generalColor;
                }
                _weaponTypeBackgroundImage.color = _generalColor;
                _weaponTypeName.text = _wheelItems._weaponTypeName;
                if (_wheelItems._particles)
                {
                    _particleSystem.Play();
                }
            }

            _backgroundBorder.color = _generalColor;

            _bonusAmount = _wheelItems._bonusAmountCalculated;
        }
    }
}