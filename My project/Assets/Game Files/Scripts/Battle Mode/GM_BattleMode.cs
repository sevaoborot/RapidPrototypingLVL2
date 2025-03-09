using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GM_BattleMode : MonoBehaviour
{
    [Header("Player settings")]
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _starterPosition;

    [Header("UI settings")]
    [SerializeField] private Button[] _buttons;
    
    private BattleStateController _battleStateController;

    private void Start()
    {
        _battleStateController = new BattleStateController(new PlayerActionState(), _player, _starterPosition, _buttons); 
        _battleStateController.battleState.SetState(_battleStateController);
        StartCoroutine(SwitchToEnemy()); 
    }

    public void SwitchBattleState()
    {
        _battleStateController.SwitchState();
    }

    private IEnumerator SwitchToEnemy()
    {
        while (true)
        {
            if (_battleStateController.battleState is PlayerDefendingState)
            {
                yield return new WaitForSeconds(5);
                SwitchBattleState();
            }
            yield return null;
        }
    }
}

#region Battle state

public class BattleStateController
{
    public IBattleState battleState;
    public GameObject player;
    public Transform starterPosition;
    public Button[] buttons;

    public BattleStateController(IBattleState battleState, GameObject player, Transform starterPosition, Button[] buttons)
    {
        this.battleState = battleState;
        this.player = player;
        this.starterPosition = starterPosition;
        this.buttons = buttons;
    }

    public void SwitchState()
    {
        battleState.ExitState(this);
        battleState.SetState(this);
    }
}

public interface IBattleState
{
    public void SetState(BattleStateController _battleStateController);
    public void ExitState(BattleStateController _battleStateController);
}

public class PlayerActionState : IBattleState
{
    public PlayerActionState()
    {
        
    }

    public void SetState(BattleStateController _battleStateController)
    {
        _battleStateController.player.SetActive(false);
        foreach(Button button in _battleStateController.buttons) button.interactable = true;
        _battleStateController.buttons[0].Select();
    }

    public void ExitState(BattleStateController _battleStateController)
    {
        _battleStateController.battleState = new PlayerDefendingState();
    }
}

public class PlayerDefendingState : IBattleState
{
    public PlayerDefendingState()
    {
        
    }

    public void SetState(BattleStateController _battleStateController)
    {
        _battleStateController.player.SetActive(true);
        _battleStateController.player.transform.position = _battleStateController.starterPosition.position;
        foreach (Button button in _battleStateController.buttons) button.interactable = false;
    }

    public void ExitState(BattleStateController _battleStateController)
    {
        _battleStateController.battleState = new PlayerActionState();
    }
}

#endregion