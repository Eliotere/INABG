using UnityEngine;
using System.Collections.Generic;
using System;

public class ArenaManager : MonoBehaviour
{

    private List<GameObject> _players = new List<GameObject>();
    private List<GameObject> _ennemies = new List<GameObject>();
    private List<GameObject> _entities = new List<GameObject>();

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
        var FixeScale = 1;
        entity.transform.SetParent(transform.Find("Entities"), worldPositionStays: true);
        entity.transform.localScale = new Vector3 (FixeScale/transform.lossyScale.x,FixeScale/transform.lossyScale.y,FixeScale/transform.lossyScale.z); // Keep the Scale of the object
    }

    public bool RemoveEntity(GameObject entity)
    {
        try
        {
            if (!_entities.Remove(entity))
            {
                throw new Exception("Entity removal failed");
            }
            entity.transform.SetParent(null, worldPositionStays: true);
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
