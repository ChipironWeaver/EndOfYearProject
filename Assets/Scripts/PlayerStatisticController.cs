using System;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerStatisticController : MonoBehaviour
{
    [SerializeField] private PlayerStatistic _basePlayerStats;
    [ReadOnly] public PlayerStatistic playerStats;

    private void Awake()
    {
        ResetStats();
        
    }

    public void Start()
    {
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
        
        PlayerInstance.healthController.SetMaxHealth(playerStats.maxHealth * playerStats.maxHealthMultiplier); 
        PlayerInstance.healthController.defense = playerStats.defense * playerStats.defenseMultiplier;
        PlayerInstance.healthController.damageResistance = playerStats.dmgResistance * playerStats.dmgResistanceMultiplier;
        PlayerInstance.healthController.healRate = playerStats.healRate;
        PlayerInstance.healthController.healAmount = playerStats.healAmount;
        PlayerInstance.healthController.invincibilityTime = playerStats.invincibleTime;
        
        PlayerInstance.playerLevelController.expMultiplier = playerStats.expMultiplier;
    }

    public float GetDamage()
    {
        float damage = playerStats.damage * playerStats.damageMultiplier;
        if (playerStats.critRate > Random.Range(0, 100))
        {
            damage *= 1 + playerStats.critDamage / 100;
        }
        return damage;
    }
}
[Flags]
public enum StatType
{
    None = 0,
    ExpMultiplier = 1 << 0,
    MoneyMultiplier = 1 << 1,
    EnemyDropFollowRange= 1 << 2,
    MoveSpeed= 1 << 3,
    GravityForce= 1 << 4,
    JumpForce= 1 << 5,
    FireRate= 1 << 6,
    FireRateMultiplier= 1 << 7,
    NumberOfProjectile= 1 << 8,
    ProjectileSpeed= 1 << 9,
    Piercing= 1 << 10,
    CritRate= 1 << 11,
    CritDamage= 1 << 12,
    Damage= 1 << 13,
    DamageMultiplier= 1 << 14,
    MaxHealth= 1 << 15,
    MaxHealthMultiplier= 1 << 16,
    Defense= 1 << 17,
    DefenseMultiplier= 1 << 18,
    DmgResistance= 1 << 19,
    DmgResistanceMultiplier= 1 << 20,
    HealRate= 1 << 21,
    HealAmount= 1 << 22,
    InvincibleTime= 1 << 23,
    
}