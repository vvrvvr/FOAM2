using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent OnItemHeld = new UnityEvent();
    public static UnityEvent OnItemDroped = new UnityEvent();
}
