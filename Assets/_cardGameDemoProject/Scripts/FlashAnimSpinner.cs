using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashAnimSpinner : MonoBehaviour
{
    float _zRotation;
    PublicValuesAndFunctions _publicValues;
    private void Awake()
    {
        _publicValues = FindObjectOfType<PublicValuesAndFunctions>();
    }
    // Update is called once per frame
    void Update()
    {
        _zRotation += Time.deltaTime * _publicValues._flashSpinSpeed;
        transform.rotation = Quaternion.Euler(0,0, -_zRotation);
    }
}