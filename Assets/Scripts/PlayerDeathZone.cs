using UnityEngine;

public class PlayerDeathZone : MonoBehaviour
{

    [SerializeField] private float _yDistanceAutoKill;
    void Update()
    {
        if(PlayerInstance.Instance.transform.position.y < _yDistanceAutoKill)
        {
            PlayerInstance.healthController.Death();
        }
    }
}
