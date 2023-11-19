using UnityEngine;
public class HealthEmitter : MonoBehaviour
{
    // Определение делегата для события изменения здоровья
    public delegate void HealthChanged(int health);
    public static event HealthChanged OnHealthChanged;

    // Метод для изменения здоровья
    public void ChangeHealth(int health)
    {
        // Вызываем событие, когда происходит изменение здоровья
        OnHealthChanged?.Invoke(health);
    }

    // private void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.K))
    //     {
    //         ChangeHealth(-10);
    //     }
    //     if(Input.GetKeyDown(KeyCode.L))
    //     {
    //         ChangeHealth(5);
    //     }
    // }
}