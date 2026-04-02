using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShootController : MonoBehaviour
{
    public float cooldown;
    public float numberOfProjectiles;
    public float bulletPower;
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
    private float _currentCooldown;
    
    private void Update()
    {
        _currentCooldown += Time.deltaTime;
        if (_currentCooldown >= cooldown)
        {
            InstantiateBullets();
            _currentCooldown = 0;
        }
    }

    private void InstantiateBullets()
    {
        Vector3 targetDirection = _projectileSpawnPoint.forward;
        if (_hasAutoTarget)
        {
            GameObject target = FindOneClosestEnemy();
            if (target != null)
            {
                targetDirection = target.transform.position - _projectileSpawnPoint.position;
                targetDirection.Normalize();
            }
        }
        
        
        for (int i = 0; i < RandomRound(numberOfProjectiles) ; i++)
        {
            GameObject instantiate = Instantiate(_projectilePrefab, _projectileSpawnPoint.position, Quaternion.identity);
            instantiate.GetComponent<Rigidbody>().AddForce(targetDirection * bulletPower, ForceMode.Impulse);
        }
    }
    
    public GameObject FindOneClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(_autoTargetTag);
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
