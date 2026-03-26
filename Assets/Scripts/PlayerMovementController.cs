using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [BoxGroup("MouseSettings"),SerializeField,Range(10,100)]
    private float _sensitivity;
    
    [MinMaxSlider(-100.0f, 100.0f),BoxGroup("MouseSettings"),SerializeField]
    private Vector2 _angleClamp;
    
    [BoxGroup("MouseSettings"),SerializeField,Required]
    private Transform _pivot;
    
    [BoxGroup("MouseSettings"),SerializeField,Required]
    private Transform _cameraFollowPoint;
    
    [BoxGroup("MouseSettings"),Range(1,10),SerializeField]
    private float _cameraDistance;
    
    [BoxGroup("Movement Settings")]
    public float moveSpeed = 1;
    
    [BoxGroup("Movement Settings")]
    public float gravityForce = -9.8f;
    
    [BoxGroup("Movement Settings")]
    public float jumpForce;
    
    [HideInInspector]
    public bool isGrounded;
    
    
    private Vector3 _move = Vector3.zero;
    private Vector2 _mouse = Vector2.zero;
    private CharacterController _characterController;
    private float _verticalVelocity;
    private Transform _transform;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {   
        Vector3 moveDirection = transform.TransformDirection(_move);
        moveDirection *= moveSpeed;
        
        isGrounded = _characterController.isGrounded;
        if (isGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = -2f;
        }
        
        _verticalVelocity += gravityForce * Time.deltaTime;
        moveDirection.y = _verticalVelocity;
        
        _characterController.Move(moveDirection * Time.deltaTime);
        
        transform.Rotate(new Vector3(0,-_mouse.x * _sensitivity * Time.deltaTime,0));
        _cameraFollowPoint.localPosition =new Vector3(0,0,-_cameraDistance) ;
        
        Vector3 cameraRotation = new Vector3(Mathf.Clamp(-_mouse.y, -30f, 30f), 0, 0) * (_sensitivity * Time.deltaTime );
        cameraRotation = _pivot.eulerAngles + cameraRotation;
        cameraRotation.x = ClampAngle(cameraRotation.x, _angleClamp.x, _angleClamp.y);
        
        _pivot.eulerAngles = cameraRotation;
    }

    void OnMove(InputValue value)
    {
        _move = new Vector3(value.Get<Vector2>().x, 0, value.Get<Vector2>().y);
    }
    
    private void OnJump(InputValue input)
    {
        if (isGrounded)
        {
            _verticalVelocity = Mathf.Sqrt(jumpForce * -2f * gravityForce);
        }
    }
    
    private void OnLook(InputValue input)
    {
        _mouse = input.Get<Vector2>();
        
        
    }
    
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + min);
        return Mathf.Min(angle, max);
    }
}
