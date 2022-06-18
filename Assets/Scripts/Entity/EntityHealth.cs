using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : MonoBehaviour
{
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    public UnityEvent<int, int> OnHealthValueChanged;

    public void InitialSetUp(int maxHealth)
    {
        SetMaxHealth(maxHealth);
        CurrentHealth = maxHealth;
    }

    public void SetMaxHealth(int maxHealth)
    {
        MaxHealth = maxHealth;
    }

    public void ReduceHealth(int amount)
    {
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        OnHealthValueChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        OnHealthValueChanged?.Invoke(CurrentHealth, MaxHealth);
    }
}
