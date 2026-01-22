
using System;
using UnityEngine;

public class Health : MonoExt, IDamageable, IHealable
{
    [SerializeField] private Stat MaxHealth;

    public float MaxHP => MaxHealth.Value;
    public float HP;

    protected void Awake()
    {
        HP = MaxHP;
    }

    public void ApplyDamage(float damageValue)
    {
        HP -= damageValue;

        if (HP <= 0)
            OnDeath();
    }

    public void ApplyHealing(float healingValue)
    {
        HP += healingValue;
        
        if (HP > MaxHP)
            HP = MaxHP;
    }

    protected virtual void OnDeath()
    {
        Destroy(gameObject, 0.2f);
    }
}