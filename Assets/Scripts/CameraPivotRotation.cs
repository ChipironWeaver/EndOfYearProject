using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPivotRotation : MonoBehaviour
{
    [SerializeField] private float sensitivity;
    [Range(1,10)]
    [SerializeField] private float distance;
    [Required]
    [SerializeField] private Transform child;
    [MinMaxSlider(-100.0f, 100.0f)]
    [SerializeField] private Vector2 _angleClamp;
    private float _mouse;
    
    private void Update()
    {
        child.localPosition =new Vector3(0,0,-distance) ;
        
        Vector3 cameraRotation =
            new Vector3(Mathf.Clamp(_mouse, -30f, 30f), 0, 0) * (sensitivity  * Time.deltaTime );
        cameraRotation = transform.eulerAngles + cameraRotation;
        cameraRotation.x = ClampAngle(cameraRotation.x, _angleClamp.x, _angleClamp.y);
        
        transform.eulerAngles = cameraRotation;
    }
    
    private void OnLook(InputValue input)
    {
        _mouse = input.Get<Vector2>().y;
    }
    
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + min);
        return Mathf.Min(angle, max);
    }
}
