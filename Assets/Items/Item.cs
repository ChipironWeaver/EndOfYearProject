using System;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string name;
    [ResizableTextArea]
    public string description;
    public Sprite sprite;
    public ItemRarity rarity;
    //public ItemClass family;
    public StatType statBoosts;
    
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
