using UnityEngine;

/// <summary>
/// Manages the main gameplay of the endless runner.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Prefab do bloco que será gerado.")]
    private Transform tilePrefab;

    [SerializeField]
    [Tooltip("Onde o primeiro bloco deve ser colocado.")]
    private Vector3 startPoint = new Vector3(0f, 0f, -5f);

    [SerializeField]
    [Tooltip("Quantos blocos devem ser criados no início.")]
    [Range(1, 15)]
    private int initSpawnNum = 10;

    private Vector3 nextTileLocation;
    private Quaternion nextTileRotation;

    private void Start()
    {
        nextTileLocation = startPoint;
        nextTileRotation = Quaternion.identity;

        for (int i = 0; i < initSpawnNum; i++)
        {
            SpawnNextTile();
        }
    }

    /// <summary>
    /// Generates a new tile and updates where the next one
    /// should be generated.
    /// </summary>
    public void SpawnNextTile()
    {
        if (tilePrefab == null)
        {
            Debug.LogError("Tile Prefab não foi definido no GameManager.");
            return;
        }

        Transform newTile = Instantiate(
            tilePrefab,
            nextTileLocation,
            nextTileRotation
        );

        Transform nextSpawnPoint = newTile.Find("Next Spawn Point");
        if (nextSpawnPoint == null)
        {
            Debug.LogError(
                "O prefab Basic Tile precisa ter um filho " +
                "chamado Next Spawn Point."
            );
            return;
        }

        nextTileLocation = nextSpawnPoint.position;
        nextTileRotation = nextSpawnPoint.rotation;
    }
}