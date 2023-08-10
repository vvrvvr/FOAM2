using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class mInterfaceManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private GameObject playerMesh;
    [SerializeField] private LookAtObject lookAtObject;
    public List<InterfaceObject> interfaceList = new List<InterfaceObject>();
    public List<InterfaceObject> interfaceListType2 = new List<InterfaceObject>();
    public List<InterfaceObject> interfaceListType3 = new List<InterfaceObject>();
    public List<InterfaceObject> interfaceListType4 = new List<InterfaceObject>();
    public bool isInterfaceFree = false;


    private void Start()
    {
        lookAtObject.enabled = false;
        foreach (var obj in interfaceList)
        {
            obj.SetLinkToManager(this);
        }
        foreach (var obj in interfaceListType2)
        {
            obj.SetLinkToManager(this);
        }
        foreach (var obj in interfaceListType3)
        {
            obj.SetLinkToManager(this);
        }
        foreach (var obj in interfaceListType4)
        {
            obj.SetLinkToManager(this);
        }
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

    public void CheckInterfaceFree()
    {
        foreach (var obj in interfaceList)
        {
            if (obj.isOnscreen == false)
            {
                isInterfaceFree = true;
                // EventManager.OnItemHeld.Invoke();
                return;
            }
        }

        isInterfaceFree = false;
        //EventManager.OnItemDroped.Invoke();
    }

    public void RemoveFromList(InterfaceObject obj, int interfaceType)
    {
        switch (interfaceType)
        {
            case 1:
                if (interfaceList.Contains(obj))
                {
                    interfaceList.Remove(obj);
                    CheckInterfaceFree();
                }

                break;
            case 2:
                if (interfaceListType2.Contains(obj))
                {
                    interfaceListType2.Remove(obj);
                    CheckInterfaceFree();
                }

                break;
            case 3:
                if (interfaceListType3.Contains(obj))
                {
                    interfaceListType3.Remove(obj);
                    CheckInterfaceFree();
                }

                break;
            case 4:
                if (interfaceListType4.Contains(obj))
                {
                    interfaceListType4.Remove(obj);
                    CheckInterfaceFree();
                }

                break;
            default:
                Debug.LogError("Invalid interface type!");
                break;
        }
    }
}