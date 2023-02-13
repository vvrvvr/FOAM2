using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float timetoRestart = 1f;
    private float _currentTime = 0f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= timetoRestart)
        {
            _currentTime = 0f;
            gameObject.SetActive(false);
        }
    }
}