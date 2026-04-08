using NaughtyAttributes;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Tag]
    public string groundTag;
    [Tag]
    public string damageTag;
    [SerializeField]
    private bool isPiercingOveride;
    [SerializeField] 
    private float _damageCooldown = 0.5f;

    private bool _canDamage;
    private float _lastDamage = 0;
    private float _timer;

    private void Start()
    {
        _lastDamage -= _damageCooldown;
    }
    
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 3f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(damageTag) && _timer > _lastDamage + _damageCooldown)
        {
            other.GetComponent<EnemyHealthManager>().TakeDamage(PlayerInstance.playerStatisticController.GetDamage());
            if(!(PlayerInstance.playerStatisticController.playerStats.isPiercing | isPiercingOveride))
            {
                Destroy(gameObject);
            }
            _lastDamage = _timer;
        }
        if (other.CompareTag(groundTag))
        {
            Destroy(gameObject);
        }
    }
}
