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

    public void EnablePlayer()
    {
        _characterController.enabled = true;
        _animator.enabled = true;
        _personController.enabled = true;
        _playerInput.enabled = true;
    }

}
