using UnityEngine;

/// <summary>
/// Handles spawning a new tile and destroying this one
/// when the player reaches the end.
/// </summary>
public class TileEndBehaviour : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Quanto tempo esperar antes de destruir o bloco antigo.")]
    private float destroyTime = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<PlayerBehaviour>(out _))
        {
            return;
        }

        GameManager gameManager = Object.FindAnyObjectByType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager não encontrado na cena.");
            return;
        }

        gameManager.SpawnNextTile();

        // O Tile End é filho do Basic Tile.
        // Por isso destruímos o objeto-pai após um atraso.
        Destroy(transform.parent.gameObject, destroyTime);
    }
}