using UnityEngine;
using System.Collections.Generic;
using System;

public class ArenaManager : MonoBehaviour
{

    private List<GameObject> _players = new List<GameObject>();
    private List<GameObject> _ennemies = new List<GameObject>();
    private List<GameObject> _entities = new List<GameObject>();

    public List<GameObject> getEntities() { return _entities; }

    [SerializeField]
    public int priority = 50;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void AddEntity(GameObject entity)
    {
        _entities.Add(entity);
    }

    public bool RemoveEntity(GameObject entity)
    {
        try
        {
            if (!_entities.Remove(entity))
            {
                throw new Exception("Entity removal failed");
            }
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($" Exception: {e.Message} when entity removing '{entity?.name}' from the arena '{this?.name}' ");
            return false;
        }
    }

    public Vector3 ProjectOnPlane(Vector3 point_to_project)
    {
        return Vector3.ProjectOnPlane(point_to_project, transform.up) + Vector3.Dot(transform.position, transform.up) * transform.up;
    }
}
