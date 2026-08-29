using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the main gameplay of the game.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField, Tooltip("A reference to the tile we want to spawn")]
    private Transform tile;

    [SerializeField, Tooltip("A reference to the obstacle we want to spawn")]
    private Transform obstacle;

    [Header("Spawn")]
    [SerializeField, Tooltip("Where the first tile should be placed at")]
    private Vector3 startPoint = new Vector3(0, 0, -5);

    [SerializeField, Tooltip("How many tiles should we create in advance")]
    [Range(1, 15)]
    private int initSpawnNum = 10;

    [SerializeField, Tooltip("How many tiles to spawn with no obstacles")]
    private int initNoObstacles = 4;

    private Vector3 nextTileLocation;
    private Quaternion nextTileRotation;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        nextTileLocation = startPoint;
        nextTileRotation = Quaternion.identity;

        for (int i = 0; i < initSpawnNum; i++)
        {
            bool spawnObstacles = i >= initNoObstacles;
            SpawnNextTile(spawnObstacles);
        }
    }

    /// <summary>
    /// Spawns a tile at the next location and updates the next position.
    /// </summary>
    /// <param name="spawnObstacles">If we should spawn an obstacle.</param>
    public void SpawnNextTile(bool spawnObstacles = true)
    {
        Transform newTile = Instantiate(tile, nextTileLocation, nextTileRotation);

        Transform nextSpawnPoint = newTile.Find("Next Spawn Point");
        if (nextSpawnPoint == null)
        {
            Debug.LogError("Next Spawn Point não encontrado no prefab Basic Tile.");
            return;
        }

        nextTileLocation = nextSpawnPoint.position;
        nextTileRotation = nextSpawnPoint.rotation;

        if (spawnObstacles)
            SpawnObstacle(newTile);
    }

    private void SpawnObstacle(Transform newTile)
    {
        var obstacleSpawnPoints = new List<Transform>();

        foreach (Transform child in newTile)
        {
            if (child.CompareTag("ObstacleSpawn"))
                obstacleSpawnPoints.Add(child);
        }

        if (obstacleSpawnPoints.Count == 0)
            return;

        int index = Random.Range(0, obstacleSpawnPoints.Count);
        Transform spawnPoint = obstacleSpawnPoints[index];

        Transform newObstacle = Instantiate(
            obstacle,
            spawnPoint.position,
            Quaternion.identity
        );

        newObstacle.SetParent(newTile, worldPositionStays: true);
    }
}