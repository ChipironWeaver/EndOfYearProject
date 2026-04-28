using System;
using System.Diagnostics;
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
        PlayerInstance.playerLevelController.expMultiplier = playerStats.expMultiplier;

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

    public void AddStats(PlayerStatistic stats,StatType statEnum)
    {
        if(statEnum.HasFlag(StatType.ExpMultiplier)) playerStats.expMultiplier += stats.expMultiplier;
        if(statEnum.HasFlag(StatType.MoneyMultiplier)) playerStats.moneyMultiplier += stats.moneyMultiplier;
        if(statEnum.HasFlag(StatType.EnemyDropFollowRange))playerStats.enemyDropFollowRange += stats.enemyDropFollowRange;

        if(statEnum.HasFlag(StatType.MoveSpeed)) playerStats.moveSpeed = Mathf.Clamp(playerStats.moveSpeed + stats.moveSpeed,2.5f,10);
        if(statEnum.HasFlag(StatType.GravityForce)) playerStats.gravityForce = Mathf.Clamp(playerStats.gravityForce + stats.gravityForce,-27,-9);
        if(statEnum.HasFlag(StatType.JumpForce)) playerStats.jumpForce = Mathf.Clamp(stats.jumpForce + playerStats.jumpForce,1,6);
        
        if(statEnum.HasFlag(StatType.FireRate)) playerStats.fireRate = Mathf.Clamp(stats.fireRate + playerStats.fireRate,0.5f,5);
        if(statEnum.HasFlag(StatType.FireRateMultiplier)) playerStats.fireRateMultiplier = Mathf.Clamp(stats.fireRateMultiplier + playerStats.fireRateMultiplier,0.25f,5);
        if(statEnum.HasFlag(StatType.NumberOfProjectile)) playerStats.numberOfProjectile = Mathf.Clamp(stats.numberOfProjectile + playerStats.numberOfProjectile,0.5f,5);
        if(statEnum.HasFlag(StatType.Piercing)) playerStats.projectileSpeed += stats.projectileSpeed;
        if(stats.isPiercing && statEnum.HasFlag(StatType.Piercing)) playerStats.isPiercing = true;

        if(statEnum.HasFlag(StatType.CritRate)) playerStats.critRate += stats.critRate;
        if(statEnum.HasFlag(StatType.CritDamage)) playerStats.critDamage += stats.critDamage;
        if(statEnum.HasFlag(StatType.Damage)) playerStats.damage += stats.damage;
        if(statEnum.HasFlag(StatType.DamageMultiplier)) playerStats.damageMultiplier += stats.damageMultiplier;

        if(statEnum.HasFlag(StatType.MaxHealth)) playerStats.maxHealth = Mathf.Clamp(playerStats.maxHealth + stats.maxHealth,1f,1000f);
        if(statEnum.HasFlag(StatType.MaxHealthMultiplier)) playerStats.maxHealthMultiplier += stats.maxHealthMultiplier;
        if(statEnum.HasFlag(StatType.Defense)) playerStats.defense += stats.defense;
        if(statEnum.HasFlag(StatType.DefenseMultiplier)) playerStats.defenseMultiplier += stats.defenseMultiplier;
        if(statEnum.HasFlag(StatType.DmgResistance)) playerStats.dmgResistance += stats.dmgResistance;
        if(statEnum.HasFlag(StatType.DmgResistanceMultiplier)) playerStats.dmgResistanceMultiplier += stats.dmgResistanceMultiplier;

        if(statEnum.HasFlag(StatType.HealRate)) playerStats.healRate += stats.healRate;
        if(statEnum.HasFlag(StatType.HealAmount)) playerStats.healAmount += stats.healAmount;
        if(statEnum.HasFlag(StatType.InvincibleTime)) playerStats.invincibleTime += stats.invincibleTime;

        if(statEnum.HasFlag(StatType.ItemPerLevel)) playerStats.itemPerLevel =  Mathf.Clamp(playerStats.itemPerLevel + stats.itemPerLevel,0,6);
        if(statEnum.HasFlag(StatType.ItemChoicePerLevel)) playerStats.itemChoicePerLevel = Mathf.Clamp(stats.itemChoicePerLevel + playerStats.itemChoicePerLevel,1,5);

        ApplyStatsOnComponent();
    }
    public void SetStats(PlayerStatistic stats,StatType statEnum)
    {
        if(statEnum.HasFlag(StatType.ExpMultiplier)) playerStats.expMultiplier = stats.expMultiplier;
        if(statEnum.HasFlag(StatType.MoneyMultiplier)) playerStats.moneyMultiplier = stats.moneyMultiplier;
        if(statEnum.HasFlag(StatType.EnemyDropFollowRange))playerStats.enemyDropFollowRange = stats.enemyDropFollowRange;

        if(statEnum.HasFlag(StatType.MoveSpeed)) playerStats.moveSpeed = stats.moveSpeed;
        if(statEnum.HasFlag(StatType.GravityForce)) playerStats.gravityForce = stats.gravityForce;
        if(statEnum.HasFlag(StatType.JumpForce)) playerStats.jumpForce = stats.jumpForce;
        
        if(statEnum.HasFlag(StatType.FireRate)) playerStats.fireRate = stats.fireRate;
        if(statEnum.HasFlag(StatType.FireRateMultiplier)) playerStats.fireRateMultiplier = stats.fireRateMultiplier;
        if(statEnum.HasFlag(StatType.NumberOfProjectile)) playerStats.numberOfProjectile = stats.numberOfProjectile;
        if(statEnum.HasFlag(StatType.Piercing)) playerStats.projectileSpeed = stats.projectileSpeed;
        if(statEnum.HasFlag(StatType.Piercing)) playerStats.isPiercing = stats.isPiercing;

        if(statEnum.HasFlag(StatType.CritRate)) playerStats.critRate = stats.critRate;
        if(statEnum.HasFlag(StatType.CritDamage)) playerStats.critDamage = stats.critDamage;
        if(statEnum.HasFlag(StatType.Damage)) playerStats.damage = stats.damage;
        if(statEnum.HasFlag(StatType.DamageMultiplier)) playerStats.damageMultiplier = stats.damageMultiplier;

        if(statEnum.HasFlag(StatType.MaxHealth)) playerStats.maxHealth = stats.maxHealth;
        if(statEnum.HasFlag(StatType.MaxHealthMultiplier)) playerStats.maxHealthMultiplier = stats.maxHealthMultiplier;
        if(statEnum.HasFlag(StatType.Defense)) playerStats.defense = stats.defense;
        if(statEnum.HasFlag(StatType.DefenseMultiplier)) playerStats.defenseMultiplier = stats.defenseMultiplier;
        if(statEnum.HasFlag(StatType.DmgResistance)) playerStats.dmgResistance = stats.dmgResistance;
        if(statEnum.HasFlag(StatType.DmgResistanceMultiplier)) playerStats.dmgResistanceMultiplier = stats.dmgResistanceMultiplier;

        if(statEnum.HasFlag(StatType.HealRate)) playerStats.healRate = stats.healRate;
        if(statEnum.HasFlag(StatType.HealAmount)) playerStats.healAmount = stats.healAmount;
        if(statEnum.HasFlag(StatType.InvincibleTime)) playerStats.invincibleTime = stats.invincibleTime;
        
        if(statEnum.HasFlag(StatType.ItemPerLevel)) playerStats.itemPerLevel = stats.itemPerLevel;
        if(statEnum.HasFlag(StatType.ItemChoicePerLevel)) playerStats.itemChoicePerLevel = stats.itemChoicePerLevel;

        ApplyStatsOnComponent();
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
    ItemPerLevel = 1 << 24,
    ItemChoicePerLevel = 11 << 25,
}