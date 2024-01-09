using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] itemsToPickFrom;
    [SerializeField] private Collider[] spawnableArea;
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
                PickAndSpawn(spawnableArea[x], itemsToPickFrom, Quaternion.identity);
            }
        }
    }

    private void PickAndSpawn(Collider spawnableAreaCollider, GameObject[] itemsToPickFrom, Quaternion rotationToSpawn)
    {
        Vector3 spawnPos = GetRandomSpawnPosition(spawnableAreaCollider);
        int randomIndex = Random.Range(0, itemsToPickFrom.Length);
        GameObject clone = Instantiate(itemsToPickFrom[randomIndex], spawnPos, rotationToSpawn);
    }

    private Vector3 GetRandomSpawnPosition(Collider spawnableAreaCollider)
    {
        Vector3 spawnPosition = Vector3.zero;
        
        bool isSpawnPosValid = false;
        int attemptCount = 0;
        int maxAttemts = 10;
        int layerToSpawnOn = LayerMask.NameToLayer("Ground");

        while(!isSpawnPosValid && attemptCount < maxAttemts) 
        {
            spawnPosition = GetRandomPointinCollider(spawnableAreaCollider);
            Collider[] objSpawnSize = Physics.OverlapSphere(spawnPosition, 1f);

            bool isInvalidCollision = false;
            foreach(Collider collider in objSpawnSize) 
            {
                if(collider.gameObject.layer != layerToSpawnOn) 
                {
                    isInvalidCollision = true;
                    break;
                }
            }

            if(!isInvalidCollision) 
            {
                isSpawnPosValid = true;
            }
            attemptCount++;
        }
        return spawnPosition;
    }

    private Vector3 GetRandomPointinCollider(Collider collider, float offset = 1f) 
    {
        Bounds colBounds = collider.bounds;
        Vector3 minBounds = new Vector3(colBounds.min.x + offset, 0, colBounds.min.z + offset);
        Vector3 maxBounds = new Vector3(colBounds.max.x - offset, 0, colBounds.max.z - offset);

        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomZ = Random.Range(minBounds.z, maxBounds.z);

        return new Vector3(randomX, 0, randomZ);
    }
}
