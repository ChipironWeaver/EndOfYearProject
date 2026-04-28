using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> heldItems{get; private set;} = new List<Item>();

    public void AddItem(Item item)
    {
        print(item.name);
        heldItems.Add(item);
        print(heldItems.Count());
        if(item.setStats) PlayerInstance.playerStatisticController.SetStats(item.GetPlayerStats(), item.statBoosts);
        else PlayerInstance.playerStatisticController.AddStats(item.GetPlayerStats(), item.statBoosts);
        
    }

    public void RemoveItem(Item item)
    {
        foreach(Item _item in heldItems) print(_item.name);
        if (heldItems.Contains(item))
        {
            heldItems.Remove(item);
            PlayerInstance.playerStatisticController.AddStats(item.GetPlayerStats().GetInverted(), item.statBoosts);
        }
        else
        {
            Debug.LogWarning("Tried removing an item that the player does not have : " + item.itemName);
        }
    }
}
