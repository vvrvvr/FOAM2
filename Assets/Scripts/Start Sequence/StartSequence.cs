using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSequence : MonoBehaviour
{
    [SerializeField] private PushSignal _pushSignal;
    [SerializeField] private PlayerSwitchViews _playerSwitchViews;
    [SerializeField] private PlayerManager _playerManager;
    public Transform anchorTransform;
    public Rigidbody AnchorRb;
    public GameObject PLayerArmature;

    public float pushForce = 0.1f; // Сила толчка по оси x
    public float returnForce = 0.05f; // Сила возврата в исходное положение по оси x
    public int maxPushCount = 20;
    public float _currentTimeBetween = 0f;
    public float _timeBetweenMin = 0.4f;
    public float _timeBetweenMax = 1f;

    //private
    private Rigidbody rb; // Ссылка на Rigidbody
    private bool isJumping = false; // Флаг, указывающий, происходит ли в данный момент толчок
    private int _currentPushCount = 0;
    private StartSequence _startSequence;
    private bool isRelease = false;
    private bool isAllowed = true;
    
    enum State
    {
        Idle,
        Pushing,
        Releasing,
        Flying
    }
    private State currentState = State.Pushing;

    void Start()
    {
        rb = AnchorRb;
        _startSequence = gameObject.GetComponent<StartSequence>();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                
                break;

            case State.Pushing:
                PushingHandleInput();
                break;

            case State.Releasing:
                
                break;
            case State.Flying:
                
                break;
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

    private void PushingHandleInput()
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
    private IEnumerator ReleaseAfterPush()
    {
        yield return new WaitForSeconds(0.3f);
        PLayerArmature.transform.parent = null;
        _playerSwitchViews.EndStartSequence();
        _playerManager.StartDissolve(false);
        _playerManager.EnablePlayer();
        _startSequence.enabled = false;
    }
}