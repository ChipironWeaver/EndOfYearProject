using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShootController : MonoBehaviour
{
    public float fireRate;
    public float numberOfProjectiles;
    public float range;
    public float projectileSpeed;
    [SerializeField]
    private bool _hasAutoTarget;
    [SerializeField,Tag,ShowIf("_hasAutoTarget")]
    private string _autoTargetTag;
    [SerializeField,ShowIf("_hasAutoTarget"),ReadOnly]
    private bool _splitTargetWithMultipleShots;
    [SerializeField,Required]
    private GameObject _projectilePrefab;
    [SerializeField,Required]
    private Transform _projectileSpawnPoint;
    [SerializeField,ReadOnly]
    public float currentCooldown;
    
    private void Update()
    {
        currentCooldown += Time.deltaTime;
        if (currentCooldown >= 1/fireRate)
        {
            InstantiateBullets();
            currentCooldown = 0;
        }
    }

    private void InstantiateBullets()
    {
        Vector3 targetDirection = _projectileSpawnPoint.forward;
        GameObject target = FindOneClosestEnemy();
        if (_hasAutoTarget)
        {
            if (target)
            {
                targetDirection = target.transform.position - _projectileSpawnPoint.position;
                targetDirection.Normalize();
            }
        }
        
        Vector3 diff = target.transform.position - transform.position;
        float curDistance = diff.sqrMagnitude;
        print(curDistance);
        if (curDistance < range)
        {
            if (Physics.Raycast(_projectileSpawnPoint.position, targetDirection, out RaycastHit hit))
            {
                if (hit.collider.tag == _autoTargetTag)
                {
                    for (int i = 0; i < RandomRound(numberOfProjectiles); i++)
                    {
                        GameObject instantiate =
                            Instantiate(_projectilePrefab, _projectileSpawnPoint.position, Quaternion.identity);
                        instantiate.GetComponent<Rigidbody>()
                            .AddForce(targetDirection * projectileSpeed, ForceMode.Impulse);
                    }
                }
            }
        }
    }
    
    public GameObject FindOneClosestEnemy()
    {
        List<GameObject> gos;
        gos = EnemySpawner.Instance.enemies;
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    
    private int RandomRound(float num)
    {
        int number = Mathf.FloorToInt(num);
        float rest = num - number;
        if (rest > 0)
        {
            if (rest * 100 > Random.Range(0, 100))
            {
                number++;
            }
        }
        return number;
    }
}
