using UnityEngine;

public class PlayerGravityManager : MonoBehaviour
{

    private Rigidbody _rigidbody;
    [SerializeField]
    private float _gravityStrengh = 10.0f;
    [SerializeField]
    private Quaternion _gravityRotation; // This is the rotation to align with gravity


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       _rigidbody = GetComponent<Rigidbody>();

        // Default gravity direction: local down (Vector3.down in world space)
        SetGravityFromDirection(-transform.up);
        
        ///////////////////////////////////////////////////////////////////
        // WILL ADD DEPLACEMENTS BASED ON ARENA ROTATION AND DSIPLACEMENT
        ///////////////////////////////////////////////////////////////////
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Fall();
    }

    private void Fall()
    {
        // Apply force in the rotated direction (down direction)
        Vector3 gravityDirection = _gravityRotation * Vector3.up * -1f;
        _rigidbody.AddForce(gravityDirection * _gravityStrengh, ForceMode.Acceleration);
    }

    // Set gravity rotation from a direction vector
    public void SetGravityFromDirection(Vector3 newGravityDir)
    {
        newGravityDir.Normalize();
        _gravityRotation = Quaternion.FromToRotation(Vector3.up, -newGravityDir);
    }

    // Set gravity rotation from a direction vector
    public void SetGravityDirection(Quaternion newGravityQuat)
    {
        _gravityRotation = newGravityQuat;
    }

    // Returns the direction gravity is pulling in
    public Vector3 GetGravityDirection()
    {
        return _gravityRotation * Vector3.up * -1f;
    }

    // Returns the rotation representing current gravity alignment
    public Quaternion GetGravityRotation()
    {
        return _gravityRotation;
    }
}
