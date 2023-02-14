using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float timetoRestart = 10f;
    private float _currentTime = 0f;
    private Animator _animator;
    private static readonly int Disappear = Animator.StringToHash("disappear");
    private bool _isDisappear = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDisappear)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= timetoRestart)
            {
                _isDisappear = true;
                _currentTime = 0f;
                _animator.SetBool(Disappear, _isDisappear);
            }
        }
       
    }

    public void DeactivateGhost()
    {
        _isDisappear = false;
        _animator.SetBool(Disappear, _isDisappear);
        gameObject.SetActive(false);
    }
}