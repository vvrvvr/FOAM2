using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using Random = System.Random;

public class GhostSpawner : MonoBehaviour
{
     [SerializeField] private float _minSpawnDistance;
     [SerializeField] private float _maxSpawnDistance;
     [SerializeField] private float _spawnRate = 3f;
     [SerializeField] private Transform[] _spawnPoints = Array.Empty<Transform>();
     [SerializeField] private GameObject[] _ghosts = Array.Empty<GameObject>();
     
     
     //private
     private ThirdPersonController _player;
     private float _currentTime = 0f;
     private GameObject currentActiveObject;
     private Transform _playerTransform;
     private int _prevSpawnPointIndex = -1;
     private float _currentSpawnRate = 0f;
     
     
     
    
    // Start is called before the first frame update
    void Start()
    {
       
        
        
        _currentTime = 0f;
        _currentSpawnRate = _spawnRate;
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
        Debug.Log("spawned");
        
        
        if (currentActiveObject == null)
        {
            var currentPlayerPosition = _playerTransform.position;
            var rng = new Random();
            rng.Shuffle(_spawnPoints);
            
            
            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                if (IsInLineOfSight(_spawnPoints[i]))
                    break;
            }

           // int properPOintIndex = -1;
            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                float dist = Vector3.Distance(currentPlayerPosition, _spawnPoints[i].position);
                if (dist < _maxSpawnDistance && dist > _minSpawnDistance)
                {
                    if (_prevSpawnPointIndex != i)
                    {
                        //properPOintIndex = i;
                        //_prevSpawnPointIndex = i;
                        Debug.Log(_spawnPoints[i].name);
                        //Debug.Log(dist);
                        break;
                    }
            
                }
            }
        }
    }

    private bool IsInLineOfSight(Transform obj)
    {
        Vector3 dir = obj.position - Camera.main.transform.position;
        float angle = Vector3.Angle(dir, Camera.main.transform.forward);

        if (angle < Camera.main.fieldOfView)
        {
            Ray ray = new Ray(Camera.main.transform.position, dir);
            if (!Physics.Raycast(ray, dir.magnitude))
            {
                Debug.Log(obj.name + " visible");
                return true;
            }
        }

        return false;
    }

}
