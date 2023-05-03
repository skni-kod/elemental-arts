using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    [SerializeField] private GameObject room; //Room prefab
    [SerializeField] private GameObject startRoom; //Start room prefab
    [SerializeField] private int numberOfRooms = 5; //Without start room
    public List<GameObject> spawnedRooms = new List<GameObject>();
    public List<Vector3> occupiedPositions = new List<Vector3>();
    private List<Vector3> directions = new List<Vector3>();
    private Vector3 forward = new Vector3(50, 0, 0);
    private Vector3 backward = new Vector3(-50, 0, 0);
    private Vector3 left = new Vector3(0, 0, 50);
    private Vector3 right = new Vector3(0, 0, -50);
    private Vector3 previousDirection;
    private Vector3 spawnPosition = Vector3.zero;
    private Vector3 nextSpawnPosition;
    void Start()
    {
        directions.Add(forward);
        directions.Add(backward);
        directions.Add(left);
        directions.Add(right);

        GameObject startRoomInstance = Instantiate(startRoom, spawnPosition, Quaternion.identity);
        occupiedPositions.Add(spawnPosition);
        for (int i = 0; i < numberOfRooms; i++)
        {
            nextSpawnPosition = spawnPosition + directions[Random.Range(0, directions.Count)];
            while(occupiedPositions.Contains(nextSpawnPosition))
            {
                nextSpawnPosition = spawnPosition + directions[Random.Range(0, directions.Count)];
                Debug.Log("reroll");
            }
            SpawnRoom(nextSpawnPosition);
        }
        
    }
    private void SpawnRoom(Vector3 direction)
    {
        GameObject roomInstance = Instantiate(room, direction, Quaternion.identity);
        spawnedRooms.Add(roomInstance);
        occupiedPositions.Add(roomInstance.transform.position);
        previousDirection = direction;
        spawnPosition = roomInstance.transform.position;
    }
}
