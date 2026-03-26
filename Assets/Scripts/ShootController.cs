using System;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShootController : MonoBehaviour
{
    public float cooldown;
    public float numberOfProjectiles;
    public float bulletPower;
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
            InstantiateBullet();
            _currentCooldown = 0;
        }
    }

    private void InstantiateBullet()
    {
        for (int i = 0; i < RandomRound(numberOfProjectiles) ; i++)
        {
            GameObject instantiate = Instantiate(_projectilePrefab, _projectileSpawnPoint.position, Quaternion.identity);
            instantiate.GetComponent<Rigidbody>().AddForce(_projectileSpawnPoint.forward * bulletPower, ForceMode.Impulse);
            print(_projectileSpawnPoint.forward * bulletPower);
        }
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
