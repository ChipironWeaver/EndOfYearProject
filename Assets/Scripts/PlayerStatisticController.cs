using NaughtyAttributes;
using UnityEngine;

public class PlayerStatisticController : MonoBehaviour
{
    [SerializeField] private PlayerStatistic _basePlayerStats;
    [ReadOnly] public PlayerStatistic playerStats;

    private void Awake()
    {
        ResetStats();
        ApplyStatsOnComponent();
    }
    
    public void ResetStats()
    {
        playerStats = _basePlayerStats;
    }

    public void ApplyStatsOnComponent()
    {
        PlayerInstance.playerMovementController.moveSpeed = playerStats.moveSpeed;
        PlayerInstance.playerMovementController.gravityForce = playerStats.gravityForce;
        PlayerInstance.playerMovementController.jumpForce = playerStats.jumpForce;
        PlayerInstance.shootController.fireRate = playerStats.fireRate * playerStats.fireRateMultiplier;
        PlayerInstance.shootController.numberOfProjectiles = playerStats.numberOfProjectile;
        PlayerInstance.shootController.projectileSpeed = playerStats.projectileSpeed;
        PlayerInstance.healthController.maxHealth = playerStats.maxHealth * playerStats.maxHealthMultiplier;
        PlayerInstance.healthController.defense = playerStats.defense * playerStats.defenseMultiplier;
        PlayerInstance.healthController.damageResistance = playerStats.dmgResistance * playerStats.dmgResistanceMultiplier;
        PlayerInstance.healthController.healRate = playerStats.healRate;
        PlayerInstance.healthController.healAmount = playerStats.healAmount;
        PlayerInstance.healthController.invincibilityTime = playerStats.invincibleTime;
    }

    public float GetDamage()
    {
        float damage = playerStats.damage * playerStats.damageMultiplier;
        if (playerStats.critRate > Random.Range(0, 100))
        {
            damage *= 1 + playerStats.critDamage / 100;
            print("crit!");
        }
        print(damage);
        return damage;
    }
}