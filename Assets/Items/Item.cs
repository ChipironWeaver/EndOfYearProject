using System;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;
    [ResizableTextArea]
    public string description;
    public Sprite sprite;
    public ItemRarity rarity;
    //public ItemClass family;
    public StatType statBoosts;
    public bool setStats;
    
    [ShowIf("statBoosts",StatType.ExpMultiplier),BoxGroup("Miscellaneous")]
    public float expMultiplier;
    [ShowIf("statBoosts",StatType.MoneyMultiplier),BoxGroup("Miscellaneous")]
    public float moneyMultiplier;
    [ShowIf("statBoosts",StatType.EnemyDropFollowRange),BoxGroup("Miscellaneous")]
    public float enemyDropFollowRange;
    [ShowIf("statBoosts",StatType.MoveSpeed),BoxGroup("Movement")]
    public float moveSpeed;
    [ShowIf("statBoosts",StatType.GravityForce),BoxGroup("Movement")]
    public float gravityForce;
    [ShowIf("statBoosts",StatType.JumpForce),BoxGroup("Movement")]
    public float jumpForce;
    [ShowIf("statBoosts",StatType.FireRate),BoxGroup("Projectile")]
    public float fireRate;
    [ShowIf("statBoosts",StatType.FireRateMultiplier),BoxGroup("Projectile")]
    public float fireRateMultiplier;
    [ShowIf("statBoosts",StatType.NumberOfProjectile),BoxGroup("Projectile")]
    public float numberOfProjectile;
    [ShowIf("statBoosts",StatType.ProjectileSpeed),BoxGroup("Projectile")]
    public float projectileSpeed;
    [ShowIf("statBoosts",StatType.Piercing),BoxGroup("Projectile")]
    public bool isPiercing;
    [ShowIf("statBoosts",StatType.CritRate),BoxGroup("Damage")]
    public float critRate;
    [ShowIf("statBoosts",StatType.CritDamage),BoxGroup("Damage")]
    public float critDamage;
    [ShowIf("statBoosts",StatType.Damage),BoxGroup("Damage")]
    public float damage;
    [ShowIf("statBoosts",StatType.DamageMultiplier),BoxGroup("Damage")]
    public float damageMultiplier;
    [ShowIf("statBoosts",StatType.MaxHealth),BoxGroup("Sustain")]
    public float maxHealth;
    [ShowIf("statBoosts",StatType.MaxHealthMultiplier),BoxGroup("Sustain")]
    public float maxHealthMultiplier;
    [ShowIf("statBoosts",StatType.Defense),BoxGroup("Sustain")]
    public float defense;
    [ShowIf("statBoosts",StatType.DefenseMultiplier),BoxGroup("Sustain")]
    public float defenseMultiplier;
    [ShowIf("statBoosts",StatType.DmgResistance),BoxGroup("Sustain")]
    public float dmgResistance;
    [ShowIf("statBoosts",StatType.DmgResistanceMultiplier),BoxGroup("Sustain")]
    public float dmgResistanceMultiplier;
    [ShowIf("statBoosts",StatType.HealRate),BoxGroup("Over Time Heal")]
    public float healRate;
    [ShowIf("statBoosts",StatType.HealAmount),BoxGroup("Over Time Heal")]
    public float healAmount;
    [ShowIf("statBoosts",StatType.InvincibleTime),BoxGroup("Over Time Heal")]
    public float invincibleTime;

    public PlayerStatistic GetPlayerStats()
    {
        PlayerStatistic playerStats = new PlayerStatistic();

        if(statBoosts.HasFlag(StatType.ExpMultiplier)) playerStats.expMultiplier = expMultiplier;
        if(statBoosts.HasFlag(StatType.MoneyMultiplier)) playerStats.moneyMultiplier = moneyMultiplier;
        if(statBoosts.HasFlag(StatType.EnemyDropFollowRange))playerStats.enemyDropFollowRange = enemyDropFollowRange;

        if(statBoosts.HasFlag(StatType.MoveSpeed)) playerStats.moveSpeed = moveSpeed;
        if(statBoosts.HasFlag(StatType.GravityForce)) playerStats.gravityForce = gravityForce;
        if(statBoosts.HasFlag(StatType.JumpForce)) playerStats.jumpForce = jumpForce;
        
        if(statBoosts.HasFlag(StatType.FireRate)) playerStats.fireRate = fireRate;
        if(statBoosts.HasFlag(StatType.FireRateMultiplier)) playerStats.fireRateMultiplier = fireRateMultiplier;
        if(statBoosts.HasFlag(StatType.NumberOfProjectile)) playerStats.numberOfProjectile = numberOfProjectile;
        if(statBoosts.HasFlag(StatType.Piercing)) playerStats.projectileSpeed = projectileSpeed;
        if(statBoosts.HasFlag(StatType.Piercing)) playerStats.isPiercing = isPiercing;

        if(statBoosts.HasFlag(StatType.CritRate)) playerStats.critRate = critRate;
        if(statBoosts.HasFlag(StatType.CritDamage)) playerStats.critDamage = critDamage;
        if(statBoosts.HasFlag(StatType.Damage)) playerStats.damage = damage;
        if(statBoosts.HasFlag(StatType.DamageMultiplier)) playerStats.damageMultiplier = damageMultiplier;

        if(statBoosts.HasFlag(StatType.MaxHealth)) playerStats.maxHealth = maxHealth;
        if(statBoosts.HasFlag(StatType.MaxHealthMultiplier)) playerStats.maxHealthMultiplier = maxHealthMultiplier;
        if(statBoosts.HasFlag(StatType.Defense)) playerStats.defense = defense;
        if(statBoosts.HasFlag(StatType.DefenseMultiplier)) playerStats.defenseMultiplier = defenseMultiplier;
        if(statBoosts.HasFlag(StatType.DmgResistance)) playerStats.dmgResistance = dmgResistance;
        if(statBoosts.HasFlag(StatType.DmgResistanceMultiplier)) playerStats.dmgResistanceMultiplier = dmgResistanceMultiplier;

        if(statBoosts.HasFlag(StatType.HealRate)) playerStats.healRate = healRate;
        if(statBoosts.HasFlag(StatType.HealAmount)) playerStats.healAmount = healAmount;
        if(statBoosts.HasFlag(StatType.InvincibleTime)) playerStats.invincibleTime = invincibleTime;

        return playerStats;
    }
}

[Serializable]
public enum ItemRarity
{
    Common,
    Rare,
    Epic,
    Legendary,
}

[Serializable]
public enum ItemClass
{
}
