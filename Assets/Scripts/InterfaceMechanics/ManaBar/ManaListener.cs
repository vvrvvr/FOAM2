using UnityEngine;

public class ManaListener : MonoBehaviour
{
    [SerializeField] private HealthBar _manaBar;
    private void OnEnable()
    {
        ManaEmitter.OnManaChanged += AcceptChangeMana;
    }

    private void OnDisable()
    {
        ManaEmitter.OnManaChanged -= AcceptChangeMana;
    }

    private void AcceptChangeMana(int mana)
    {
        _manaBar.SetHealth(mana);
    }

    // public static void DisableAllEmiters()
    // {
    //     HealthEmitter[] healthManagers = FindObjectsOfType<HealthEmitter>();
    //     foreach (HealthEmitter manager in healthManagers)
    //     {
    //         manager.enabled = false;
    //     }
    // }
}