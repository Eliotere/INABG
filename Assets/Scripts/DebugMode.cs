using UnityEngine;
using UnityEngine.InputSystem;

public class DebugMode : MonoBehaviour
{

    public InputActionAsset m_inputActions;

    private InputAction debug1Action;
    private InputAction debug2Action;
    private InputAction debug3Action;
    private InputAction debug4Action;
    
    public GameObject player;
    public GameObject prefabToSpawn;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        // Enable the action map
        var debugMap = m_inputActions.FindActionMap("Debug");
        debugMap.Enable();

        // Register the callback
        debug1Action.performed += Debug1;
        debug2Action.performed += Debug2;
        debug3Action.performed += Debug3;
        debug4Action.performed += Debug4;
    }

    private void OnDisable()

    {
        // Unregister the callback and disable the action
        if (debug1Action != null)
        {
            debug1Action.performed -= Debug1;
            debug1Action.Disable();
        }

        if (debug2Action != null)
        {
            debug2Action.performed -= Debug1;
            debug2Action.Disable();
        }

        if (debug3Action != null)
        {
            debug3Action.performed -= Debug1;
            debug3Action.Disable();
        }

        if (debug4Action != null)
        {
            debug4Action.performed -= Debug1;
            debug4Action.Disable();
        }

        // Disable the action map
        var debugMap = m_inputActions.FindActionMap("Debug");
        debugMap.Disable();
    }

    private void Awake()
    {
        var debugMap = m_inputActions.FindActionMap("Debug");

        // Find the action named "Debug1"
        debug1Action = debugMap.FindAction("Debug1");
        debug2Action = debugMap.FindAction("Debug2");
        debug3Action = debugMap.FindAction("Debug3");   
        debug4Action = debugMap.FindAction("Debug4");
    }

    private void Debug1(InputAction.CallbackContext context)
    {
        Debug.Log("Debug1 key pressed!");

        //player.GetComponent<Rigidbody>().AddForce(player.transform.forward * 1000, ForceMode.Acceleration);
        player.GetComponent<Rigidbody>().AddForce(player.transform.forward * 1000, ForceMode.Acceleration);
    }

    private void Debug2(InputAction.CallbackContext context)
    {
        Debug.Log("Debug2 key pressed!");

        player.GetComponent<Rigidbody>().AddForce(-player.transform.up * 1000, ForceMode.Acceleration);

    }

    private void Debug3(InputAction.CallbackContext context)
    {
        Debug.Log("Debug3 key pressed!");
        player.GetComponent<Rigidbody>().AddForce(player.transform.up * 1000, ForceMode.Acceleration);
    }

private void Debug4(InputAction.CallbackContext context)
{
    Debug.Log("[ArenaGravityManager] Debug4 key pressed!");

    // Spawn the item using current gravity-aligned rotation
    GameObject item = Instantiate(
        prefabToSpawn, 
        transform.position, 
        player.GetComponent<PlayerGravityManager>().GetGravityRotation()
    );

    Rigidbody rb = player.GetComponent<Rigidbody>();
    if (rb == null)
    {
        Debug.LogWarning("[ArenaGravityManager] No Rigidbody found on player.");
        return;
    }

    Debug.Log("[ArenaGravityManager] --- Player Rigidbody Debug Info ---");

    // World-space velocity
    Debug.Log($"[ArenaGravityManager] Velocity (world): {rb.linearVelocity}");
        Debug.Log($"[ArenaGravityManager] Speed (magnitude): {rb.linearVelocity.magnitude}");

    // Local velocity (relative to player's transform)
    Vector3 localVelocity = player.transform.InverseTransformDirection(rb.linearVelocity);
    Debug.Log($"[ArenaGravityManager] Local Velocity: {localVelocity}");

    // Gravity direction
    Vector3 gravityDir = player.GetComponent<PlayerGravityManager>().GetGravityDirection();
    Debug.Log($"[ArenaGravityManager] Current Gravity Direction: {gravityDir}");

    // Up, forward, right (for orientation context)
    Debug.Log($"[ArenaGravityManager] Player Up: {player.transform.up}");
    Debug.Log($"[ArenaGravityManager] Player Forward: {player.transform.forward}");
    Debug.Log($"[ArenaGravityManager] Player Right: {player.transform.right}");

    Debug.Log("[ArenaGravityManager] --- End Rigidbody Debug Info ---");
}
    

}
