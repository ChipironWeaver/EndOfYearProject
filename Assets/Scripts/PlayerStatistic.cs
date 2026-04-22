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
}