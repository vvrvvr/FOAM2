using System.Collections;
using UnityEngine;
using DG.Tweening;

public class InterfaceObject : MonoBehaviour
{
    [SerializeField] private float targetScale = 0.5f;
    [SerializeField] private float scaleTime = 0.2f;
    [SerializeField] private float rotationSpeed = 1f;
    private Vector3 originalScale;
    private bool isScaling = false;
    private bool isRotating = false;
    private Coroutine rotateCoroutine;
    private Coroutine returnToScreenCoroutine;
    private Rigidbody rb;
    private mInterfaceManager _mInterfaceManager;

    private bool _isDropped = false;
    public bool isOnscreen = true;
    private Vector3 dropDir = Vector3.zero;

    private Vector3 onScreenPosition;
    private Quaternion onScreenRotation;

    private void Start()
    {
        originalScale = transform.localScale;
        rb = GetComponent<Rigidbody>();
        var transform1 = transform;
        onScreenPosition = transform1.position;
        onScreenRotation = transform1.rotation;
    }

    public void SetLinkToManager(mInterfaceManager manager)
    {
        _mInterfaceManager = manager;
    }

    public void IsTaken()
    {
        rb.isKinematic = true;
         isOnscreen = false;
         _mInterfaceManager.CheckInterfaceFree();
        _isDropped = false;
        SetInHandScale();
        if (returnToScreenCoroutine != null)
            StopCoroutine(returnToScreenCoroutine);
    }

    public void isDropped(Vector3 dropDirection)
    {
        rb.isKinematic = false;
        SetDefaultScale();
        dropDir = dropDirection;
        _isDropped = true;
        rb.AddForce(dropDirection, ForceMode.Impulse);
        returnToScreenCoroutine = StartCoroutine(ReturnToScreen(4f));
    }

    public void SetInHandScale()
    {
        ScaleOverTime(targetScale, scaleTime);
        isRotating = true;
        rotateCoroutine = StartCoroutine(RotateOverTime(rotationSpeed));
    }

    public void SetDefaultScale()
    {
        transform.DOScale(originalScale, scaleTime).OnComplete(() => { isScaling = false; });
        isRotating = false;
        StopCoroutine(rotateCoroutine);
    }

    private void ScaleOverTime(float targetScale, float time)
    {
        var target = new Vector3(originalScale.x / this.targetScale, originalScale.y / targetScale,
            originalScale.z / targetScale);
        transform.DOScale(target, time).OnComplete(() => { isScaling = false; });
    }

    IEnumerator RotateOverTime(float speed)
    {
        while (isRotating)
        {
            //Debug.Log("rotating");
            transform.Rotate(new Vector3(rotationSpeed, rotationSpeed, rotationSpeed) * Time.deltaTime);
            yield return null; // Ждем один кадр
        }
    }

    IEnumerator ReturnToScreen(float time)
    {
        yield return new WaitForSeconds(time);
        if (_isDropped)
        {
            rb.isKinematic = true;
            transform.DOMove(onScreenPosition, 0.2f).OnComplete(() => { isOnscreen = true; _mInterfaceManager.CheckInterfaceFree(); });
            transform.DORotateQuaternion(onScreenRotation, 1f);
        }
    }
}