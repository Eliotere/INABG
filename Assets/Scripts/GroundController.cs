using System;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [SerializeField]
    private float m_groundDistanceTolerance;

    [SerializeField]
    private LayerMask m_groundLayerMask;

    [SerializeField]
    private SphereCollider _sphereCollider;
    private bool _wasGrounded = false;
    public bool _isGrounded { get; private set; }
    public event Action OnPlayerLanded;

    public float? _distanceToGround { get; private set; }

    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float sphereCastRadius = _sphereCollider.radius - 0.1f;
        Vector3 sphereCastOrigin = transform.position;
        bool isGroundedBelow = Physics.SphereCast(
            sphereCastOrigin,
            sphereCastRadius,
            transform.up * -1,
            out RaycastHit hitInfo,
            1000,
            m_groundLayerMask,
            QueryTriggerInteraction.Ignore
        );

        if (isGroundedBelow)
        {
            _distanceToGround = Vector3.Distance(transform.position, hitInfo.point);
        }
        else
        {
            _distanceToGround = null;
        }

        // Define isGrounded State
        _isGrounded = isGroundedBelow && _distanceToGround <= m_groundDistanceTolerance;

        if (_isGrounded && !_wasGrounded)
        {
            OnPlayerLanded?.Invoke();
        }
        _wasGrounded = _isGrounded;
    }
}
