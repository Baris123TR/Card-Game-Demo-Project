using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class SpinScript : MonoBehaviour
{
    public RectTransform _targetToSpin;
    public Button SpinButton;
    public Vector2 RotationAmountBetween = new Vector2(1, 3);
    public float RotationDuration;
    public float RealignToCenterDuration = 0.5f;
    [HideInInspector]
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
        SpinButton.onClick.AddListener(Spin);
    }
    public void RectRotCalcButton()
    {
        print(_targetToSpin.rotation.eulerAngles);
    }

    public void Spin()
    {
        Tween Rotate = RotateTheWheel(_targetToSpin, _rotation, RotationDurationCalculator, RotateMode.FastBeyond360, true);
        Rotate.ForceInit();
        Rotate.OnStart(() =>
        {
            SpinButton.interactable = false;
            LockSlotRotationsBool = true;
        }).OnComplete(()=>
        {
            SpinEndingSliceAligner();
        });
    }
    void SpinEndingSliceAligner()
    {
        float ZRotationOfWheel = _targetToSpin.rotation.eulerAngles.z;
        float SliceCenterAngle = 45;
        float HalfOfSliceCenterAngle = 22.5f;
        float Kalan = ZRotationOfWheel % SliceCenterAngle;
        if (ZRotationOfWheel % SliceCenterAngle != 0)
        {
            if (Kalan > HalfOfSliceCenterAngle)
            {
                ZRotationOfWheel += (SliceCenterAngle - Kalan);
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
            SpinButton.interactable = true;
        });
    }
}