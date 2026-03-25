using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPivotRotation : MonoBehaviour
{
    [SerializeField] private float sensitivity;
    [Range(1,10)][SerializeField] private float distance;
    [SerializeField] private Transform child;

    private void Update()
    {
        child.localPosition =new Vector3(0,0,-distance) ;
    }
    
    private void OnLook(InputValue input)
    {
        float mouse = input.Get<Vector2>().y;
        transform.Rotate(new Vector3(-mouse*sensitivity,0,0));
        
    }
}
