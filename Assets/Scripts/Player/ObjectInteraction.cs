using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteraction : MonoBehaviour
{
    public float pickupDistance = 10f;
    private GameObject heldObject = null;
    [SerializeField] private float grabDistance = 5f;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private Transform heldObjectLocalPosition;
    [SerializeField] private float impulseForce = 1f;

    private Camera _camera;

    //
    [SerializeField] private Image pointerImage;
    [SerializeField] private Sprite crosshair;
    [SerializeField] private Sprite hand;

    private void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = _camera.transform.TransformDirection(Vector3.forward);
        
        if (heldObject != null && Input.GetMouseButtonDown(0)) //drop object
        {
            var objectRb = heldObject.GetComponent<Rigidbody>();
            objectRb.isKinematic = false;
            heldObject.transform.parent = null;
            objectRb.AddForce(heldObjectLocalPosition.forward * impulseForce, ForceMode.Impulse);
            
            var interfaceObj  = heldObject.GetComponent<InterfaceObject>();
            if (interfaceObj != null)
                interfaceObj.SetDefaultScale();
            
            heldObject = null;
        }
        else if (heldObject == null &&
                 Physics.Raycast(_camera.transform.position, fwd, out hit, grabDistance, layerMaskInteract.value))
        {
            pointerImage.sprite = hand;
            if (Input.GetMouseButtonDown(0))//take object
            {
                heldObject = hit.collider.gameObject;
                var objectRb = heldObject.GetComponent<Rigidbody>();
                
                var interfaceObj  = heldObject.GetComponent<InterfaceObject>();
                if (interfaceObj != null)
                    interfaceObj.SetInHandScale();
                
                objectRb.isKinematic = true;
                heldObject.transform.parent = heldObjectLocalPosition;
                heldObject.transform.localPosition = Vector3.zero;
            }
        }
        else
        {
            pointerImage.sprite = null;
        }
    }
}