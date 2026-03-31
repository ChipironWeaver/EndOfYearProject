using System;
using System.Collections;
using NaughtyAttributes;
using NUnit.Framework.Constraints;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float damageResistance;
    public float defence;
    public bool isInvincible;
    public float invincibilityTime;

    [BoxGroup("Testing")]
    [SerializeField] private float damagetest;
    public static HealthController Instance { get; private set; }

    void Awake()
    {
        Singleton();
    }

    void TakeDamage(float damage, bool isTrueDamage = false)
    {
        if (!isInvincible)
        {
            float finalDamage = 0;
            if (isTrueDamage)
            {
                finalDamage = damage;
            }
            else
            {
                finalDamage = Mathf.Clamp(damage - defence, 1, 100000) *
                              (1 - Mathf.Clamp(damageResistance, -1000, 99f)/100);
                
            }
            isInvincible = true;
            Invoke(nameof(RemoveInvincibility), invincibilityTime);
            print(finalDamage);
        }
    }

    void Heal(float healAmount)
    {
        if (healAmount + currentHealth > maxHealth) currentHealth = maxHealth;
        else currentHealth += healAmount;
    }
    
    
    [Button]
    void RemoveInvincibility()
    {
        isInvincible = false;
    }

    void Singleton()
    {
        if (Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    [Button]
    void DamageTest()
    {
        TakeDamage(damagetest);
    }
    [Button]
    void TrueDamageTest()
    {
        TakeDamage(damagetest, true);
    }
}
