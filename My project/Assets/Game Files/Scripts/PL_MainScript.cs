using UnityEngine;

public class PL_MainScript : MonoBehaviour
{
    private PlayerControls _playerInput;

    void Awake()
    {
        _playerInput = new PlayerControls();
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
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 movementDirection = _playerInput.Player.Movement.ReadValue<Vector2>();
        transform.position += movementDirection * 1;
    }
}
