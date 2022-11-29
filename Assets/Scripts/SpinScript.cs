using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class SpinScript : SpinBehaviour
{
    public bool LockSlotRotationsBool;

    private void Awake()
    {
        if (!SpinButton)
        {
            SpinButton = GameObject.FindWithTag("SpinButton").GetComponent<Button>();
        }
        SpinButton.onClick.AddListener(Spin);
    }

    public void Spin()
    {
        RotateTheWheel.ForceInit();
        RotateTheWheel.OnStart(() =>
        {
            SpinButton.interactable = false;
            LockSlotRotationsBool = true;
        }).OnComplete(()=>
        {
            LockSlotRotationsBool = false;
            SpinButton.interactable = true;
        });
    }
}

public abstract class SpinBehaviour : MonoBehaviour
{
    public Button SpinButton;
    public Vector2 RotationAmountBetween = new Vector2(1, 3);
    public float RotationDuration;
    private float RotationDurationCalculator
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
    private float RotationAmount
    {
        get
        {
            return Random.Range(RotationAmountBetween.x, RotationAmountBetween.y);
        }
    }
    private Vector3 Rotation 
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
    public Tween RotateTheWheel
    {
        get
        {
            return transform.DORotate(Rotation, RotationDurationCalculator, RotateMode.FastBeyond360).SetRelative(true);
        }
    }
}