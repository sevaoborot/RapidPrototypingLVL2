using UnityEngine;

public class PL_MainScript : MonoBehaviour
{
    [SerializeField] private float velocity;

    private PlayerControls _playerInput;
    private Rigidbody2D _rb;

    void Awake()
    {
        _playerInput = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Player.Disable();
    }

    void FixedUpdate()
    {
        MovePlayerWithRb();
    }

    void MovePlayer()
    {
        Vector3 movementDirection = _playerInput.Player.Movement.ReadValue<Vector2>();
        transform.position += movementDirection * velocity;
    }

    void MovePlayerWithRb()
    {
        Vector2 movementDirection = _playerInput.Player.Movement.ReadValue<Vector2>();
        _rb.MovePosition(_rb.position + movementDirection * velocity);
    }
}
