using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushSignal : MonoBehaviour
{
    public bool isTouched = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerCube")
        {
            isTouched = true;
            //Debug.Log("touched");
        }
    }
}