using Meta.XR.MRUtilityKit;
using UnityEngine;

public class RespawnOnMove : MonoBehaviour
{
    private Vector3 initialPosition;
    private FindSpawnPositions spawner;
    bool old = false;

    void Start()
    {
        // Store initial position
        initialPosition = transform.position;

        // Get the FindSpawnPositions component from the same GameObject
        spawner = GetComponent<FindSpawnPositions>();
    }

    void Update()
    {
        // Check if the object has moved
        if (transform.position != initialPosition && !old)
        {
            // Trigger the spawner to respawn
            if (spawner != null) // Check if spawner is found
            {
                spawner.StartSpawn();
                old = true;
            }
            else
            {
                Debug.LogError("Spawner not found!");
            }
            

            // Destroy this object or disable the script
            //Destroy(gameObject);
            // or
            // this.enabled = false; 
        }
    }
}