using System;
using UnityEngine;

public class EnemyDropMagnetism : MonoBehaviour
{
    private bool _goToPlayer;
    [SerializeField] private float _detectRange;
    [SerializeField] private float _moveSpeed;

    private Rigidbody _rb;

    public void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void FixedUpdate()
    {
        Vector3 PlayerPosition = PlayerInstance.Instance.transform.position;
        if (_goToPlayer)
        {
            
            Vector3 PlayerDirection = PlayerPosition - transform.position;
            PlayerDirection.Normalize();
            if (Physics.Raycast(transform.position, PlayerDirection, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    print("i see the player");
                    _rb.linearVelocity = PlayerDirection * _moveSpeed;
                }
                else
                {
                    _rb.velocity = _rb.velocity / 2;
                    print("i don't see player");
                }
            }
        }
        else
        {
            _detectRange = PlayerInstance.playerStatisticController.playerStats.enemyDropFollowRange;
            if (Vector3.Distance(transform.position, PlayerPosition) <= _detectRange)
            {
                _goToPlayer = true;
                print("Go to Player");
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.coral;
        
        Gizmos.DrawWireSphere(transform.position, _detectRange);
    }
}
