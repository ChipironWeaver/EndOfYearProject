using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [ReadOnly]
    public List<Transform> spawnPoints;
    [ReadOnly]
    public List<GameObject> enemies;

    public List<EnemyWeight> enemiesPrefab;
    [SerializeField] private int _spawnTries;
    public float spawnDistance;
    public int waveAmount;
    
    [CurveRange(0,0,1,1)]
    public AnimationCurve enemyAmount; //will be based on the space int in the enemy weight class, an enemy with a wait of 5 will take 5
    [MinMaxSlider(0.0f, 500.0f)]
    public Vector2 enemyAmountRange;
    
    [CurveRange(0,0,1,1)]
    public AnimationCurve waveTime;
    [MinMaxSlider(10.0f, 500.0f)]
    public Vector2 waveTimeRange;
    public int waveCount;
    
    public float spawnCooldown;

    private Transform _enemyParent;

    //Waves Data
    private int _currentWave;
    private float _currentWaveTime;
    private float _currentSpawnCooldown;
    private int _enemiesNeeded;
    
    public static EnemySpawner Instance { get; private set; }

    private void Awake()
    {
        Singleton();
    }

    void Start()
    {
        foreach (Transform child in transform) 
        {
            spawnPoints.Add(child);
        }

        GameObject instantiate = new GameObject();
        instantiate.transform.parent = transform;
        instantiate.transform.position = Vector3.zero;
        instantiate.name = "EnemyParent";
        _enemyParent = instantiate.transform;
    }

    void Update()
    {
        if (waveCount <= waveAmount)
        {
            // Next Wave Check
            if (enemies.Count <= 0 && _enemiesNeeded <= 0)
            {
                NextWave();
            }
            else if (_currentWaveTime > EvaluateVector2(waveTimeRange, waveTime.Evaluate(_currentWave)))
            {
                NextWave();
            }

            //try to spawn enemy
            if (_enemiesNeeded > 0)
            {
                _currentSpawnCooldown += Time.deltaTime;
                if (_currentSpawnCooldown >= spawnCooldown)
                {
                    SpawnEnemy();
                }
            }
        }
        else
        {
            Debug.Log("Win");
            this.enabled = false;
        }
        
    }

    private void SpawnEnemy()
    {
        Transform spawnPoint = null;
        for (int _ = 0; _ < _spawnTries; _++)
        {
            Transform potentialSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            if (Vector3.Distance(PlayerInstance.playerMovementController.transform.position,potentialSpawnPoint.position) >= spawnDistance)
            {
                spawnPoint = potentialSpawnPoint;
                break;
            }
        }
        
        if (!spawnPoint)
        {
            return;
        }
        
        EnemyWeight enemy = FindEnemy();
        GameObject instantiate = Instantiate(enemy.prefab, spawnPoint.position, spawnPoint.rotation);
        instantiate.transform.parent = _enemyParent;
        _enemiesNeeded -= enemy.space;
    }
    
    private void NextWave()
    {
        _currentWave++;
        _currentWaveTime = 0f;
        _currentSpawnCooldown = 0f;
        _enemiesNeeded += (int)EvaluateVector2(enemyAmountRange , enemyAmount.Evaluate(_currentWave / (float)waveAmount));
    }
    
    private EnemyWeight FindEnemy()
    {
        float weight = 0;
        EnemyWeight findEnemy = null;
        float targetedWeight = Random.Range(0,FindCurrentWeight());

        foreach (EnemyWeight enemy in enemiesPrefab)
        {
            weight += enemy.weightCurve.Evaluate(_currentWave / (float)waveAmount);
            if (targetedWeight < weight)
            {
                findEnemy = enemy;
            }
        }
        return findEnemy;
    }
    
    private float EvaluateVector2(Vector2 vector2, float value)
    {
        float returnValue = vector2.x + (vector2.y - vector2.x) * value;
        return returnValue;
    }

    private float FindCurrentWeight()
    {
        float weight = 0;
        foreach (EnemyWeight enemy in enemiesPrefab)
        {
            weight += enemy.weightCurve.Evaluate(_currentWave / (float)waveAmount);
        }
        return weight;
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

[Serializable]
public class EnemyWeight
{
    public GameObject prefab;
    [CurveRange(0,0,1,50)]
    public AnimationCurve weightCurve;
    public int space = 1; 
}