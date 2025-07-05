using UnityEngine;

public class PlayerGravityManager : MonoBehaviour
{

    private Rigidbody _rigidbody;
    [SerializeField]
    private float _gravityStrengh = 10.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Fall();
    }
    
    private void Fall()
    {
        _rigidbody.AddForce(_rigidbody.transform.up * -1 * _gravityStrengh, ForceMode.Acceleration); // Gets the down vector * _fallStrengh
    }
}
