using UnityEngine;
public class ManaEmitter : MonoBehaviour
{
    // Определение делегата для события изменения маны
    public delegate void ManaChanged(int mana);
    public static event ManaChanged OnManaChanged;

    // Метод для изменения здоровья
    public void ChangeMana(int mana)
    {
        // Вызываем событие, когда происходит изменение здоровья
        OnManaChanged?.Invoke(mana);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            ChangeMana(-10);
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            ChangeMana(5);
        }
    }
}