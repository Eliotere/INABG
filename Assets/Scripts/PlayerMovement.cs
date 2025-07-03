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
        m_inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        m_inputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        m_moveAction = InputSystem.actions.FindAction("Move");

        m_rigidbody = GetComponent<Rigidbody>();
    }

    void Move()
    {
        Vector3 moveDirection = new Vector3(m_moveAmount.x, 0f, m_moveAmount.y); // Convert move m_moveAmount into a Vector3 (to compute next operation)
        m_rigidbody.MovePosition(m_rigidbody.position + moveDirection * moveSpeed * Time.deltaTime);
    }
}
