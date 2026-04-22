using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomInstantiate : MonoBehaviour
{
    [SerializeField,MinMaxSlider(0.0f, 25.0f)] Vector2 _numberRange;
    [SerializeField] private List<PrefabWeight> _prefabWeights;

    public void InstantiateRandom()
    {
        int numberOfInstantiate = (int)Random.Range(_numberRange.x , _numberRange.y); 
        float totalWeight = 0;
        foreach (PrefabWeight prefabWeight in _prefabWeights)
        {
            totalWeight += prefabWeight.weight;
        }
        for(int i = 1; i <= numberOfInstantiate ; i++)
        {
            GameObject toInstantiate = null;
            float selectedWeight = Random.Range(0, totalWeight);
            float currentWeight = 0;
            
            foreach (PrefabWeight prefabWeight in _prefabWeights)
            {
                toInstantiate = prefabWeight.prefab;
                currentWeight += prefabWeight.weight;
                if (currentWeight >= selectedWeight)
                {
                    break;
                }
            }
            Instantiate(toInstantiate);
        }
    }
    
    [Serializable]
    private class PrefabWeight
    {
        public GameObject prefab;
        public float weight;
    }
}

