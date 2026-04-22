using System;
using NaughtyAttributes;
using UnityEngine;
[Serializable]
public class PlayerStatistic
{
    public float expMultiplier;
    public float moneyMultiplier;
    public float enemyDropFollowRange;
    [HorizontalLine(color: EColor.White)]
    public float moveSpeed;
    public float gravityForce;
    public float jumpForce;
    [HorizontalLine(color: EColor.White)]
    public float fireRate;
    public float fireRateMultiplier;
    public float numberOfProjectile;
    public float projectileSpeed;
    public bool isPiercing;
    [HorizontalLine(color: EColor.White)]
    public float critRate;
    public float critDamage;
    public float damage;
    public float damageMultiplier;
    [HorizontalLine(color: EColor.White)]
    public float maxHealth;
    public float maxHealthMultiplier;
    public float defense;
    public float defenseMultiplier;
    public float dmgResistance;
    public float dmgResistanceMultiplier;
    [HorizontalLine(color: EColor.White)]
    public float healRate;
    public float healAmount;
    public float invincibleTime;
}