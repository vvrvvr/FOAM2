using System;
using System.Collections;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Teleporter : MonoBehaviour
{
    public Teleporter OtherTeleport;
    private ThirdPersonController _player;
    [HideInInspector] public bool isTeleported = false;
    
    private float _dirPrevious;
    private bool _isFirstDirCheck = true;

    private void Awake()
    {
        _player = ThirdPersonController.Instance;
    }

    private void OnTriggerStay(Collider other)
    {
        float zDir = Mathf.Sign(transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).z);
        
        if (_isFirstDirCheck)
        {
            _dirPrevious = zDir;
            _isFirstDirCheck = false;
        }
        
        _player._isTeleporting = false;
        
        if (zDir != _dirPrevious)
        {
            _isFirstDirCheck = true;
            Teleport(other.transform);
            isTeleported = true;
        }

        _dirPrevious = zDir;
    }

    private void Teleport(Transform obj)
    {
        // Position
         Vector3 localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(obj.position);
         localPos = new Vector3(-localPos.x, localPos.y, -localPos.z);
         obj.position = OtherTeleport.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);
         
         _player._isTeleporting = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _isFirstDirCheck = true;
        _player._isTeleporting = true;
    }

    private void OnTriggerExit(Collider other)
    {
        //other.gameObject.layer = 8;
    }
    void OnValidate()
    {
        if (OtherTeleport != null)
        {
            OtherTeleport.OtherTeleport = this;
        }
    }
}
