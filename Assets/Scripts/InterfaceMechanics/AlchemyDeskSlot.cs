using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AlchemyDeskSlot : MonoBehaviour
{
    [SerializeField] private GameObject outline;
    [SerializeField] private Transform itemPlace;
    [SerializeField] private mInterfaceManager interfaceManager;
    //[SerializeField] private ObjectInteraction objectInteraction;
    public int InterfaceTypeInSlot = 0;
    private InterfaceObject currentInterface = null;
    public AlchemyDeskMain DeskMain;
    private Coroutine wait;
        

    public bool isSlotBusy = false;

    void Start()
    {
        outline.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.OnItemHeld.AddListener(OutlineOn);
        EventManager.OnItemDroped.AddListener(OutlineOff);
    }

    private void OnDisable()
    {
        EventManager.OnItemHeld.RemoveListener(OutlineOn);
        EventManager.OnItemDroped.AddListener(OutlineOff);
    }

    private void OutlineOn()
    {
        if (!isSlotBusy)
            outline.SetActive(true);
    }

    private void OutlineOff()
    {
        if (!isSlotBusy )
            outline.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interface") && !isSlotBusy)
        {
            currentInterface = other.GetComponent<InterfaceObject>();
            currentInterface.MoveInterfaceToSlot(0f, 0.3f, itemPlace );
            currentInterface.SetInHandScale(1.1f, 0.3f, 0.3f);
            InterfaceTypeInSlot = currentInterface.interfaceType;
            isSlotBusy = true;
            wait = StartCoroutine(WaitToMerge(1f));
        }
    }

    public void RejectMerge()
    {
        currentInterface.isDropped(Vector3.left, 15f); //поправить направление
        currentInterface.SetInterfaceLayer(15);
        currentInterface = null;
        InterfaceTypeInSlot = 0;
        isSlotBusy = false;
    }

    public void ApplyMerge()
    {
        // удалять из списка менеджера интерфейсов, удалять объект
        
        // currentInterface = null;
        // InterfaceTypeInSlot = 0;
        // isSlotBusy = false;
    }
    
    IEnumerator WaitToMerge(float time)
    {
        yield return new WaitForSeconds(time);
        //Debug.Log("try to merge");
        DeskMain.MergeInterfaces();
    }
}