using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    public static PlayerInstance Instance { get; private set; }
    public static ShootController shootController;
    public static HealthController healthController;
    public static PlayerStatisticController playerStatisticController;
    public static PlayerMovementController playerMovementController;
    public static PlayerLevelController playerLevelController;

    void Awake()
    {
        Singleton();
    }
    
    void Start()
    {
        shootController = GetComponent<ShootController>();
        healthController = GetComponent<HealthController>();
        playerStatisticController = GetComponent<PlayerStatisticController>();
        playerMovementController = GetComponent<PlayerMovementController>();
        playerLevelController = GetComponent<PlayerLevelController>();
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
