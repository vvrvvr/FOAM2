using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class mInterfaceManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private GameObject playerMesh;
    [SerializeField] private LookAtObject lookAtObject;
    

    private void Start()
    {
        lookAtObject.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MakePlayerVisible();
            lookAtObject.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MakePlayerDefault();
            lookAtObject.enabled = false;
        }
    }


    private void MakePlayerVisible()
    {
        playerMesh.layer = 16;
        _playerManager.StartDissolve(true);
    }

    private void MakePlayerDefault()
    {
        playerMesh.layer = 0;
        _playerManager.StartDissolve(false);
    }
}