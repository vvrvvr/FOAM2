
using System.Collections;
using StarterAssets;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Teleporter otherTeleport;
    [SerializeField] private float teleportCooldown = 0.3f;
    private ThirdPersonController _player;
    private bool isTeleportCooldown = false;
    
    private float _dirZPrevious;
    private float _dirXPrevious;
    private float _zDir;
    private float _xDir;
    
    private bool _isFirstDirCheck = true;

    private void Awake()
    {
        _player = ThirdPersonController.Instance;
    }

    private void OnTriggerStay(Collider other)
    {
        _zDir = Mathf.Sign(transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).z);
        _xDir = Mathf.Sign(transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).x);
        
        if (_isFirstDirCheck)
        {
            _dirZPrevious = _zDir;
            _dirXPrevious = _xDir;
            
            _isFirstDirCheck = false;
        }
        
        _player._isTeleporting = false;
        
        if ((_zDir != _dirZPrevious || _xDir !=_dirXPrevious) && !isTeleportCooldown)
        {
            _isFirstDirCheck = true;
            Teleport(other.transform);
           // isTeleported = true;
            otherTeleport.OnTravellerEnterPortal();
        }

        _dirZPrevious = _zDir;
        _dirXPrevious = _xDir;
    }

    private void Teleport(Transform obj)
    {
        // Position
         Vector3 localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(obj.position);
         localPos = new Vector3(-localPos.x, localPos.y, -localPos.z);
         obj.position = otherTeleport.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);
         
         _player._isTeleporting = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _isFirstDirCheck = true;
        _player._isTeleporting = false;
        _zDir = Mathf.Sign(transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).z);
        _xDir = Mathf.Sign(transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).x);
        _dirZPrevious = _zDir;
        _dirXPrevious = _xDir;
        
        //isTeleported = true;
    }

    public void OnTravellerEnterPortal()
    {
       // isTeleported = true;
       _isFirstDirCheck = true;
        StartCoroutine(TeleportCooldown());
        
    }
    
    void OnValidate()
    {
        if (otherTeleport != null)
        {
            otherTeleport.otherTeleport = this;
        }
    }

    private IEnumerator TeleportCooldown()
    {
        isTeleportCooldown = true;
        yield return new WaitForSeconds(teleportCooldown);
        isTeleportCooldown = false;
    }
}
