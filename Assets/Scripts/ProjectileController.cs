using NaughtyAttributes;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Tag]
    public string groundTag;
    [Tag]
    public string damageTag;
    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 3f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(groundTag))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag(damageTag))
        {
            Debug.Log("Not Implemented");
            Destroy(gameObject);
        }
    }
}
