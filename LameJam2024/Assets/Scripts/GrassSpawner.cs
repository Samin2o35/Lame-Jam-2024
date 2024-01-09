using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject grassPrefab;
    //[SerializeField] private Collider[] spawnableArea;
    public int gridX;
    public int gridZ;
    public float gridSpacingOffset;
    //public Vector3 gridOrigin;

    private void Start()
    {
        //spawn grass blades
        SpawnGrid();
    }

    private void SpawnGrid()
    {
        for(int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                Vector3 spawnPosition = new Vector3(x * gridSpacingOffset, 0, z * gridSpacingOffset) + transform.position;
                PickAndSpawn(spawnPosition, Quaternion.identity);
            }
        }
    }

    private void PickAndSpawn(Vector3 positionToSpawn, Quaternion rotatationToSpawn)
    {
        GameObject clone = Instantiate(grassPrefab, positionToSpawn, rotatationToSpawn);
    }
}
