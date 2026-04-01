using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [SerializeField] Vector3 rotateSpeed = Vector3.zero;
    void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime);
    }
}
