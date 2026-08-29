using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class ObstacleBehaviour : MonoBehaviour
{
    [Tooltip("How long to wait before restarting the game")]
    [SerializeField, Min(0f)] private float waitTime = 2.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent<PlayerBehaviour>(out _))
            return;

        Destroy(collision.gameObject);
        Invoke(nameof(ResetGame), waitTime);
    }

    /// <summary>Reinicia a cena atual.</summary>
    private void ResetGame()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}