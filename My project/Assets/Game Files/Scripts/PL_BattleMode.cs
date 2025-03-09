using UnityEngine;

public class PL_BattleMode : MonoBehaviour
{
    [SerializeField] private float velocity;

    private PlayerControls _playerInput;
    private Rigidbody2D _rb;
    
    private GM_BattleMode _battleMode;

    private void Awake()
    {
        _playerInput = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        _battleMode = transform.parent.GetComponent<GM_BattleMode>();
        if ( _battleMode == null ) 
            throw new System.NullReferenceException("GM_BattleMode is not found! Please check if the parent has GM_BattleMode component.");
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
