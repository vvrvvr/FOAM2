using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
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

    private void Start()
    {
        originalScale = transform.localScale;
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
    
    IEnumerator RotateOverTime(float speed) {
        
        while (isRotating) { 
            Debug.Log("rotating");
            transform.Rotate(new Vector3(rotationSpeed, rotationSpeed, rotationSpeed) * Time.deltaTime); 
            yield return null; // Ждем один кадр
        }
    }
}