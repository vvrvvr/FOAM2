using System;
using UnityEngine;

public class AlchemyDeskMain : MonoBehaviour
{
    [SerializeField] private AlchemyDeskSlot slot1;
    [SerializeField] private AlchemyDeskSlot slot2;

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
                slot1.ApplyMerge();
                slot2.ApplyMerge();
                
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
}
