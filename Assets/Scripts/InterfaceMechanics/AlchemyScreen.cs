using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyScreen : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Interface") )
        {
            var collisionPoint = collision.contacts[0].point;
            var currentInterface = collision.gameObject.GetComponent<InterfaceObject>();
            currentInterface.onScreenPosition = collisionPoint;
            currentInterface.MoveInterfaceToScreen(0, 1, collisionPoint);
        }
    }
    
}
