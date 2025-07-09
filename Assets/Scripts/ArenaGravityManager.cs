
using UnityEngine;

public class ArenaGravityManager : MonoBehaviour
{

    private Vector3 _previousPosition;
    private Quaternion _previousRotation;

    [SerializeField]
    private ArenaManager _arenaManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _previousPosition = transform.position;
        _previousRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (_previousPosition != transform.position) // If the platform move
        {
            // Add displacement to all entities
            Vector3 arenaDisplacement = transform.position - _previousPosition;
            foreach (GameObject entity in _arenaManager.getEntities()) {
                entity.transform.position += arenaDisplacement;
            }
            // Add rigidboy displacement speed
            // _arenaManager.entities
        }

        if (_previousRotation != transform.rotation) // If the platform rotated
        {
            //  Rotation that occurred since last frame
            Quaternion deltaRot = transform.rotation * Quaternion.Inverse(_previousRotation);

            foreach (GameObject entity in _arenaManager.getEntities())
            {
                // vector from arena centre to entity
                Vector3 localOffset = entity.transform.position - transform.position;
                // where that vector points after the arena’s rotation
                Vector3 rotatedOffset = deltaRot * localOffset;
                // add the difference to the entity’s world position
                entity.transform.position = transform.position + rotatedOffset;
                /* ---------- orientational effect (make entity keep its local up) -- */
                entity.GetComponent<PlayerArenaManager>().SetForward(transform);
                //entity.transform.rotation = deltaRot * entity.transform.rotation;

                //Set direction for gravity
                //entity.GetComponent<PlayerGravityManager>.SetNormalizedGravityDirection();


            }
            // Add angular movement
            // Add rigidboy angular speed
            // _arenaManager.entities
        }
        
        _previousPosition = transform.position;
        _previousRotation = transform.rotation;
    }
}
