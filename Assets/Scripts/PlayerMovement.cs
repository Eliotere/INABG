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
    private InputAction m_jumpAction;
    [SerializeField]
    private float _jumpStrength = 1000.0f;
    private float _fallStrengh = 10.0f;

    private Rigidbody _rigidbody;

    [SerializeField]
    private float moveSpeed = 5f;

    private void Awake()
    {
        var playerMap = _inputActions.FindActionMap("Player");

        _moveAction = playerMap.FindAction("Move");
        m_jumpAction = playerMap.FindAction("Jump");

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
        Fall();
    }

    private void OnEnable()
    {
        // Enable the action map
        var playerMap = _inputActions.FindActionMap("Player");
        playerMap.Enable();

        m_jumpAction.performed += Jump;
    }

    private void OnDisable()
    {
        // Unregister the callback and disable the action
        if (m_jumpAction != null)
        {
            m_jumpAction.performed -= Jump;
            m_jumpAction.Disable();
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

    private void Fall()
    {
        _rigidbody.AddForce(_rigidbody.transform.up * -1 * _fallStrengh);
    }

    private void Jump(InputAction.CallbackContext context)
    {

        if (_groundController.isGrounded)
        {
            Vector3 jumpVector = transform.up * _jumpStrength;
            _rigidbody.AddForce(jumpVector);
        }

    }
}
