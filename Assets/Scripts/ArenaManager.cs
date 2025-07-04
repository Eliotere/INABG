using UnityEngine;
using System.Collections.Generic;
using System;

public class ArenaManager : MonoBehaviour
{

    private List<GameObject> _players = new List<GameObject>();
    private List<GameObject> _ennemies = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPlayer(GameObject player)
    {
        _players.Add(player);
        player.transform.SetParent(transform.Find("Players"), true);
    }

    public bool RemovePlayer(GameObject player)
    {
        try
        {
            if (!_players.Remove(player))
            {
                throw new Exception("Player removal failed");
            }
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($" Exception: {e.Message} when player removing '{player?.name}' from the arena '{this?.name}' ");
            return false;
        }
    }

    public void AddEnnemy(GameObject ennemy)
    {
        _players.Add(ennemy);
        ennemy.transform.SetParent(transform.Find("Ennemies"), true);
    }

    public bool RemoveEnnemy(GameObject ennemy)
    {
        try
        {
            if (!_players.Remove(ennemy))
            {
                throw new Exception("Player removal failed");
            }
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($" Exception: {e.Message} when ennemy removing '{ennemy?.name}' from the arena '{this?.name}' ");
            return false;
        }
    }

    public Vector3 ProjectOnPlane(Vector3 point_to_project)
    {
        return Vector3.ProjectOnPlane(point_to_project, transform.up) + Vector3.Dot(transform.position, transform.up) * transform.up;
    }
}
