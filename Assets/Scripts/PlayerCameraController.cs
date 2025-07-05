using UnityEngine;
using UnityEngine.TextCore;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraHolder;
    [SerializeField]
    private Vector3 localOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraHolder.transform.position = transform.position + transform.rotation * localOffset;
    }
}
