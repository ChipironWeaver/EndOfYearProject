using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 3f)
            Destroy(gameObject);
    }

}
