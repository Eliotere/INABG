using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

}

// public class PlayerCameraController : MonoBehaviour
// {

//     [SerializeField] private Transform cameraHolder;   // Should *not* be parented to player
//     [SerializeField] private Rigidbody playerRb;
//     [SerializeField] private Vector3 targetOffset = new Vector3(0f, 2f, -4f);
//     [SerializeField] private float followSpeed = 6f;
    
//     private Vector3 velocity = Vector3.zero;

//     void Awake()
//     {
//         cameraHolder.transform.SetParent(null);  // Unparents the object in world space
//         playerRb = GetComponent<Rigidbody>();
//     }

//     private void LateUpdate()
//     {
//         // Translate offset into world space using TransformPoint
//         Vector3 targetWorldPos = playerRb.position + playerRb.rotation * targetOffset;

//         // Smooth follow in world space
//         cameraHolder.position = Vector3.SmoothDamp(
//             cameraHolder.position,
//             targetWorldPos,
//             ref velocity,
//             0.05f); // Time to reach target — lower is snappier (try 0.03f to 0.08f)

//     }
// }