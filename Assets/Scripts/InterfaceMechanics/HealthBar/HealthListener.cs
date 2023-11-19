using UnityEngine;

public class HealthListener : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    private void OnEnable()
    {
        HealthEmitter.OnHealthChanged += AcceptChangeHealth;
    }

    private void OnDisable()
    {
        HealthEmitter.OnHealthChanged -= AcceptChangeHealth;
    }

    private void AcceptChangeHealth(int health)
    {
        _healthBar.SetHealth(health);
    }

    public static void DisableAllEmiters()
    {
        HealthEmitter[] healthManagers = FindObjectsOfType<HealthEmitter>();
        foreach (HealthEmitter manager in healthManagers)
        {
            manager.enabled = false;
        }
    }
}