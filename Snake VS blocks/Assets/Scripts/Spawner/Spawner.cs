using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform container;
    [SerializeField] private int repeatCount;
    [SerializeField] private int distanceBetweenFullLine;
    [SerializeField] private int distanceBetweenRandomLine;

    [Header("Blocks")]
    [SerializeField] private Block blockTemplate;
    [SerializeField] private int blockSpawnChance;

    [Header("Walls")]
    [SerializeField] private Wall wallTemplate;
    [SerializeField] private int wallSpawnChance;

    private BlockSpawnPoint[] blocksSpawnPoints;
    private WallSpawnPoint[] wallSpawnPoints;
    private void Start()
    {
        blocksSpawnPoints = GetComponentsInChildren<BlockSpawnPoint>();
        wallSpawnPoints = GetComponentsInChildren<WallSpawnPoint>();

        for (int i = 0; i < repeatCount; i++)
        {
            MoveSpawner(distanceBetweenFullLine);
            GenerateRandomLine(wallSpawnPoints, wallTemplate.gameObject, wallSpawnChance, distanceBetweenFullLine, distanceBetweenFullLine / 2f);
            GenerateFullLine(blocksSpawnPoints, blockTemplate.gameObject);
            MoveSpawner(distanceBetweenFullLine);
            GenerateRandomLine(wallSpawnPoints, wallTemplate.gameObject, wallSpawnChance, distanceBetweenRandomLine, distanceBetweenRandomLine / 2f);
            GenerateRandomLine(blocksSpawnPoints, blockTemplate.gameObject, blockSpawnChance);
        }
    }

    private void GenerateFullLine(SpawnPoint[] spawnPoints, GameObject generatedElement)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GenerateElement(spawnPoints[i].transform.position, generatedElement);
        }
    }

    private void GenerateRandomLine(SpawnPoint[] spawnPoints, GameObject generatedElement, int spawnChance)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (Random.Range(0, 100) < spawnChance)
            {
                GameObject element = GenerateElement(spawnPoints[i].transform.position, generatedElement);
            }
        }
    }
    private void GenerateRandomLine(SpawnPoint[] spawnPoints, GameObject generatedElement, int spawnChance, int scaleY = 1, float offsetY = 0)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (Random.Range(0, 100) < spawnChance)
            {
                GameObject element = GenerateElement(spawnPoints[i].transform.position, generatedElement, offsetY);
                element.transform.localScale = new Vector3(element.transform.localScale.x, scaleY, element.transform.localScale.z);
            }
        }
    }

    private GameObject GenerateElement(Vector3 spawnPoint, GameObject generatedElement, float offsetY = 0)
    {
        spawnPoint.y -= offsetY;
        return Instantiate(generatedElement, spawnPoint, Quaternion.identity, container);
    }

    private void MoveSpawner(int distanceY)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + distanceY, transform.position.z);
    }
}
