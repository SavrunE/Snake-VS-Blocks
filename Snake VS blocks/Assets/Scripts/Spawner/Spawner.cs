using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform container;
    [SerializeField] private int repeatCount;
    [SerializeField] private float distanceBetweenFullLine;
    [SerializeField] private float distanceBetweenRandomLine;

    [Header("Blocks")]
    [SerializeField] private Block blockTemplate;
    [SerializeField] private int blockSpawnChance;

    [Header("Walls")]
    [SerializeField] private Wall wallTemplate;
    [SerializeField] private int wallSpawnChance;

    [Header("Bonuses")]
    [SerializeField] private Bonus bonusTemplate;
    [SerializeField] private Vector2Int bonusCountSpawnRange;

    private BlockSpawnPoint[] blocksSpawnPoints;
    private WallSpawnPoint[] wallSpawnPoints;
    private float levelDistance;

    private void Start()
    {
        blocksSpawnPoints = GetComponentsInChildren<BlockSpawnPoint>();
        wallSpawnPoints = GetComponentsInChildren<WallSpawnPoint>();

        for (int i = 0; i < repeatCount; i++)
        {
            MoveSpawner(distanceBetweenFullLine);
            GenerateRandomLine(wallSpawnPoints, wallTemplate.gameObject, wallSpawnChance, wallTemplate.transform.localScale.y * distanceBetweenFullLine, distanceBetweenFullLine / 2f);
            GenerateFullLine(blocksSpawnPoints, blockTemplate.gameObject);
            MoveSpawner(distanceBetweenRandomLine);
            GenerateRandomLine(wallSpawnPoints, wallTemplate.gameObject, wallSpawnChance, distanceBetweenRandomLine * wallTemplate.transform.localScale.y, distanceBetweenRandomLine / 2f);
            GenerateRandomLine(blocksSpawnPoints, blockTemplate.gameObject, blockSpawnChance);
            MoveSpawner(distanceBetweenRandomLine);
            GenerateRandomLine(wallSpawnPoints, wallTemplate.gameObject, wallSpawnChance, distanceBetweenRandomLine * wallTemplate.transform.localScale.y, distanceBetweenRandomLine / 2f);
            GenerateCorrectCountLine(blocksSpawnPoints, bonusTemplate.gameObject, Random.Range(bonusCountSpawnRange.x, bonusCountSpawnRange.y));
        }
        SpawnSideWall(levelDistance);
    }

    private void GenerateFullLine(SpawnPoint[] spawnPoints, GameObject generatedElement)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GenerateElement(spawnPoints[i].transform.position, generatedElement);
        }
    }

    private void GenerateCorrectCountLine(SpawnPoint[] spawnPoints, GameObject generatedElement, int spawnCount)
    {
        List<SpawnPoint> points = new List<SpawnPoint>();
        points.AddRange(spawnPoints);

        SpawnPoint spawnedPoint;
        for (int i = 0; i < spawnCount; i++)
        {
            spawnedPoint = points[Random.Range(0, points.Count)];
            GenerateElement(spawnedPoint.transform.position, generatedElement);
            points.Remove(spawnedPoint);
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
    private void GenerateRandomLine(SpawnPoint[] spawnPoints, GameObject generatedElement, int spawnChance, float scaleY, float offsetY)
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

    private void MoveSpawner(float distanceY)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + distanceY, transform.position.z);
        levelDistance += distanceY;
    }
    private void GenerateSideWalls(Vector3 spawnPoint, Wall walls, float SizeY)
    {
        GameObject wall = Instantiate(walls.gameObject, spawnPoint, Quaternion.identity, container);
        wall.transform.localScale = new Vector3(wall.transform.localScale.x, SizeY, wall.transform.localScale.z);
    }

    public void SpawnSideWall(float levelDistance)
    {
        float defaultWidth = Camera.main.orthographicSize;
        GenerateSideWalls(new Vector3(-defaultWidth / 2f, levelDistance / 2f + distanceBetweenFullLine, 0f), wallTemplate, levelDistance * wallTemplate.transform.localScale.y);
        GenerateSideWalls(new Vector3(defaultWidth / 2f, levelDistance / 2f + distanceBetweenFullLine, 0f), wallTemplate, levelDistance * wallTemplate.transform.localScale.y);
    }
}
