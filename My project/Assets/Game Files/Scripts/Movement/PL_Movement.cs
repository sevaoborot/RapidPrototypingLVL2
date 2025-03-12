using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PL_Movement : MonoBehaviour
{
    [SerializeField] private float velocity;

    private PlayerControls _playerInput;
    private Rigidbody2D _rb;

    public void InitializeMovement(PlayerControls playerInput)
    {
        _playerInput = playerInput;
        _rb = GetComponent<Rigidbody2D>();
        Debug.Log("Player movement is initialized successfully!");
    }

    private void OnEnable()
    {
        _playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Player.Disable();
    }

    private void FixedUpdate()
    {
        MovePlayerWithRb();
    }

    private void MovePlayerWithRb()
    {
        Vector2 movementDirection = _playerInput.Player.Movement.ReadValue<Vector2>();
        _rb.MovePosition(_rb.position + movementDirection * velocity);
    }
}
