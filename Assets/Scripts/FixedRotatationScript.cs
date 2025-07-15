using UnityEngine;
using UnityEngine.AI;

public class FixedRotatationScript : MonoBehaviour
{

    [SerializeField]
    private Vector3 _rotationVector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb)
        {
            // Calculate incremental rotation this physics frame as a Quaternion
            Quaternion deltaRotation = Quaternion.Euler(_rotationVector * Time.fixedDeltaTime);

            // Apply the rotation relative to current rotation
            Quaternion newRotation = rb.rotation * deltaRotation;

            rb.MoveRotation(newRotation);
        }
        else
        {
            transform.Rotate(_rotationVector * Time.deltaTime, Space.Self);
        }
    }
}
