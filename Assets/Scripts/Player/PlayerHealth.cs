using System;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    public event Action<int, int> OnHealthChanged;

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        OnHealthChanged?.Invoke(GetCurrentHealth(), GetMaxHealth());
    }

    protected override void Die()
    {
        base.Die();
        OnHealthChanged?.Invoke(0, GetMaxHealth());
    }
}
