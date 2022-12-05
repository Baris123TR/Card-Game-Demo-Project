using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEnableScript : MonoBehaviour
{
    private void OnEnable()
    {
        print(gameObject + " destroyed.");
        Destroy(gameObject);
    }
    void Start()
    {
        
    }
}