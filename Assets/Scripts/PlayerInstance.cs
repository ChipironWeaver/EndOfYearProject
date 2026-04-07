using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    public static PlayerInstance Instance { get; private set; }
    public static ShootController shootController;
    public static HealthController healthController;
    public static ProjectileController projectileController;
    public static PlayerStatisticController playerStatisticController;
    public static PlayerMovementController playerMovementController;

    void Awake()
    {
        Singleton();
    }
    
    void Start()
    {
        shootController = GetComponent<ShootController>();
        healthController = GetComponent<HealthController>();
        projectileController = GetComponent<ProjectileController>();
        playerStatisticController = GetComponent<PlayerStatisticController>();
        playerMovementController = GetComponent<PlayerMovementController>();
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
