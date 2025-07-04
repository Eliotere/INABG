using UnityEngine;
using UnityEngine.InputSystem;

public class DebugMode : MonoBehaviour
{

    public InputActionAsset m_inputActions;

    private InputAction debug1;
    private InputAction debug2;
    private InputAction debug3;
    private InputAction debug4;

    public GameObject first_arena;
    public GameObject second_arena;
    public GameObject player;


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

        debug2 = debugMap.FindAction("Debug2");
        debug2.performed += Debug2;

        debug3 = debugMap.FindAction("Debug3");
        debug3.performed += Debug3;
        
        debug4 = debugMap.FindAction("Debug4");
        debug4.performed += Debug4;
    }

    private void OnDisable()

    {
        // Unregister the callback and disable the action
        if (debug1 != null)
        {
            debug1.performed -= Debug1;
            debug1.Disable();
        }

        if (debug2 != null)
        {
            debug2.performed -= Debug1;
            debug2.Disable();
        }

        if (debug3 != null)
        {
            debug3.performed -= Debug1;
            debug3.Disable();
        }

        if (debug4 != null)
        {
            debug4.performed -= Debug1;
            debug4.Disable();
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

        first_arena.GetComponent<ArenaManager>().AddPlayer(player);
    }

    private void Debug2(InputAction.CallbackContext context)
    {
        Debug.Log("Debug2 key pressed!");

        first_arena.GetComponent<ArenaManager>().RemoveEnnemy(player);
    }

    private void Debug3(InputAction.CallbackContext context)
    {
        Debug.Log("Debug3 key pressed!");

        second_arena.GetComponent<ArenaManager>().AddPlayer(player);

    }

    private void Debug4(InputAction.CallbackContext context)
    {
        Debug.Log("Debug4 key pressed!");
    }
    

}
