using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Responsible for moving the player automatically
/// and receiving input.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    /// <summary>
    /// A reference to the Rigidbody component.
    /// </summary>
    private Rigidbody rb;

    [Tooltip("Input Action: Player/MoveHorizontal")]
    public InputActionReference moveHorizontalAction;

    [Tooltip("How fast the ball moves left/right")]
    public float dodgeSpeed = 5;

    [Tooltip("How fast the ball moves forwards automatically")]
    [Range(0, 10)]
    public float rollSpeed = 5;

    private float horizontalInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        moveHorizontalAction.action.Enable();
    }

    void OnDisable()
    {
        moveHorizontalAction.action.Disable();
    }

    void Update()
    {
        horizontalInput = moveHorizontalAction.action.ReadValue<float>();
    }

    /// <summary>
    /// FixedUpdate is a prime place to put physics
    /// calculations happening over a period of time.
    /// </summary>
    void FixedUpdate()
    {
        float horizontalSpeed = horizontalInput * dodgeSpeed;
        rb.AddForce(horizontalSpeed, 0, rollSpeed);
    }
}