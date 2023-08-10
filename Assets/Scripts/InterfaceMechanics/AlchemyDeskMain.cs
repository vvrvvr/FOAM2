using System;
using UnityEngine;
using System.Collections.Generic;

public class AlchemyDeskMain : MonoBehaviour
{
    [SerializeField] private AlchemyDeskSlot slot1;
    [SerializeField] private AlchemyDeskSlot slot2;
    [SerializeField] private mInterfaceManager _interfaceManager;
    [SerializeField] private Transform SpawnPosition;

    private void Start()
    {
        slot1.DeskMain = this;
        slot2.DeskMain = this;
    }


    public void MergeInterfaces()
    {
        if (slot1.isSlotBusy && slot2.isSlotBusy)
        {
            //Debug.Log("MERGE");
            if (slot1.InterfaceTypeInSlot == slot2.InterfaceTypeInSlot)
            {
                Debug.Log("MERGE успех");
                var interfaceType = slot1.InterfaceTypeInSlot;
                slot1.ApplyMerge();
                slot2.ApplyMerge();
                switch (interfaceType)
                {
                    case 1:
                        var newInterface = GetRandomElementFromList(_interfaceManager.interfaceListType2);
                        newInterface.transform.position = SpawnPosition.position;
                        newInterface.gameObject.SetActive(true);
                        newInterface.isDropped(Vector3.zero, 0f, 1f);
                        break;
                    default:
                        Debug.LogError("Invalid interface type in slot!");
                        break;
                }
                //проверяем типы интерфейсов и в зависимости от этого спавним новый
            }
            else
            {
                Debug.Log("MERGE провал");
                slot1.RejectMerge();
                slot2.RejectMerge();
            }
        }
    }

    private T GetRandomElementFromList<T>(List<T> inputList)
    {
        if (inputList.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, inputList.Count);
            T randomElement = inputList[randomIndex];
            return randomElement;
        }
        else
        {
            Debug.LogWarning("List is empty!");
            return default; // Вернуть дефолтное значение для типа (например, null для ссылочных типов)
        }
    }
}