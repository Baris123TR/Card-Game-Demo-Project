using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class SpinScript : MonoBehaviour
{
    PublicValuesAndFunctions _publicValues;



    public RectTransform _targetToSpin;
    public GameObject _wheelParent;
    public Button _spinButton;
    public Button _playButton;
    public Vector2 RotationAmountBetween = new Vector2(1, 3);
    public float RotationDuration;
    public float RealignToCenterDuration = 0.5f;
    [HideInInspector] public Vector3 _wheelScaleAtStart;
    [HideInInspector] public Vector3 _playButtonScaleAtStart;
    public GameObject _slotsParent;
    public GameObject[] _slots;
    [HideInInspector] public GameObject _choosenSlot;
    public GameObject _choosenSlotTransform;
    public GameObject _cloneIconsParent;
    GameObject _canvas;

    float _sliceCenterAngle = 45;

    public float RotationDurationCalculator
    {
        get
        {
            return RotationDuration;
        }
    }
    public bool CounterClockwise;
    [HideInInspector]
    public float RotationAmount
    {
        get
        {
            return Random.Range(RotationAmountBetween.x, RotationAmountBetween.y);
        }
    }
    [HideInInspector]
    public Vector3 _rotation
    {
        get
        {
            if (CounterClockwise)
            {
                return new Vector3(0, 0, 360 * RotationAmount);
            }
            else
            {
                return new Vector3(0, 0, -360 * RotationAmount);
            }
        }
    }
    public Tween RotateTheWheel(Transform TransformToRotate, Vector3 RotationVector, float TweenDuration, RotateMode RotateMode = RotateMode.Fast, bool SetRelativeBool = false)
    {
        return TransformToRotate.DORotate(RotationVector, TweenDuration, RotateMode).SetRelative(SetRelativeBool);
    }
    [HideInInspector] public bool LockSlotRotationsBool;
    private void Awake()
    {
        _publicValues = GameObject.FindWithTag("PublicValues").GetComponent<PublicValuesAndFunctions>();

        if (!_canvas)
        {
            if (GameObject.FindWithTag("Canvas"))
            {
                _canvas = GameObject.FindWithTag("Canvas");
            }
            else
            {
                print("Canvas can't be found.");
            }
        }

        if (!_spinButton)
        {
            _spinButton = GameObject.FindWithTag("SpinButton").GetComponent<Button>();
        }

        _slots = new GameObject[_slotsParent.transform.childCount];
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i] = transform.GetChild(0).GetChild(i).gameObject;
        }

        _spinButton.onClick.AddListener(Spin);

        GetAndSetScaleInfo();
    }
    private void Start()
    {
        
    }
    private void GetAndSetScaleInfo()
    {
        _wheelScaleAtStart = _wheelParent.transform.localScale;
        _playButtonScaleAtStart = _playButton.transform.localScale;

        _wheelParent.transform.localScale = Vector3.zero;
        _playButton.transform.localScale = _playButtonScaleAtStart;
    }
    public void Spin()
    {
        Tween Rotate = RotateTheWheel(_targetToSpin, _rotation, RotationDurationCalculator, RotateMode.FastBeyond360, true);
        Rotate.ForceInit();
        Rotate.OnStart(() =>
        {
            _spinButton.interactable = false;
            LockSlotRotationsBool = true;
        }).OnComplete(()=>
        {
            SpinEndingSliceAligner();
        });
    }
    void SpinEndingSliceAligner()
    {
        float ZRotationOfWheel = _targetToSpin.rotation.eulerAngles.z;
        float HalfOfSliceCenterAngle = 22.5f;
        float Kalan = ZRotationOfWheel % _sliceCenterAngle;
        if (ZRotationOfWheel % _sliceCenterAngle != 0)
        {
            if (Kalan > HalfOfSliceCenterAngle)
            {
                ZRotationOfWheel += (_sliceCenterAngle - Kalan);
            }
            else
            {
                ZRotationOfWheel -= Kalan;
            }
        }
        Vector3 NewRotationVector = new Vector3(0, 0, ZRotationOfWheel);
        Tween AlignToSliceCenter = RotateTheWheel(_targetToSpin, NewRotationVector, RealignToCenterDuration);
        AlignToSliceCenter.ForceInit();
        AlignToSliceCenter.OnComplete(()=>
        {
            LockSlotRotationsBool = false;
            GetTheSelectedSlot();
        });
    }
    void GetTheSelectedSlot()
    {
        float RotationAngle = transform.GetComponent<RectTransform>().rotation.eulerAngles.z;
        //print(RotationAngle);
        int RotationAngleInt = Mathf.RoundToInt(RotationAngle);
        //print(RotationAngleInt);
        int SlotNumber = (RotationAngleInt / 45) % _slots.Length;
        //print(SlotNumber);
        _choosenSlot = _slots[SlotNumber];
        //print(_choosenSlot);
        StartCoroutine(CardAppearIconFlyAnimation(_choosenSlot, Vector3.one * 1.5f, 6, _choosenSlotTransform.transform));
        StartCoroutine(_publicValues.WheelDisapeear());
    }

    public GameObject[] _iconsSpawned;

    IEnumerator CardAppearIconFlyAnimation(GameObject ChoosenSlotObject, Vector3 TargetVector, 
        int AmountOfIconsToAppear = 1, Transform TransformParent = null)
    {
        GameObject InstantiatedCardObject = Instantiate(ChoosenSlotObject, TransformParent);

        yield return _publicValues.InstantiateObjectWithScaleTween(InstantiatedCardObject, TargetVector, TransformParent, default, true).WaitForCompletion();
        GameObject IconObject = ChoosenSlotObject.GetComponentInChildren<EmptyBonusAmountIconGetScript>(true).gameObject;

        for (int i = 1; i <= AmountOfIconsToAppear; i++)
        {
            if (i == AmountOfIconsToAppear)
            {
                yield return _publicValues.InstantiateObjectWithScaleTween(IconObject, 
                    IconObject.transform.localScale, _cloneIconsParent.transform, true).WaitForCompletion();
            }
            else
            {
                yield return _publicValues.InstantiateObjectWithScaleTween(IconObject, 
                    IconObject.transform.localScale, _cloneIconsParent.transform, true);
            }
        }

        _iconsSpawned = new GameObject[_cloneIconsParent.transform.childCount];
        for (int i = 0; i < _iconsSpawned.Length; i++)
        {
            _iconsSpawned[i] = _cloneIconsParent.transform.GetChild(i).gameObject;
        }

        StartCoroutine(GetItemType(ChoosenSlotObject, InstantiatedCardObject));
    }
    IEnumerator GetItemType(GameObject ChoosenSlotObject, GameObject InstantiatedChoosenSlot = null)
    {
        CollectedAreasTransformAndItemAmountScript[] _possessions = FindObjectsOfType<CollectedAreasTransformAndItemAmountScript>(true);
        var ChoosenSlotObjectType = ChoosenSlotObject.GetComponent<SlotContentDistributer>()._scriptableWheelItems._itemType;
        int ChoosenSlotBonusAmount = ChoosenSlotObject.GetComponent<SlotContentDistributer>()._bonusAmount;
        GameObject TargetTransformGameObject = null;
        for (int i = 0; i < _possessions.Length; i++)
        {
            if (_possessions[i]._itemTypeScript._itemType == ChoosenSlotObjectType)
            {
                TargetTransformGameObject = _possessions[i].gameObject;
            }
        }

        _instantiatedChoosenSlot = InstantiatedChoosenSlot;

        if (ChoosenSlotObjectType == ItemTypeIncludedScriptableObject.ItemType.Bomb)
        {
            _publicValues.BombConditionMenu();

        }
        else
        {
            TargetTransformGameObject.SetActive(true);

            CollectedAreasTransformAndItemAmountScript TargetScriptToIncreaseBonus = TargetTransformGameObject.
               GetComponent<CollectedAreasTransformAndItemAmountScript>();

            yield return MoveItemsToTargetAreaAndDestroy(_iconsSpawned, TargetTransformGameObject.gameObject);

            yield return TargetScriptToIncreaseBonus.TweenStarter(ChoosenSlotBonusAmount);

            yield return NextLevelChooserAndStarter(false);
        }
    }
    private GameObject _instantiatedChoosenSlot;
    public IEnumerator NextLevelChooserAndStarter(bool BombRevive)
    {
        _publicValues._levelSliderScript.NextLevel();

        yield return _publicValues._levelDesignerScript.WheelLoader();

        yield return _publicValues.ObjectAppearAndDissappearTween(_instantiatedChoosenSlot, Vector3.zero).WaitForCompletion();

        if (BombRevive)
        {
            Destroy(_instantiatedChoosenSlot);
        }
    }
    IEnumerator MoveItemsToTargetAreaAndDestroy(GameObject[] ItemsToMove, GameObject TransformObject)
    {
        Sequence MoveSequence = DOTween.Sequence();
        for (int i = 0; i < ItemsToMove.Length; i++)
        {
            if (i == 0)
            {
                MoveSequence.Append(MoveObjectToDesiredParentTween(ItemsToMove[i], TransformObject, default, true));
            }
            else
            {
                MoveSequence.Join(MoveObjectToDesiredParentTween(ItemsToMove[i], TransformObject, default, true));
            }
        }
        yield return MoveSequence.WaitForCompletion();
    }

    

    public Tween MoveObjectToDesiredParentTween(GameObject ObjectToMove, GameObject DesiredParentObject, bool LocalMoveToZero = true, bool DestroyAtEnding = false)
    {
        if (LocalMoveToZero)
        {
            ObjectToMove.transform.parent = DesiredParentObject.transform;
            return ObjectToMove.transform.DOLocalMove(Vector3.zero, _publicValues._itemsTweeningDuration).OnComplete(()=>
            {
                if (DestroyAtEnding)
                {
                    Destroy(ObjectToMove);
                }
            });
        }
        else
        {
            return ObjectToMove.transform.DOMove(DesiredParentObject.transform.position, _publicValues._itemsTweeningDuration).OnComplete(() =>
            {
                if (DestroyAtEnding)
                {
                    Destroy(ObjectToMove);
                }
            });
        }
    }
}