using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public InputActionAsset _inputActions;
    private GroundController _groundController;

    // Ground Movement
    private InputAction _moveAction;
    private Vector2 _moveAmount;

    // Jump Action
    private InputAction _jumpAction;
    [SerializeField]
    private float _jumpStrength = 10.0f;

    [SerializeField]
    private int _maxNumberOfDoubleJump = 1;
    [SerializeField]
    private int _currentNumberOfDoubleJump = 1;

    private Rigidbody _rigidbody;

    [SerializeField]
    private float moveSpeed = 5f;

    private void Awake()
    {
        var playerMap = _inputActions.FindActionMap("Player");

        _moveAction = playerMap.FindAction("Move");
        _jumpAction = playerMap.FindAction("Jump");

        _groundController = GetComponent<GroundController>();
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }

    void Update()
    {
        _moveAmount = _moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void OnEnable()
    {
        // Enable the action map
        var playerMap = _inputActions.FindActionMap("Player");
        playerMap.Enable();

        _jumpAction.performed += Jump;

        _groundController.OnPlayerLanded += Land;
    }

    private void OnDisable()
    {
        // Unregister the callback and disable the action
        if (_jumpAction != null)
        {
            _jumpAction.performed -= Jump;
            _jumpAction.Disable();

            _groundController.OnPlayerLanded -= Land;
        }

        // Disable the action map
        var playerMap = _inputActions.FindActionMap("Player");
        playerMap.Disable();
    }

    private void Move()
    {
        // Use local space directions 2D
        Vector3 moveDirection = transform.forward * _moveAmount.y + transform.right * _moveAmount.x;
        _rigidbody.MovePosition(_rigidbody.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    private void Jump(InputAction.CallbackContext context)
    {

        if (_groundController._isGrounded) // If jumping from ground
        {
            Vector3 jumpVector = transform.up * _jumpStrength;
            _rigidbody.linearVelocity = jumpVector;
            //_rigidbody.AddForce(jumpVector);
        }
        else if (_currentNumberOfDoubleJump > 0) // If double jumping
        {
            _currentNumberOfDoubleJump--;
            Vector3 jumpVector = transform.up * _jumpStrength;
            _rigidbody.linearVelocity = jumpVector;
        }

    }

    private void Land()
    {
        _currentNumberOfDoubleJump = _maxNumberOfDoubleJump;
    }
}
