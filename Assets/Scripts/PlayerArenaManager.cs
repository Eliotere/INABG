using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Mono.Cecil.Cil;
using UnityEngine;

public class PlayerArenaManager : MonoBehaviour
{
    [SerializeField]
    private GameObject parentArena = null;
    [SerializeField]
    private List<GameObject> closeArenas;

    [SerializeField]
    private PlayerGravityManager _playerGravityManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (parentArena)
        {
            transform.rotation = parentArena.transform.rotation; // Actualize player rotation
            _playerGravityManager.SetNormalizedGravityDirection(parentArena.transform.up * -1); // Actualize gravity direction
        }
    }

    public void SetArena(GameObject arena)
    {
        try
        {
            ArenaManager arenaManager = arena.GetComponent<ArenaManager>();
            if (!arenaManager)
            {
                throw new Exception("Component 'arenaManager' has not been found");
            }

            if (parentArena)
            {
                parentArena.GetComponent<ArenaManager>().RemovePlayer(gameObject);
            }
            arena.GetComponent<ArenaManager>().AddPlayer(gameObject);
            parentArena = arena;

        }
        catch (Exception e)
        {
            Debug.LogError($" Exception: {e.Message} when setting player arena of '{this?.name}' for the arena '{arena?.name}' ");
        }
    }

    public void RemoveArena()
    {
        try
        {
            if (!parentArena)
            {
                return;
            }
            else
            {
                ArenaManager arenaManager = parentArena.GetComponent<ArenaManager>();
                if (!arenaManager)
                {
                    throw new Exception("Component 'arenaManager' has not been found");
                }
                if (parentArena)
                {
                    parentArena.GetComponent<ArenaManager>().RemovePlayer(gameObject);
                }
                gameObject.transform.SetParent(null);
                parentArena = null;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($" Exception: {e.Message} when removing player arena of '{this?.name}' for the parentArena '{parentArena?.name}' ");
        }
    }

    public void AddToCloseArenas(GameObject gameObject)
    {
        closeArenas.Add(gameObject);
        parentArena = GetHighestPriorityCloseArenas();
    }

    public void RemoveFromCloseArenas(GameObject gameObject)
    {
        closeArenas.Remove(gameObject);
        parentArena = GetHighestPriorityCloseArenas();
    }

    public GameObject GetHighestPriorityCloseArenas()
    {
        GameObject highestArena = null;
        int highestPriority = int.MinValue;

        foreach (GameObject arena in closeArenas) {
            ArenaManager arenaManager = arena.GetComponent<ArenaManager>();
            if (arenaManager && arenaManager.priority > highestPriority)
            {
                highestArena = arena;
                highestPriority = arenaManager.priority;
            }
        }
    return highestArena;
    }
}
