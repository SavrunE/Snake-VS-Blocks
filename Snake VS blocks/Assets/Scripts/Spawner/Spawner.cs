using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private int repeatCount;
    [SerializeField] private int distanceBetweenFullLine;
    [SerializeField] private int distanceBetweenRandomLine;

    [SerializeField] private Block blockTemplate;
    [SerializeField] private int blockSpawnChance;

    private SpawnPoint[] blocksSpawnPoints;
    private void Start()
    {
        blocksSpawnPoints = GetComponentsInChildren<SpawnPoint>();

        for (int i = 0; i < repeatCount; i++)
        {
            GenerateFullLine(blocksSpawnPoints, blockTemplate.gameObject);
            MoveSpawner(distanceBetweenFullLine);
        }
    }

    private void GenerateFullLine(SpawnPoint[] spawnPoints, GameObject generatedElement)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GenerateElement(spawnPoints[i].transform.position, generatedElement);
        }
    }

    private void GenerateRandomLine()
    {

    }

    private GameObject GenerateElement(Vector3 spawnPoint,GameObject generatedElement)
    {
        spawnPoint.y -= generatedElement.transform.localScale.y;
        return Instantiate(generatedElement, spawnPoint, Quaternion.identity, container);
    }
    
    private void MoveSpawner(int distanceY)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + distanceY, transform.position.z);
    }
}
