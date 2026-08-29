using UnityEngine;

/// <summary>
/// Makes the camera follow a target at a fixed offset
/// and turn to face it.
/// </summary>
public class CameraBehaviour : MonoBehaviour
{
    [Tooltip("The object the camera will follow")]
    public Transform target;

    [Tooltip("Position of the camera relative to the target")]
    public Vector3 offset = new Vector3(0, 1, -10);

    void LateUpdate()
    {
        if (target == null)
            return;

        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}