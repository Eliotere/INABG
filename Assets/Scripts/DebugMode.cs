using UnityEngine;
using UnityEngine.InputSystem;

public class DebugMode : MonoBehaviour
{

    public InputActionAsset m_inputActions;

    private InputAction debug1;


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

        // Find the action named "Debug1"
        debug1 = debugMap.FindAction("Debug1");

        // Register the callback
        debug1.performed += Debug1;
    }

    private void OnDisable()
    
    {
        // Unregister the callback and disable the action
        if (debug1 != null)
        {
            debug1.performed -= Debug1;
            debug1.Disable();
        }

        // Disable the action map
        var debugMap = m_inputActions.FindActionMap("Debug");
        debugMap.Disable();
    }

    private void Awake()
    {

    }
    
    private void Debug1(InputAction.CallbackContext context)
    {
        Debug.Log("Debug1 key pressed!");
    }
}
