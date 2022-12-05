using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotContentDistributer : MonoBehaviour
{
    PublicValuesAndFunctions _publicValues;

    [HideInInspector] public ScriptableWheelContent _scriptableWheelItems;

    [Header("Slot Hierarchy")]
    public Image _content;
    public Image _backgroundImage;
    public Image _backgroundBorder;
    public Image _flash;
    public Image _offerShine;
    public Image _weaponTypeBackgroundImage;
    public Image _bonusIcon;
    public TextMeshProUGUI _weaponTypeNameText;
    public TextMeshProUGUI _itemType;
    public TextMeshProUGUI _name;
    public TextMeshProUGUI _bonusAmountText;
    public ParticleSystem _particleSystem;
    public int _bonusAmount;
    [HideInInspector] public bool _particleActiveBool;

    private void Awake()
    {
        _publicValues = FindObjectOfType<PublicValuesAndFunctions>();
    }
    public void UpdateContent()
    {
        _content.sprite = _scriptableWheelItems._contentImage;
        _content.SetNativeSize();
        _itemType.text = _scriptableWheelItems._itemType.ToString();

        _backgroundBorder.color = _scriptableWheelItems._generalColor;
        _name.text = _scriptableWheelItems._name;

        _offerShine.enabled = _scriptableWheelItems._offerShineEnabled;
        _offerShine.color = _scriptableWheelItems._generalColor;
        _bonusIcon.enabled = _scriptableWheelItems._bonusAmountTextAndIconEnabled;
        _bonusIcon.sprite = _scriptableWheelItems._contentImage;
        _bonusAmountText.enabled = _scriptableWheelItems._bonusAmountTextAndIconEnabled;
        _weaponTypeBackgroundImage.enabled = _scriptableWheelItems._weaponTypeNameTexAndBackgroundEnabled;
        _weaponTypeBackgroundImage.color = _scriptableWheelItems._generalColor;
        _weaponTypeNameText.enabled = _scriptableWheelItems._weaponTypeNameTexAndBackgroundEnabled;
        _weaponTypeNameText.text = _scriptableWheelItems._weaponTypeName;

        _bonusIcon.SetNativeSize();
        _bonusAmount = _scriptableWheelItems._bonusAmountCalculated;
        _bonusAmountText.text = _bonusAmount.ToString();

        EnableAllowedParticles(_scriptableWheelItems._activateParticles);
    }
    public void EnableAllowedParticles(bool EnableBool)
    {
        if (EnableBool)
        {
            _particleSystem.Play();
        }
        else
        {
            _particleSystem.Stop();
        }
    }
}