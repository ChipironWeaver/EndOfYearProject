using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public ItemRarity itemRarity;
    //public ItemClass itemClass;
    public PlayerStatistic statsBoost;
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
