using UnityEngine;

public class PL_BattleMode : MonoBehaviour
{
    [SerializeField] private float velocity;

    private PlayerControls _playerInput;
    private Rigidbody2D _rb;
    
    private GM_BattleMode _battleMode;

    void Awake()
    {
        _playerInput = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        _battleMode = transform.parent.GetComponent<GM_BattleMode>();
        //if ( _battleMode == null ) выкинуть ексепш
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

    void Update()
    {
        if (_battleMode.IsPlayerTurn == true) _playerInput.Disable();
        else _playerInput.Enable();
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
