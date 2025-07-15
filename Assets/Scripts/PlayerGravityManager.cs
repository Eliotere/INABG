using UnityEngine;

public class PlayerGravityManager : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField]
    private float _gravityStrengh = 10.0f;

    [SerializeField]
    private Quaternion _gravityRotation; // This is the rotation to align with gravity

    [SerializeField]
    private Vector3 previousPosition;

    [SerializeField]
    private Vector3 displacement;

    [SerializeField]
    private Vector3 calculatedVelocity;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        // Set default gravity direction (local down)
        SetGravityFromDirection(-transform.up);

        previousPosition = transform.position;
    }

    // FixedUpdate is called on a fixed time step
    void FixedUpdate()
    {
        Fall();

        // Calculate displacement and velocity based on previous position
        displacement = transform.position - previousPosition;
        calculatedVelocity = displacement / Time.fixedDeltaTime;

        // Update for next frame
        previousPosition = transform.position;
    }

    private void Fall()
    {
        Vector3 gravityDirection = _gravityRotation * Vector3.down; // Aligned "down" vector
        _rigidbody.AddForce(gravityDirection * _gravityStrengh, ForceMode.Acceleration);
    }

    public Vector3 GetDisplacement()
    {
        return displacement;
    }

    public float GetVelocityMagnitude()
    {
        return calculatedVelocity.magnitude;
    }

    public Vector3 GetVelocity()
    {
        return calculatedVelocity;
    }

    public Vector3 VelocityDifference(Vector3 otherVelocity)
    {
        return calculatedVelocity - otherVelocity;
    }

    public void SetGravityFromDirection(Vector3 newGravityDir)
    {
        newGravityDir.Normalize();
        _gravityRotation = Quaternion.FromToRotation(Vector3.up, -newGravityDir);
    }

    public void SetGravityDirection(Quaternion newGravityQuat)
    {
        _gravityRotation = newGravityQuat;
    }

    public Vector3 GetGravityDirection()
    {
        return _gravityRotation * Vector3.down;
    }

    public Quaternion GetGravityRotation()
    {
        return _gravityRotation;
    }
}