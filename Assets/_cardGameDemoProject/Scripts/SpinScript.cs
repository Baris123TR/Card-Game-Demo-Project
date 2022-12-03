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
    public GameObject _choosenSlotParent;
    GameObject _canvas;

    float _sliceCenterAngle = 45;

    public float RotationDurationCalculator
    {
        get
        {
            /*if (RotationDuration > RotationAmountBetween.y || RotationDuration < RotationAmountBetween.x)
            {
                return RotationAmount;
            }
            else
            {
                return RotationDuration;
            }*/

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
        StartCoroutine(ChoosenSlotAppearIconFlyAnimation(_choosenSlot, Vector3.one * 1.5f, 6, _choosenSlotParent.transform));
        _publicValues.WheelDisapeear();
    }
    IEnumerator ChoosenSlotAppearIconFlyAnimation(GameObject ChoosenSlotObject, Vector3 TargetVector, 
        int AmountOfIconsToAppear = 1, Transform TransformParent = null)
    {
        yield return InstantiateObjectWithScaleTween(ChoosenSlotObject, TargetVector, TransformParent, true);
        GameObject IconObject = ChoosenSlotObject.GetComponentInChildren<EmptyBonusAmountIconGetScript>(true).gameObject;
        for (int i = 0; i < AmountOfIconsToAppear; i++)
        {
            yield return InstantiateObjectWithScaleTween(IconObject, IconObject.transform.localScale, _choosenSlotParent.transform, false, true);
        }
    }



    public IEnumerator InstantiateObjectWithScaleTween(GameObject ObjectToInstantiateAndScale, 
        Vector3 TargetScaleOfInstantiatedObject, Transform TransformParent, bool WaitForCompletion, bool ScatterAround = false)
    {
        print(ObjectToInstantiateAndScale + " will be instantiated and scaled.");

        GameObject InstantiatedObject = Instantiate(ObjectToInstantiateAndScale, TransformParent);

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

        if (WaitForCompletion)
        {
            yield return _publicValues.ObjectAppearAndDissappearTween(InstantiatedObject,
                TargetScaleOfInstantiatedObject).WaitForCompletion();
        }
        else
        {

            yield return _publicValues.ObjectAppearAndDissappearTween(InstantiatedObject,
                   TargetScaleOfInstantiatedObject);
        }

        if (ScatterAround)
        {
            float Random1 = Random.Range(_publicValues._scatterRangeBetween.x, _publicValues._scatterRangeBetween.y);
            float Random2 = Random.Range(_publicValues._scatterRangeBetween.x, _publicValues._scatterRangeBetween.y);
            Vector2 RandomVector = new Vector2(Random1, Random2);
            print(RandomVector);
            yield return InstantiatedObject.transform.DOLocalMove(InstantiatedObject.transform.localPosition + 
                new Vector3(RandomVector.x, RandomVector.y, 0), 
                _publicValues._levelDesignerScript._itemsTweeningDuration);
        }
    }
}