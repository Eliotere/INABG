using UnityEngine;
using System.Collections.Generic;
using System;

public class ArenaManager : MonoBehaviour
{

    public List<GameObject> m_players = new List<GameObject>();
    public List<GameObject> m_ennemies = new List<GameObject>();

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
        player.transform.position = ProjectOnPlane(player.transform.position);
        m_players.Add(player);
        player.transform.SetParent(transform.Find("Players"), true);
    }

    public bool RemovePlayer(GameObject player)
    {
        try
        {
            if (!m_players.Remove(player))
            {
                throw new Exception("Player removal failed");
            }
            player.transform.SetParent(null, true);
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
        ennemy.transform.position = ProjectOnPlane(ennemy.transform.position);
        m_players.Add(ennemy);
        ennemy.transform.SetParent(transform.Find("Ennemies"), true);

    }

    public bool RemoveEnnemy(GameObject ennemy)
    {
        try
        {
            if (!m_players.Remove(ennemy))
            {
                throw new Exception("Player removal failed");
            }
            ennemy.transform.SetParent(null, true);
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
