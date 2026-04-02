using NaughtyAttributes;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [BoxGroup("Health")]
    public float currentHealth;
    [BoxGroup("Health")]
    public float maxHealth;
    [BoxGroup("Defence")]
    public float damageResistance;
    [BoxGroup("Defence")]
    public float defence;
    [BoxGroup("Invincibility")]
    public bool isInvincible;
    [BoxGroup("Invincibility")]
    public float invincibilityTime;

    public delegate void OnPlayerDamage();
    public static event OnPlayerDamage onPlayerDamage;
    
    public delegate void OnPlayerHeal();
    public static event OnPlayerHeal onPlayerHeal;
    
    public delegate void OnPlayerDeath();
    public static event OnPlayerDeath onPlayerDeath;
    
    void Awake()
    {
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
            onPlayerDamage?.Invoke();
        }
    }
    void Heal(float healAmount)
    {
        if (healAmount + currentHealth > maxHealth) currentHealth = maxHealth;
        else currentHealth += healAmount;
        onPlayerHeal?.Invoke();
    }
    void RemoveInvincibility()
    {
        isInvincible = false;
    }
}
