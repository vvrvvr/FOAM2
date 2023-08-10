using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteraction : MonoBehaviour
{
    public float pickupDistance = 10f;
    public GameObject heldObject = null;
    [SerializeField] private float grabDistance = 5f;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private Transform heldObjectLocalPosition;
    [SerializeField] private float impulseForce = 1f;
    [SerializeField] private GameObject cursor;
    private float defaultCharacterRadius;
    private CharacterController _characterController;

    private Camera _camera;

    //
    [SerializeField] private Image pointerImage;
    [SerializeField] private Sprite crosshair;
    [SerializeField] private Sprite hand;

    private void Start()
    {
        _camera = Camera.main;
        _characterController = GetComponent<CharacterController>();
        defaultCharacterRadius = _characterController.radius;
        cursor.SetActive(true);
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = _camera.transform.TransformDirection(Vector3.forward);

        if (heldObject != null && Input.GetMouseButtonDown(0)) //drop object
        {
            heldObject.transform.parent = null;
            heldObject.GetComponent<InterfaceObject>().isDropped(heldObjectLocalPosition.forward * impulseForce, 1, 4f);
            heldObject = null;
            _characterController.radius = defaultCharacterRadius;
            cursor.SetActive(true);
        }
        else if (heldObject == null &&
                 Physics.Raycast(_camera.transform.position, fwd, out hit, grabDistance, layerMaskInteract.value))
        {
            pointerImage.sprite = hand;
            if (Input.GetMouseButtonDown(0)) //take object
            {
                heldObject = hit.collider.gameObject;
                var heldObjectInterface = heldObject.GetComponent<InterfaceObject>();
                if (heldObjectInterface.CanTake)
                {
                    heldObjectInterface.IsTaken();
                    EventManager.OnItemHeld.Invoke();
                    heldObject.transform.parent = heldObjectLocalPosition;
                    heldObject.transform.localPosition = Vector3.zero;
                    _characterController.radius = 1f;
                    cursor.SetActive(false);
                }
            }
        }
        else
        {
            pointerImage.sprite = null;
        }
    }
}