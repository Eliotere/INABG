
using UnityEngine;

public class ArenaGravityManager : MonoBehaviour
{

    private Vector3 _previousPosition;
    private Quaternion _previousRotation;

    [SerializeField]
    private ArenaManager _arenaManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _previousPosition = transform.position;
        _previousRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(_previousRotation);
        Vector3 deltaPosition = transform.position - _previousPosition;

        foreach (GameObject entity in _arenaManager.getEntities())
        {
            Rigidbody rb = entity.GetComponent<Rigidbody>();
            if (!rb) continue;

            // Local offset from the platform’s *previous* frame
            Vector3 localOffset = Quaternion.Inverse(_previousRotation) * (rb.position - _previousPosition);

            // Compute new world position after applying delta rotation and position
            Vector3 newWorldPos = transform.position + (transform.rotation * localOffset);

            // Teleport the Rigidbody to maintain same relative position
            rb.MovePosition(newWorldPos);

            // Optional: rotate player along with platform
            Quaternion newRotation = deltaRotation * rb.rotation;
            rb.MoveRotation(newRotation);

            // Optional: Align visual up/forward with platform (depends on your design)
            entity.GetComponent<PlayerArenaManager>().SetForward(transform);

            PrintEntityPhysicsInfo(entity);
        }

        // Save current frame values for next update
        _previousPosition = transform.position;
        _previousRotation = transform.rotation;


    }
    
    void PrintEntityPhysicsInfo(GameObject entity)
{
    Rigidbody rb = entity.GetComponent<Rigidbody>();
    if (rb != null)
    {
        float speed = rb.linearVelocity.magnitude;
        Vector3 angularVelocity = rb.angularVelocity;
        Debug.Log($"Entity: {entity.name} | Speed: {speed:F2} m/s | Angular Velocity: {angularVelocity}");
    }
    else
    {
        Debug.LogWarning($"Entity: {entity.name} does not have a Rigidbody component.");
    }
}
}
