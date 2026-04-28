using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootPoolController : MonoBehaviour
{
    
    [SerializeField] private Item[] _itemList;
    [SerializeField] private RarityWeightPerWave[] _rarityWeightPerWaves; 
    public static LootPoolController Instance;

    void Awake()
    {
        Singleton();
    }
    //select rarity
    //pick a random item from this rarity appened the items in a list, get a random index

    public Item GetRandomItem()
    {
        Item finalItem = _itemList[0];
        float totalRarityWeight = 0;
        foreach(RarityWeightPerWave weight in _rarityWeightPerWaves)
        {
            totalRarityWeight += GetRarityWeight(weight);   
        }
        float targetedWeight = Random.Range(0,totalRarityWeight * 100)/100;
        float currentWeight = 0;
        ItemRarity targetRarity = ItemRarity.Common;
        foreach(RarityWeightPerWave weight in _rarityWeightPerWaves)
        {
            currentWeight += GetRarityWeight(weight);   
            if(currentWeight > targetedWeight)
            {
                targetRarity = weight.itemRarity;
                break;
            }
        }
        List<Item> potentialItem = new List<Item>();
        foreach(Item item in _itemList)
        {
            if(item.rarity == targetRarity)
            {
                potentialItem.Add(item);
            }
        }

        if(potentialItem.Count > 0)
        {
            finalItem = potentialItem[Random.Range(0,potentialItem.Count)];
        }
        return finalItem;
    }

    public float GetRarityWeight(RarityWeightPerWave rarity)
    {
        if(rarity != null)
        {
            return EvaluateVector2(rarity.minMaxWeight,rarity.normalizedWeight,EnemySpawner.Instance.currentWave / EnemySpawner.Instance.waveAmount);
        }
        return 0;
    }

    public float EvaluateVector2(Vector2 vector, AnimationCurve curve, float time)
    {
        return vector.x + (vector.y - vector.x)*Mathf.Clamp(curve.Evaluate(time),0,1);
    }

    [Serializable]
    public class RarityWeightPerWave
    {
        public ItemRarity itemRarity;
        [CurveRange(0,0,1,1)]
        public AnimationCurve normalizedWeight;
        public Vector2 minMaxWeight;
    }

        void Singleton()
    {
        if (Instance !=null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
