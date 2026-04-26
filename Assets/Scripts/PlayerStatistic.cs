using System;
using NaughtyAttributes;
using UnityEngine;
[Serializable]
public class PlayerStatistic
{
    public float expMultiplier;
    public float moneyMultiplier;
    public float enemyDropFollowRange;
    [Header("Movement")]
    public float moveSpeed;
    public float gravityForce;
    public float jumpForce;
    [Header("Projectile")]
    public float fireRate;
    public float fireRateMultiplier;
    public float numberOfProjectile;
    public float projectileSpeed;
    public bool isPiercing;
    [Header("Damage")]
    public float critRate;
    public float critDamage;
    public float damage;
    public float damageMultiplier;
    [Header("Substain")]
    public float maxHealth;
    public float maxHealthMultiplier;
    public float defense;
    public float defenseMultiplier;
    public float dmgResistance;
    public float dmgResistanceMultiplier;
    [Header("Over Time Heal")]
    public float healRate;
    public float healAmount;
    public float invincibleTime;
    [Header("Item Optaining")]
    public float itemPerLevel;
    public float itemChoicePerLevel;

    public PlayerStatistic GetInverted()
    {
        PlayerStatistic returnStats = new PlayerStatistic();

        returnStats.expMultiplier = expMultiplier * -1;
        returnStats.moneyMultiplier = moneyMultiplier  * -1;
        returnStats.enemyDropFollowRange = enemyDropFollowRange  * -1;

        returnStats.moveSpeed = moveSpeed* -1;
        returnStats.gravityForce = gravityForce* -1;
        returnStats.jumpForce = jumpForce* -1;
        
        returnStats.fireRate = fireRate* -1;
        returnStats.fireRateMultiplier = fireRateMultiplier* -1;
        returnStats.numberOfProjectile = numberOfProjectile* -1;
        returnStats.projectileSpeed = projectileSpeed* -1;

        returnStats.critRate = critRate* -1;
        returnStats.critDamage = critDamage* -1;
        returnStats.damage = damage* -1;
        returnStats.damageMultiplier = damageMultiplier* -1;

        returnStats.maxHealth = maxHealth* -1;
        returnStats.maxHealthMultiplier = maxHealthMultiplier* -1;
        returnStats.defense = defense* -1;
        returnStats.defenseMultiplier = defenseMultiplier* -1;
        returnStats.dmgResistance = dmgResistance* -1;
        returnStats.dmgResistanceMultiplier = dmgResistanceMultiplier* -1;

        returnStats.healRate = healRate* -1;
        returnStats.healAmount = healAmount* -1;
        returnStats.invincibleTime = invincibleTime* -1;

        returnStats.itemPerLevel = itemPerLevel* -1;
        returnStats.itemChoicePerLevel = itemChoicePerLevel* -1;

        return returnStats;
    }
}