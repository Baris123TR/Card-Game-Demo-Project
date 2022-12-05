using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;
using UnityEngine.Rendering;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PublicValuesAndFunctions : MonoBehaviour
{
    [HideInInspector] public LevelDesignerScript _levelDesignerScript;
    [HideInInspector] public WheelAssignScript _wheelAssignScript;
    [HideInInspector] public LevelSliderScript _levelSliderScript;

    [HideInInspector] public int _levelCount;

    [Header("For Testing Purposes\n(Only On Editor)")]
    public bool _bombEverySlot;

    [Header("Level Loader")]
    public SpinScript _spinScript;

    [Header("UI Camera")]
    public Camera _parcileCamera;

    [Space(10)]
    [Header("Wheel")]
    public float _flashSpinSpeed = 1;
    [Header("Background")]
    public Color _backgroundColor;
    public Image _background;
    [Header("Notification Panel")]
    public GameObject _notificationPanelParent;
    public Button[] AllButtons;
    public Button[] PanelButtons;

    [Space(10)]
    [Header("Transforms")]
    public GameObject _levelSlider;
    public GameObject _levelStagePrefab;

    [Space(10)]
    [Header("Prefab Data")]
    public GameObject _zoomedAndMaskedLevelPrefab;
    public GameObject _levelBarLevelPrefab;

    [Space(10)]
    [Header("Rect Transform Data")]
    public RectTransform _activeLevelIndicator;
    public RectTransform _backgroundLevelStages;

    [Space(10)]
    [Header("Icon Data")]
    public ScriptableItemIcons _scriptableIconData;
    public SpriteAtlas _iconAtlas;
    public Sprite[] _icons;
    [Space(0)]
    [Header("Icon Distribution")]
    public bool _distributeFromScriptableData;
    public GameObject[] _goldObjects;
    public GameObject[] _cashObjects;
    public GameObject[] _weaponUpgradeObjects;
    public GameObject[] _gearUpgradeObjects;

    [Header("Icon Tween")]
    public Vector2 _scatterRangeBetween = new Vector2(-200, 200);

    [Space(10)]
    [Header("DOTween Related")]
    public Vector2 _tweenCapacity = new Vector2(1000, 1000);
    [Space(20)]
    [Header("Tweening Durations")]
    public float _levelPassDuration = 0.2f;
    public float _wheelAppearDuration = 0.5f;
    public float _itemsTweeningDuration = 0.5f;
    public float _collectedAmountIncreaseTweeningDuration = 2;
    private void PublicDistributer(GameObject[] ObjectsToDistribute, Sprite IconToDistribute = null)
    {
        if (IconToDistribute)
        {
            if (ObjectsToDistribute.Length != 0)
            {
                for (int i = 0; i < ObjectsToDistribute.Length; i++)
                {
                    if (ObjectsToDistribute[i])
                    {
                        Image ObjectsImage = ObjectsToDistribute[i].GetComponent<Image>();
                        ObjectsImage.sprite = IconToDistribute;
                        ObjectsImage.SetNativeSize();
                    }
                }
            }
        }
    }
    private void IconDistributer()
    {
        PublicDistributer(_goldObjects, _scriptableIconData._gold);
        PublicDistributer(_cashObjects, _scriptableIconData._cash);
        PublicDistributer(_weaponUpgradeObjects, _scriptableIconData._weaponUpgrade);
        PublicDistributer(_gearUpgradeObjects, _scriptableIconData._gearUpgrade);
    }
    private void Awake()
    {
        _spinScript._spinButton.interactable = false;

        _wheelAssignScript = GameObject.FindWithTag("Wheel").GetComponent<WheelAssignScript>();
        _levelSliderScript = _levelSlider.transform.GetComponent<LevelSliderScript>();
        _levelDesignerScript = GetComponent<LevelDesignerScript>();
        IconDistributer();
        _spinScript._playButton.onClick.AddListener(PlayButtonFunction);
    }
    private void Start()
    {

    }
    public Tween ObjectAppearAndDissappearTween(GameObject ObjectToScale, Vector3 TargetScale)
    {
        /*print(ObjectToScale + " will be scaled to: " + TargetScale);*/
        return ObjectToScale.transform.DOScale(TargetScale, _wheelAppearDuration);
    }
    public IEnumerator ObjectAppearAndDissappear(GameObject ObjectToScale, Vector3 TargetScale)
    {
         yield return ObjectAppearAndDissappearTween(ObjectToScale, TargetScale);
    }

    public void PlayButtonFunction()
    {
        StartCoroutine(ObjectAppearAndDissappear(_spinScript._playButton.gameObject, Vector3.zero));
        _spinScript._playButton.interactable = false;
        StartCoroutine(_levelDesignerScript.WheelLoader());
    }

    //For easy access
    public IEnumerator WheelApeear()
    {
        yield return ObjectAppearAndDissappearTween(_spinScript._wheelParent, _spinScript._wheelScaleAtStart);
    }
    public IEnumerator WheelDisapeear()
    {
        yield return ObjectAppearAndDissappearTween(_spinScript._wheelParent, Vector3.zero);
    }

    private void OnValidate()
    {
        if (_background)
        {
            _background.color = _backgroundColor;
        }
    }
    public void ActivateNotificationPanel(bool ExitButtonFunction)
    {
        _parcileCamera.enabled = !ExitButtonFunction;

        AllButtons = FindObjectsOfType<Button>();
        for (int i = 0; i < AllButtons.Length; i++)
        {
            AllButtons[i].enabled = !ExitButtonFunction;
        }

    }
    public Tween InstantiateObjectWithScaleTween(GameObject ObjectToInstantiateAndScale,
       Vector3 TargetScaleOfInstantiatedObject, Transform TransformParent, bool ScatterAround = false, bool OnlyScale = false)
    {
        GameObject InstantiatedObject;

        if (OnlyScale)
        {
            InstantiatedObject = ObjectToInstantiateAndScale;
        }
        else
        {
            InstantiatedObject = Instantiate(ObjectToInstantiateAndScale, TransformParent);
        }

        if (InstantiatedObject.GetComponent<SlotRotationFixer>())
        {
            Destroy(InstantiatedObject.GetComponent<SlotRotationFixer>());
        }
        if (InstantiatedObject.transform.
            GetComponentInChildren<EmptyScriptForFindingHiddenBackground>(true))
        {
            GameObject BackgroundParent;
            BackgroundParent = InstantiatedObject.transform.
               GetComponentInChildren<EmptyScriptForFindingHiddenBackground>(true).gameObject;
            BackgroundParent.SetActive(true);
        }

        InstantiatedObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        InstantiatedObject.transform.localScale = Vector3.zero;

        if (ScatterAround)
        {
            float Random1 = Random.Range(_scatterRangeBetween.x, _scatterRangeBetween.y);
            float Random2 = Random.Range(_scatterRangeBetween.x, _scatterRangeBetween.y);
            Vector2 RandomVector = new Vector2(Random1, Random2);
            //print(RandomVector); 
            
            ObjectAppearAndDissappearTween(InstantiatedObject,
                 TargetScaleOfInstantiatedObject);

            return InstantiatedObject.transform.DOLocalMove(InstantiatedObject.transform.localPosition +
                new Vector3(RandomVector.x, RandomVector.y, 0),
                _itemsTweeningDuration);
        }
        else
        {
            return ObjectAppearAndDissappearTween(InstantiatedObject,
                TargetScaleOfInstantiatedObject);
        }
    }
    public GameObject _exitButtonPrefab;
    public GameObject _bombConditionMenuPrefab;
    private string _notificationPanelString = "NotificationPanel";
    private void InstantiateAndTag(GameObject ObjToInst)
    {
        ActivateNotificationPanel(true);
        GameObject InstantiatedObj = Instantiate(ObjToInst, _notificationPanelParent.transform);
        InstantiatedObj.tag = _notificationPanelString;
    }
    private void DestroyNotifications()
    {
        ActivateNotificationPanel(false);
        Destroy(GameObject.FindWithTag(_notificationPanelString));
    }
    public void ExitButton()
    {
        InstantiateAndTag(_exitButtonPrefab);
    }

    public void RestartScene()
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }

    public void BackToGame(bool PayToBackFromBomb = false)
    {
        if (PayToBackFromBomb)
        {
            if (true)
            {
                //TODO: If enough coins paid, back to game. Directly back to game for now.
                StartCoroutine(_spinScript.NextLevelChooserAndStarter(true));
                DestroyNotifications();
            }
        }
        else
        {
            DestroyNotifications();
        }
    }

    public void BombConditionMenu()
    {
        InstantiateAndTag(_bombConditionMenuPrefab);
    }
    public void SaveTheEarnings()
    {
        print("Save method called.");
    }
    public void SaveAndExit()
    {
        SaveTheEarnings();
        RestartScene();
    }
}