using System;
using System.Collections.Generic;
using System.Linq;
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
        GameObject newCondenter = GetHighestPriorityCloseArena(closeArenas); // Can be null
        if (!newCondenter)
        {
            RemoveArena();
        }
        else if (newCondenter != parentArena)
        {
            SetArena(newCondenter);
        }
    }

    public void SetForward(Transform arena){
        Debug.Log($"new player rotation = {arena.rotation.eulerAngles} and new gravity direction = {-arena.transform.up}");
        transform.rotation = arena.rotation; // Change the rotation of the player to be alligned with the arena
        _playerGravityManager.SetGravityFromDirection(-arena.transform.up); // Change the gravity of the player
    }

    public void SetArena(GameObject arena)
    {

        if (arena && arena != parentArena)
        {
            try
            {
                ArenaManager arenaManager = arena.GetComponent<ArenaManager>();
                if (!arenaManager)
                {
                    throw new Exception("Component arenaManager has not been found");
                }

                if (parentArena)
                {
                    parentArena.GetComponent<ArenaManager>().RemoveEntity(gameObject);
                }
                arena.GetComponent<ArenaManager>().AddEntity(gameObject); // Add the Entity to the Arena's list of entities for better tracking
                SetForward(arena.transform);
                parentArena = arena; // All good 

            }
            catch (Exception e)
            {
                Debug.LogError($" Exception: '{e.Message}' when setting entity arena of '{this?.name}' for the arena '{arena?.name}' ");
            }
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

                parentArena.GetComponent<ArenaManager>().RemoveEntity(gameObject);
                parentArena = null;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($" Exception: {e.Message} when removing entity arena of '{this?.name}' for the parentArena '{parentArena?.name}' ");
        }
    }

    public void AddToCloseArenas(GameObject gameObject)
    {
        closeArenas.Add(gameObject); // Add the arena to the list of arenas that the entity can be in
        SetArena(GetHighestPriorityCloseArena(closeArenas)); // Rechose what is the next parent arena
    }

    public void RemoveFromCloseArenas(GameObject gameObject)
    {
        closeArenas.Remove(gameObject);
        SetArena(GetHighestPriorityCloseArena(closeArenas)); // Rechose what is the next parent arena
    }

    public GameObject GetHighestPriorityCloseArena(List<GameObject> arenas)
    {

        List<GameObject> highestPriorityArenas = GetHighestPriorityArenas(arenas);
        GameObject highestPriorityArenaCloestArena = GetNearestArena(highestPriorityArenas);
        return highestPriorityArenaCloestArena;
    }

    public List<GameObject> GetHighestPriorityArenas(List<GameObject> arenas)
    {
        if (arenas == null || arenas.Count == 0)
        {
            return null; // or throw new ArgumentNullException(nameof(arenas))
        }
        else
        {
            SortedDictionary<int, List<GameObject>> arenaPriorities = new SortedDictionary<int, List<GameObject>>(); // Create a Dictionnary contain as key the priorty of arena, and as valuea list of the arens with this priorty
            foreach (GameObject arena in arenas)
            {
                ArenaManager arenaManager = arena.GetComponent<ArenaManager>(); // We don't check if items don't have an arenaManager to create an error if it happens

                // get-or-create the list for this priority (store the get result in 'list' variable)
                if (!arenaPriorities.TryGetValue(arenaManager.priority, out var list))
                {
                    list = new List<GameObject>();
                    arenaPriorities[arenaManager.priority] = list;
                }
                list.Add(arena);
            }

            if (arenaPriorities.Count > 0)
            {
                int highestKey = arenaPriorities.Keys.Last();
                return arenaPriorities[highestKey];
            }
            else
            {
                return null;
            }
        }
    }
    
    public GameObject GetNearestArena(List<GameObject> arenas)
    {

        if (arenas == null || arenas.Count == 0)
        {
            return null; // or throw new ArgumentNullException(nameof(arenas))
        }
        else
        {
            GameObject nearestArena = null;
            float nearestArenaDistance = int.MaxValue;
            foreach (GameObject arena in arenas)
            {
                ArenaManager arenaManager = arena.GetComponent<ArenaManager>();
                float arenaDistance = Vector3.Distance(transform.position, arenaManager.ProjectOnPlane(transform.position));

                if (arenaManager && arenaDistance < nearestArenaDistance)
                {
                    nearestArena = arena;
                    nearestArenaDistance = arenaDistance;
                }
            }
            return nearestArena;
        }
    }
}
