using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent _agent;
    Transform _target;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_target == null)
        {
            _target = PlayerInstance.Instance.transform;
        }
    }
    
    void Update()
    {
        _agent.SetDestination(_target.position);
    }
}
