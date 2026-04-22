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
                    _rb.linearVelocity = PlayerDirection * _moveSpeed;
                }
                else
                {
                    _rb.linearVelocity = _rb.linearVelocity / 2;
                }
            }
        }
        else
        {
            _detectRange = PlayerInstance.playerStatisticController.playerStats.enemyDropFollowRange;
            if (Vector3.Distance(transform.position, PlayerPosition) <= _detectRange)
            {
                _goToPlayer = true;
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.coral;
        
        Gizmos.DrawWireSphere(transform.position, _detectRange);
    }
}
