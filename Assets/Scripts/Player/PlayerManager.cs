using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerArmature;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private ThirdPersonController _personController;
    [SerializeField] private PlayerInput _playerInput;
    
    //Dissolve
    [SerializeField] private Material _currentMat;
    [SerializeField] private float _dissolveTime = 0.2f;
    private Coroutine _dissolveCoroutine;
    public bool isVisible = false;


    public void EnablePlayer()
    {
        _characterController.enabled = true;
        _animator.enabled = true;
        _personController.enabled = true;
        _playerInput.enabled = true;
    }

    private void Start()
    {
        // Убедитесь, что материал имеет параметр _Dissolve
        if (!_currentMat.HasProperty("_Dissolve"))
        {
            Debug.LogError("Material does not have Dissolve property!");
            enabled = false;
            return;
        }

        if (isVisible)
        {
            _currentMat.SetFloat("_Dissolve", 0f);
        }
        else
        {
            _currentMat.SetFloat("_Dissolve", 1f);
        }

    }
    public void StartDissolve(bool dissolveIn)
    {
        if (_dissolveCoroutine != null)
            StopCoroutine(_dissolveCoroutine);

        float startValue = _currentMat.GetFloat("_Dissolve");
        float endValue = dissolveIn ? 0 : 1;
        _dissolveCoroutine = StartCoroutine(DissolveCoroutine(startValue, endValue));
    }
    
    private IEnumerator DissolveCoroutine(float startValue, float endValue)
    {
        float elapsedTime = 0f;
        while (elapsedTime < _dissolveTime)
        {
            float t = elapsedTime / _dissolveTime;
            float value = Mathf.Lerp(startValue, endValue, t);
            _currentMat.SetFloat("_Dissolve", value);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _currentMat.SetFloat("_Dissolve", endValue);
    }
    public void DisablePlayer()
    {
        _characterController.enabled = false;
        _animator.enabled = false;
        _personController.enabled = false;
        _playerInput.enabled = false;
    }
}
