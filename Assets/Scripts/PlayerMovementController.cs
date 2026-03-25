using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float sensitivity;
    public float moveSpeed = 1;
    public float gravityForce = -9.8f;
    public bool isGrounded;
    public float jumpForce;
    
    private Vector3 _move = Vector3.zero;
    private CharacterController _characterController;
    private float _verticalVelocity;
    private Transform _transform;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();
    }
    
    void Update()
    {   
        MovementUpdate();
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
        float mouse = input.Get<Vector2>().x;
        _transform.Rotate(new Vector3(0,-mouse*sensitivity,0));
    }
    
    void MovementUpdate()
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
    }
}
