using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSequence : MonoBehaviour
{
    [SerializeField] private PushSignal _pushSignal;
    public float pushForce = 0.1f;   // Сила толчка по оси x
    public float returnForce = 0.05f;  // Сила возврата в исходное положение по оси x

    private Rigidbody rb;           // Ссылка на Rigidbody
    private bool isJumping = false; // Флаг, указывающий, происходит ли в данный момент толчок
    private Vector3 initialPosition; // Начальная позиция тела
    public Transform anchorTransform;
    public Rigidbody AnchorRb;
    public GameObject PLayerArmature;
    public int maxPushCount = 20;
    private int _currentPushCount = 0;
    [SerializeField] private PlayerSwitchViews _playerSwitchViews;
    [SerializeField] private PlayerManager _playerManager;

    private StartSequence _startSequence;

    public float _currentTimeBetween = 0f;
    public float _timeBetweenMin = 0.4f;
    public float _timeBetweenMax = 1f;

    private bool isRelease = false;
    private bool isAllowed = true;

    void Start()
    {
        rb = AnchorRb;
        initialPosition = anchorTransform.position;
        _startSequence = gameObject.GetComponent<StartSequence>();
    }

    void Update()
    {
        _currentTimeBetween += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && isAllowed)
        {
            _currentPushCount++;
            isJumping = true;
            if (_currentPushCount < maxPushCount)
            {
                if (pushForce < 1 && _currentTimeBetween > _timeBetweenMin && _currentTimeBetween < _timeBetweenMax)
                {
                    pushForce += 0.1f;
                }

                if (_pushSignal.isTouched)
                {
                    _pushSignal.isTouched = false;
                    if ((_currentTimeBetween > _timeBetweenMin) && (_currentTimeBetween <= _timeBetweenMax) &&
                        (pushForce > 1f))
                    {
                        pushForce += 0.2f;
                    }
                }

                if (_currentTimeBetween > _timeBetweenMax)
                {
                    pushForce = 0.1f;
                }

                _currentTimeBetween = 0f;
            }
            else
            {
                
                pushForce = 4.5f;
                isRelease = true;
                //Debug.Log("here");

            }

            if (_currentTimeBetween > _timeBetweenMax)
            {
                pushForce = 0.1f;
            }
        }
        
    }

    void FixedUpdate()
    {
        if (isJumping)
        {
            // Добавляем силу вправо по оси x
            rb.AddRelativeForce(Vector3.right * pushForce, ForceMode.Impulse);
            isJumping = false;
            if (isRelease)
            {
                isAllowed = false;
                _currentPushCount = 0;
                isRelease = false;
                StartCoroutine(ReleaseAfterPush());
            }
        }

        
    }

    private IEnumerator ReleaseAfterPush()
    {
        yield return new WaitForSeconds(0.3f);
        PLayerArmature.transform.parent = null;
        _startSequence.enabled = false;
        _playerSwitchViews.EndStartSequence();
        _playerManager.EnablePlayer();
    }
    
}
