using UnityEngine;

public class PlayerGravityManager : MonoBehaviour
{

    private Rigidbody _rigidbody;
    [SerializeField]
    private float _gravityStrengh = 10.0f;
    [SerializeField]
    private Vector3 _normalizedGravityDirection;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _normalizedGravityDirection = transform.up * -1; // Sets default gravity direction as local down
        
        ///////////////////////////////////////////////////////////////////
        // WILL ADD DEPLACEMENTS BASED ON ARENA ROTATION AND DSIPLACEMENT
        ///////////////////////////////////////////////////////////////////
    }

    // Update is called once per frame
    void Update()
    {
        Fall();
    }

    private void Fall()
    {
        _rigidbody.AddForce(_normalizedGravityDirection * _gravityStrengh, ForceMode.Acceleration); // Gets the down vector * _fallStrengh
    }

    public void SetNormalizedGravityDirection(Vector3 newNormalizedGravityDirection)
    {
        _normalizedGravityDirection = newNormalizedGravityDirection;
    }
    
    public Vector3 GetNormalizedGravityDirection()
    {
        return _normalizedGravityDirection;
    }
}
