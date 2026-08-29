using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Restarts the current scene when the player hits
/// this obstacle.
/// </summary>
public class ObstacleBehaviour : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Quanto tempo esperar antes de reiniciar a cena.")]
    private float waitTime = 2f;

    private bool isRestarting;

    private void OnCollisionEnter(Collision collision)
    {
        if (isRestarting)
        {
            return;
        }

        if (!collision.gameObject.TryGetComponent<PlayerBehaviour>(out _))
        {
            return;
        }

        isRestarting = true;
        Destroy(collision.gameObject);
        Invoke(nameof(ResetGame), waitTime);
    }

    private void ResetGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}