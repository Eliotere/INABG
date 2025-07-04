using System.Data;
using UnityEngine;
using UnityEngine.Rendering;

public class GroundController : MonoBehaviour
{
    [SerializeField]
    private float m_groundDistanceTolerance;

    [SerializeField]
    private LayerMask m_groundLayerMask;

    [SerializeField]
    private SphereCollider m_sphereCollider;
    public bool isGrounded { get; private set; }

    public float? distanceToGround { get; private set; }

    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float sphereCastRadius = m_sphereCollider.radius - 0.1f;
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
            distanceToGround = Vector3.Distance(transform.position, hitInfo.point);
        }
        else
        {
            distanceToGround = null;
        }
        isGrounded = isGroundedBelow && distanceToGround <= m_groundDistanceTolerance;
    }
}
