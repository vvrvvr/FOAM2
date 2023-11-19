using UnityEngine;

public class RotateToMatchTarget : MonoBehaviour
{
    public Transform MainTarget;
    private bool _isMainTargetNotNull;

    private void Start()
    {
        _isMainTargetNotNull = MainTarget != null;
    }

    void LateUpdate()
    {
        if (_isMainTargetNotNull)
        {
            // Match the rotation of the target
            transform.rotation = MainTarget.rotation;
        }
    }
}