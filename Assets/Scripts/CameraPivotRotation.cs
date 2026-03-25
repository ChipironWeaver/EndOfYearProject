using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPivotRotation : MonoBehaviour
{
    [SerializeField] Transform pivot;
    [SerializeField] private float sensitivity;

    private void OnLook(InputValue input)
    {
        float mouse = input.Get<Vector2>().x;
        pivot.Rotate(new Vector3(0,-mouse*sensitivity,0));
    }
}
