using UnityEngine;
using UnityEngine.InputSystem;

public class DebugMode : MonoBehaviour
{

    public InputActionAsset m_inputActions;

    private InputAction debug1Action;
    private InputAction debug2Action;
    private InputAction debug3Action;
    private InputAction debug4Action;

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
        debug1Action = debugMap.FindAction("Debug1");
        // Register the callback
        debug1Action.performed += Debug1;

        debug2Action = debugMap.FindAction("Debug2");
        debug2Action.performed += Debug2;

        debug3Action = debugMap.FindAction("Debug3");
        debug3Action.performed += Debug3;
        
        debug4Action = debugMap.FindAction("Debug4");
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
