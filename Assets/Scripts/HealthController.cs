using NaughtyAttributes;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [BoxGroup("Health")]
    public float currentHealth;
    [BoxGroup("Health")]
    public float maxHealth;
    [BoxGroup("Defense")]
    public float damageResistance;
    [BoxGroup("Defense")]
    public float defense;
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

    public void TakeDamage(float damage, bool isTrueDamage = false)
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
                finalDamage = Mathf.Clamp(damage - defense, 1, 100000) * (1 - Mathf.Clamp(damageResistance, -1000, 99f)/100);
                
            }
            isInvincible = true;
            Invoke(nameof(RemoveInvincibility), invincibilityTime);
            
            currentHealth -= finalDamage;
            if(currentHealth < 0)
            {
                Death();
            }


            onPlayerDamage?.Invoke();
        }
    }
    public void Heal(float healAmount)
    {
        if (healAmount + currentHealth > maxHealth) currentHealth = maxHealth;
        else currentHealth += healAmount;
        onPlayerHeal?.Invoke();
    }

    public void Death()
    {
        currentHealth = 0;
        onPlayerDeath?.Invoke();
        print("im dead");
    }


    void RemoveInvincibility()
    {
        isInvincible = false;
    }
}
