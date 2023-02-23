using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class EdgePortalActivator : MonoBehaviour
{
  [SerializeField] private GameObject edgePortalController;
  private ThirdPersonController _player;

  private void Start()
  {
    _player = ThirdPersonController.Instance;
  }

  private void OnCollisionEnter(Collision collision)
  {
    Debug.Log("here ssss");
    if (collision.gameObject.CompareTag("Player"))
    {
      edgePortalController.SetActive(true);
      Debug.Log("here");
    }
  }

  private void OnCollisionExit(Collision other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      edgePortalController.SetActive(true);
      Debug.Log("deactivate");
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      edgePortalController.SetActive(true);
      Debug.Log("here");
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      edgePortalController.SetActive(false);
      Debug.Log("deactivate");
    }
  }
}
