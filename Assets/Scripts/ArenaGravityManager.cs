
using UnityEngine;
using System.Collections.Generic;

public class ArenaGravityManager : MonoBehaviour
{

    private Vector3 _previousPosition;
    private Quaternion _previousRotation;

    // DEBUG
    private Dictionary<GameObject, Transform> entitiesHistorics = new Dictionary<GameObject, Transform>();

    [SerializeField]
    private ArenaManager _arenaManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _previousPosition = transform.position;
        _previousRotation = transform.rotation;
    }

    // void FixedUpdate()
    // {
    //     Quaternion deltaRotation = Quaternion.Inverse(_previousRotation) * transform.rotation;
    //     Vector3 deltaPosition = transform.position - _previousPosition;

    //     float epsilion = 1e-5f;
    //     if (Quaternion.Angle(Quaternion.identity, deltaRotation) > epsilion || deltaPosition.magnitude > epsilion)
    //     {
    //         foreach (GameObject entity in _arenaManager.getEntities())
    //         {
    //             Rigidbody entityRB = entity.GetComponent<Rigidbody>();
    //             if (!entityRB) continue;

    //             // Sets new position/rotation
    //             Vector3 oldWorldPos = entityRB.position;
    //             Quaternion oldWorldRotation = entityRB.rotation;

    //             UpdateEntityVelocity(entityRB);

    //             Vector3 newWorldPos = CaculateEntityPostionAfterMovement(oldWorldPos);
    //             // Teleport the Rigidbody to maintain same relative position
    //             entityRB.MovePosition(newWorldPos);

    //             Quaternion newRotation = CaculateEntityRotationAfterMovement();
    //             entityRB.MoveRotation(newRotation);


    //             // Set forward and gravity direction of Entity
    //             entity.GetComponent<PlayerArenaManager>().SetForward(transform); // WARNING in its current state, the rotation is set twice (before and here)

    //             // Update new forces of entity
    //             Vector3 deltaWorldPos = entityRB.transform.position - oldWorldPos;
    //             // Compute velocity based on delta position cause we don't use physics to move
    //             Vector3 linearSpeedByArenaEntity = deltaWorldPos / Time.deltaTime;
    //             // Rotate the velocity by the platform’s rotation change
    //             // Align up/forward with platform 


    //         }

    //         // Save current frame values for next update
    //         _previousPosition = transform.position;
    //         _previousRotation = transform.rotation;


    //     }
    // }

    //     private Vector3 CaculateEntityPostionAfterMovement(Vector3 entityPos)
    //     {
    //         // Local offset from the platform’s *previous* frame
    //         // Vector3 oldWorldPos = entityRB.position;
    //         Vector3 localOffset = Quaternion.Inverse(_previousRotation) * (entityPos - _previousPosition);

    //         // Calculate new World Position
    //         return transform.position + (transform.rotation * localOffset);
    //     }

    //     private Quaternion CaculateEntityRotationAfterMovement()
    //     {
    //         return transform.rotation; // Return the rotation of the platform
    //     }

    //     private void UpdateEntityVelocity(Rigidbody entityRB)
    //     {
    //         //Convert forces in the new Arena
    //         // 1. Convert velocity from world space to local space
    //         Vector3 localVelocity = Quaternion.Inverse(_previousRotation) * entityRB.linearVelocity;
    //         Debug.Log(localVelocity);
    //         // 2. Transform it into new world space (new orientation)
    //         Vector3 rotatedVelocity = transform.rotation * localVelocity;
    //         // 3. Apply it
    //         entityRB.linearVelocity = rotatedVelocity;

    //     }

    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Inverse(_previousRotation) * transform.rotation;
        Vector3 deltaPosition = transform.position - _previousPosition;

        float epsilion = 1e-5f;
        if (Quaternion.Angle(Quaternion.identity, deltaRotation) > epsilion || deltaPosition.magnitude > epsilion)
        {
            foreach (GameObject entity in _arenaManager.getEntities())
            {
                Rigidbody entityRB = entity.GetComponent<Rigidbody>();
                if (!entityRB) continue;

                UpdateEntityVelocity(entityRB);

                // Set forward and gravity direction of Entity
                entity.GetComponent<PlayerArenaManager>().SetForward(transform); // WARNING in its current state, the rotation is set twice (before and here)


            }

            // Save current frame values for next update
            _previousPosition = transform.position;
            _previousRotation = transform.rotation;


        }
    }

    private void UpdateEntityVelocity(Rigidbody entityRB)
    {
        //Convert forces in the new Arena
        // 1. Convert velocity from world space to local space
        Vector3 localVelocity = Quaternion.Inverse(_previousRotation) * entityRB.linearVelocity;
        Debug.Log(localVelocity);
        // 2. Transform it into new world space (new orientation)
        Vector3 rotatedVelocity = transform.rotation * localVelocity;
        // 3. Apply it
        entityRB.linearVelocity = rotatedVelocity;

    }
}