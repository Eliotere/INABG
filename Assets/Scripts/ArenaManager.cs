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

        entity.transform.SetParent(gameObject.transform, true); // DEBUG
        Vector3 parentLossyScale = transform.lossyScale;
        // Vector3 parentScale = transform.rotation * transform.lossyScale;
        Vector3 parentScale = transform.lossyScale;
        Vector3 targetScale = new Vector3(1, 1, 1);
        Vector3 result = new Vector3(targetScale.x / parentScale.x, targetScale.y / parentScale.y, targetScale.z / parentScale.z);
        Debug.Log($"parentLossyScale = {parentLossyScale} ||| transform.rotation = {transform.rotation.eulerAngles}||| parentScale = {parentScale} ||| targetScale = {targetScale} ||| result = {result}");
        entity.transform.localScale = result;

        entity.GetComponent<PlayerArenaManager>().SetForward(transform);
    }

    public bool RemoveEntity(GameObject entity)
    {
        try
        {
            entity.transform.SetParent(null, true);// DEBUG
            entity.transform.localScale = new Vector3(1,1,1);

            Debug.Log($"Entity Got out of Arena");

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
