using NaughtyAttributes;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [BoxGroup("Health")]
    public float currentHealth;
    [BoxGroup("Health")]
    public float maxHealth;
    [BoxGroup("Health")]
    public MaxHealthChangeBehavior maxHealthChangeBehavior;
    [BoxGroup("Defense")]
    public float damageResistance;
    [BoxGroup("Defense")]
    public float defense;
    [BoxGroup("Invincibility")]
    public bool isInvincible;
    [BoxGroup("Invincibility")]
    public float invincibilityTime;

    [BoxGroup("Auto Heal")] 
    public float healRate = -1;
    [BoxGroup("Auto Heal")]
    public float healAmount;

    public delegate void OnPlayerDamage();
    public static event OnPlayerDamage onPlayerDamage;
    
    public delegate void OnPlayerHeal();
    public static event OnPlayerHeal onPlayerHeal;

    private float _currentCooldown;
    
    void Update()
    {
        if(healRate > 0)
        {
            _currentCooldown += Time.deltaTime;
            if (_currentCooldown >= 5/healRate)
            {
                Heal(healAmount);
                _currentCooldown = 0;
            }
        }
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
            
            if(currentHealth <= 0)
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
        Actions.OnPlayerLose?.Invoke();
    }

    public void SetMaxHealth(float newMaxHealth)
    {
        if (maxHealth > newMaxHealth)
        {
            onPlayerDamage?.Invoke();
        }
        else if (maxHealth < newMaxHealth)
        {
            onPlayerHeal?.Invoke();
        }
        else
        {
            maxHealth = newMaxHealth;
            return;
        }

        if (maxHealthChangeBehavior == MaxHealthChangeBehavior.AddNewAmount)
        {
            currentHealth += newMaxHealth - maxHealth;
        }
        else if (maxHealthChangeBehavior == MaxHealthChangeBehavior.BasedOnPercentage)
        {
            currentHealth = currentHealth / maxHealth * newMaxHealth;
        }
        else
        {
            currentHealth = Mathf.Clamp(currentHealth, 0, newMaxHealth);
        }
        maxHealth = newMaxHealth;
    }

    void RemoveInvincibility()
    {
        isInvincible = false;
    }
}

public enum MaxHealthChangeBehavior
{
    AddNewAmount,
    BasedOnPercentage,
    DontChange
}
