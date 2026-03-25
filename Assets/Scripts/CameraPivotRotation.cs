using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPivotRotation : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] private float sensitivity;
    [SerializeField] private float distance;

    private void OnLook(InputValue input)
    {
        float mouse = input.Get<Vector2>().y;
        transform.Rotate(new Vector3(-mouse*sensitivity,0,0));

    }
}
