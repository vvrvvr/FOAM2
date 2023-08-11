using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class InterfaceObject : MonoBehaviour
{
    [SerializeField] private float targetScale = 0.5f;
    [SerializeField] private float scaleTime = 0.2f;
    [SerializeField] private float rotationSpeed = 1f;
    private Vector3 _originalScale;
    private bool _isScaling = false;
    private bool _isRotating = false;
    private Coroutine _rotateCoroutine;
    private Coroutine _moveToCoroutine;
    private Rigidbody _rb;
    private mInterfaceManager _mInterfaceManager;

    private bool _isDropped = false;
    public bool isOnscreen = true;
    private Vector3 dropDir = Vector3.zero;

    public int interfaceType = 1;
    public Vector3 onScreenPosition;
    [SerializeField]private Quaternion onScreenRotation;
    public Transform _parentObj;
    public bool CanTake = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _originalScale = transform.localScale;
    }

    private void Start()
    {
        var transform1 = transform;
        if (isOnscreen)
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
        _rb.isKinematic = true;
         isOnscreen = false;
         _mInterfaceManager.CheckInterfaceFree();
        _isDropped = false;
        SetInHandScale(targetScale, scaleTime, rotationSpeed);
        if (_moveToCoroutine != null)
            StopCoroutine(_moveToCoroutine);
    }

    public void isDropped(Vector3 dropDirection, float dropforce, float returnTime)
    {
        EventManager.OnItemDroped.Invoke();
        _rb.isKinematic = false;
        SetDefaultScale();
        dropDir = dropDirection;
        _isDropped = true;
        _rb.AddForce(dropDirection *dropforce, ForceMode.Impulse);
        _moveToCoroutine = StartCoroutine(MoveAndRotateTo(returnTime, 0.2f, onScreenPosition, onScreenRotation));
        SetParentObj(_parentObj);
        
    }

    public void SetInHandScale(float targetScale, float scaleTime, float rotationSpeed)
    {
        ScaleOverTime(targetScale, scaleTime);
        _isRotating = true;
        _rotateCoroutine = StartCoroutine(RotateOverTime(rotationSpeed));
    }

    public void SetDefaultScale()
    {
        transform.DOScale(_originalScale, scaleTime).OnComplete(() => { _isScaling = false; });
        _isRotating = false;
        if(_rotateCoroutine != null)
            StopCoroutine(_rotateCoroutine);
    }

    private void ScaleOverTime(float targetScale, float time)
    {
        var target = new Vector3(_originalScale.x / targetScale, _originalScale.y / targetScale,
            _originalScale.z / targetScale);
        transform.DOScale(target, time).OnComplete(() => { _isScaling = false; });
    }

    IEnumerator RotateOverTime(float speed)
    {
        while (_isRotating)
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
            _rb.isKinematic = true;
            transform.DOMove(position, speed).OnComplete(() => { isOnscreen = true; _mInterfaceManager.CheckInterfaceFree();
                CanTake = true;
            });
            transform.DORotateQuaternion(rotation, 1f);
        }
    }
    

    public void MoveInterfaceToSlot(float time, float speed, Transform targetPosition)
    {
        StopCoroutine(_moveToCoroutine);
        _moveToCoroutine = StartCoroutine(MoveAndRotateTo(time, speed, targetPosition.position, targetPosition.rotation));
        gameObject.layer = 0;
    }
    public void MoveInterfaceToScreen(float time, float speed, Vector3 position)
    {
        StopCoroutine(_moveToCoroutine);
        _moveToCoroutine = StartCoroutine(MoveAndRotateTo(time, speed, position, onScreenRotation));
        //gameObject.layer = 0;
    }

    public void SetInterfaceLayer(int layer)
    {
        gameObject.layer = layer;
    }

    public void DeleteInterface()
    {
        _mInterfaceManager.RemoveFromList(this, interfaceType);
        _mInterfaceManager.CheckInterfaceFree();
        Destroy(gameObject);
    }
    
}