using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class EdgePortalController : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private Transform _edgePortal;


    //private vars
    private Transform anchor;
    private ThirdPersonController _player;

    private void Start()
    {
        anchor = GetComponent<Transform>();
        _player = ThirdPersonController.Instance;
    }

    private void Update()
    {
        ControlPortalPosition();
    }

    private void ControlPortalPosition()
    {
        var position = anchor.position;
        Vector3 playerPos = _player.transform.position;
        Vector3 anchorToPlayer = new Vector3(playerPos.x, 0f, playerPos.z) -
                                 new Vector3(position.x, 0f, position.z);
        float distanceToPlayer = anchorToPlayer.magnitude;

        if (distanceToPlayer > distance) // Если игрок находится вне радиуса окружности
        {
            Vector3 newPos = new Vector3(anchor.position.x, _edgePortal.position.y, anchor.position.z) +
                             anchorToPlayer.normalized *
                             distance; // Вычисляем новую позицию объекта на расстоянии от центра окружности, сохраняя y координату объекта
            _edgePortal.position = newPos; // Перемещаем объект на новую позицию
        }
        else // Если игрок находится внутри радиуса окружности
        {
            Vector3 anchorToObj = new Vector3(_edgePortal.position.x, 0f, _edgePortal.position.z) -
                                  new Vector3(anchor.position.x, 0f,
                                      anchor.position
                                          .z); // Получаем вектор от центра окружности до объекта, игнорируя y координату
            Vector3
                newAnchorToObj =
                    anchorToPlayer.normalized *
                    distance; // Вычисляем новый вектор от центра окружности до объекта, который лежит на линии, проходящей через центр окружности и центр игрока
            Vector3 newPos = new Vector3(anchor.position.x, _edgePortal.position.y, anchor.position.z) +
                             new Vector3(newAnchorToObj.x, 0f,
                                 newAnchorToObj.z); // Вычисляем новую позицию объекта, сохраняя y координату объекта
            _edgePortal.position = newPos; // Перемещаем объект на новую позицию
        }
    }
}