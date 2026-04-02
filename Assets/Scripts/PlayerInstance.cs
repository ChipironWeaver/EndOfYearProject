using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    public static PlayerInstance Instance { get; private set; }
    public static ShootController shootController;
    public static HealthController healthController;
    public static ProjectileController projectileController;

    void Start()
    {
        Singleton();
        shootController = GetComponent<ShootController>();
        healthController = GetComponent<HealthController>();
        projectileController = GetComponent<ProjectileController>();
        
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
