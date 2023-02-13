using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private float _minSpawnDistance;
    [SerializeField] private float _maxSpawnDistance;
    [SerializeField] private float _spawnRateNormal = 3f;
    [SerializeField] private float _spawnRateMin = 1f;
    [SerializeField] private Transform[] _spawnPoints = Array.Empty<Transform>();
    [SerializeField] private GameObject[] _ghosts = Array.Empty<GameObject>();
    public LayerMask mask;


    //private
    private ThirdPersonController _player;
    private float _currentTime = 0f;
    private GameObject currentActiveObject;
    private Transform _playerTransform;
    private float _currentSpawnRate = 0f;
    private Camera _mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _currentTime = 0f;
        _currentSpawnRate = _spawnRateNormal;
        _player = ThirdPersonController.Instance;
        _playerTransform = _player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _currentSpawnRate)
        {
            _currentTime = 0f;
            SpawnGhost();
        }
    }

    private void SpawnGhost()
    {
        Debug.Log("Checked in " + _currentSpawnRate);
        
        var currentPlayerPosition = _playerTransform.position;
        var rng = new Random();
        rng.Shuffle(_spawnPoints);

        _currentSpawnRate = _spawnRateMin;

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            float dist = Vector3.Distance(currentPlayerPosition, _spawnPoints[i].position);
            if (dist < _maxSpawnDistance && dist > _minSpawnDistance)
            {
                if (IsInLineOfSight(_spawnPoints[i]))
                {
                    Debug.Log("spawned at " + _spawnPoints[i].name + "distance: " + dist);
                    var ghost = RandomExtensions.GetRandomElement(_ghosts);
                    ghost.SetActive(true);
                    
                    ghost.GetComponent<Transform>().position = _spawnPoints[i].position;
                    _currentSpawnRate = _spawnRateNormal;
                    break;
                }
            }
        }
    }

    private bool IsInLineOfSight(Transform obj)
    {
        Vector3 dir = obj.position - _mainCamera.transform.position;
        float angle = Vector3.Angle(dir, _mainCamera.transform.forward);

        if (angle < _mainCamera.fieldOfView)
        {
            Ray ray = new Ray(_mainCamera.transform.position, dir);
            if (!Physics.Raycast(ray, dir.magnitude, mask))
            {
                return true;
            }
        }

        return false;
    }
}