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
    private Coroutine MoveToCoroutine;
    private Rigidbody rb;
    private mInterfaceManager _mInterfaceManager;

    private bool _isDropped = false;
    public bool isOnscreen = true;
    private Vector3 dropDir = Vector3.zero;

    public int interfaceType = 1;
    [SerializeField] private bool isType1 = true;
    
    [SerializeField]private Vector3 onScreenPosition;
    [SerializeField]private Quaternion onScreenRotation;
    public Transform _parentObj;
    public bool CanTake = true;
    
    

    private void Start()
    {
        originalScale = transform.localScale;
        rb = GetComponent<Rigidbody>();
        var transform1 = transform;
        if (isType1)
        {
            onScreenPosition = transform1.position;
            onScreenRotation = transform1.rotation;
        }
        if (transform.parent != null)
            _parentObj = transform.parent;
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
        SetInHandScale(targetScale, scaleTime, rotationSpeed);
        if (MoveToCoroutine != null)
            StopCoroutine(MoveToCoroutine);
    }

    public void isDropped(Vector3 dropDirection, float dropforce)
    {
        EventManager.OnItemDroped.Invoke();
        rb.isKinematic = false;
        SetDefaultScale();
        dropDir = dropDirection;
        _isDropped = true;
        rb.AddForce(dropDirection *dropforce, ForceMode.Impulse);
        MoveToCoroutine = StartCoroutine(MoveAndRotateTo(4f, 0.2f, onScreenPosition, onScreenRotation));
        SetParentObj(_parentObj);
        
    }

    public void SetInHandScale(float targetScale, float scaleTime, float rotationSpeed)
    {
        ScaleOverTime(targetScale, scaleTime);
        isRotating = true;
        rotateCoroutine = StartCoroutine(RotateOverTime(rotationSpeed));
    }

    public void SetDefaultScale()
    {
        transform.DOScale(originalScale, scaleTime).OnComplete(() => { isScaling = false; });
        isRotating = false;
        if(rotateCoroutine != null)
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
    
    public void SetParentObj( Transform parent)
    {
        if (_parentObj != null)
            gameObject.transform.parent = parent;
        else
            Debug.LogWarning("Ранее кешированный родительский объект отсутствует");
    }

    IEnumerator MoveAndRotateTo(float time, float speed, Vector3 position, Quaternion rotation)
    {
        yield return new WaitForSeconds(time);
        if (_isDropped)
        {
            CanTake = false;
            rb.isKinematic = true;
            transform.DOMove(position, speed).OnComplete(() => { isOnscreen = true; _mInterfaceManager.CheckInterfaceFree();
                CanTake = true;
            });
            transform.DORotateQuaternion(rotation, 1f);
        }
    }
    

    public void MoveInterfaceToSlot(float time, float speed, Transform targetPosition)
    {
        StopCoroutine(MoveToCoroutine);
        MoveToCoroutine = StartCoroutine(MoveAndRotateTo(time, speed, targetPosition.position, targetPosition.rotation));
        // _mInterfaceManager.RemoveFromList(this);
        // SetParentObj(targetPosition);
        gameObject.layer = 0;
    }

    public void SetInterfaceLayer(int layer)
    {
        gameObject.layer = layer;
    }
    
    
}