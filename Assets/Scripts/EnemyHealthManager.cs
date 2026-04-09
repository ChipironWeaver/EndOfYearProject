using NaughtyAttributes;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    [BoxGroup("Health")]
    public float currentHealth;
    [BoxGroup("Health")]
    public float maxHealth;
    [BoxGroup("Defense")]
    public float damageResistance;
    [BoxGroup("Defense")]
    public float defense;
    public void TakeDamage(float damage, bool isTrueDamage = false)
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
        
        currentHealth -= finalDamage;

        if(currentHealth <= 0)
        {
             Death();
        }
    }
    public void Heal(float healAmount)
    {
        if (healAmount + currentHealth > maxHealth) currentHealth = maxHealth;
        else currentHealth += healAmount;
    }

    public void Death()
    {
        EnemySpawner.Instance.enemies.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

}
