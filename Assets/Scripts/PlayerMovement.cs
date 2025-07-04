using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public InputActionAsset m_inputActions;

    // Ground Movement
    private InputAction m_moveAction;
    private Vector2 m_moveAmount;

    private Rigidbody m_rigidbody;

    public float moveSpeed = 5f;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.freezeRotation = true;
    }

    void Update()
    {
        m_moveAmount = m_moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void OnEnable()
    {
        // Enable the action map
        var PlayerMap = m_inputActions.FindActionMap("Player");
        PlayerMap.Enable();
    }

    private void OnDisable()
    {
        // Disable the action map
        var PlayerMap = m_inputActions.FindActionMap("Player");
        PlayerMap.Disable();
    }

    private void Awake()
    {
        m_moveAction = InputSystem.actions.FindAction("Move");

        m_rigidbody = GetComponent<Rigidbody>();
    }

    void Move()
    {
        // Use local space directions 2D
        Vector3 moveDirection = transform.forward * m_moveAmount.y + transform.right * m_moveAmount.x;
        m_rigidbody.MovePosition(m_rigidbody.position + moveDirection * moveSpeed * Time.deltaTime);
    }
}
