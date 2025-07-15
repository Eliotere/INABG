using UnityEngine;

public class ArenaCloseCollider : MonoBehaviour
{
    [SerializeField]
    private GameObject parentArena;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Entity")) {
            GameObject entity = other.gameObject;
            entity.GetComponent<PlayerArenaManager>().AddToCloseArenas(gameObject.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Entity")) {
            GameObject entity = other.gameObject;
            entity.GetComponent<PlayerArenaManager>().RemoveFromCloseArenas(gameObject.transform.parent.gameObject);
        }
    }
}
