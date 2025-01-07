using UnityEngine;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
//using Oculus.Interaction;

public class FlashcardSpawner : MonoBehaviour
{
    public GameObject flashcardPrefab; // Assign your flashcard prefab here
    public float minEdgeDistance = 0.3f;
    public MRUKAnchor.SceneLabels spawnLabels;
    public float normalOffset;
    Vector3 spawnPoint= Vector3.up * 1f;
    GameObject currentFlashcard = null;
    //private int spawnTry = 1000;
    //private bool taken = false;

    void Start()
    {
        //MRUKRoom room = MRUK.Instance.GetCurrentRoom();

        // Get the center position of the room
        //Vector3 roomCenter = room.transform.position;
        //Instantiate(flashcardPrefab, spawnPoint, Quaternion.identity);

        // Set the spawn point to the center of the room, 1 meter above the floor
        //spawnPoint = roomCenter + Vector3.up * 1;

        //Debug.Log("The Spawn point is: " + spawnPoint.x + "," + spawnPoint.y + "," + spawnPoint.z);
        SpawnFlashcard();

        //int currentTry = 0;
        /*while (currentTry < spawnTry)
        {
            bool hasFoundPosition = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.FACING_UP, minEdgeDistance, new LabelFilter(spawnLabels), out Vector3 pos, out Vector3 norm);
            if (hasFoundPosition)
            {
                spawnPoint = pos + norm * normalOffset;
                //spawnPoint.y = 3;
                //Instantiate(flashcardPrefab, spawnPoint, Quaternion.identity);
                SpawnFlashcard();
            }
            else
            {
                currentTry++;
            }
        }*/

    }

    void Update()
    {
        if (!MRUK.Instance && !MRUK.Instance.IsInitialized)
        { return; }
        if (currentFlashcard == null || currentFlashcard.transform.position != spawnPoint)//Vector3.up * 2f)
        {
            SpawnFlashcard();
        }
    }

    // Spawns a flashcard on a random scanned object
    public void SpawnFlashcard()
    {
        // Instantiate the flashcard at the spawn point
        currentFlashcard = Instantiate(flashcardPrefab, spawnPoint, Quaternion.identity);
    }
}
