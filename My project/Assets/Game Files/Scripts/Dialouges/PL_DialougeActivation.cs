using UnityEngine;
using UnityEngine.InputSystem;

public class PL_DialougeActivation : MonoBehaviour
{
    private PlayerControls _playerInput;
    private UI_Dialouges _dialogueUI;

    private bool _isNPCInTrigger = false;

    public void InitializeDialogues(PlayerControls playerInput, UI_Dialouges dialogueUI)
    {
        _playerInput = playerInput;
        _dialogueUI = dialogueUI;
        Debug.Log("Player dialouge activation is initialized successfully!");
    }

    private void OnEnable()
    {
        _playerInput.Player.Action.performed += ActionDialouge;
        _playerInput.Player.Enable();
        _dialogueUI.DialogueFinished += DialogueEnded;
    }

    private void OnDisable()
    {
        _playerInput.Player.Disable();
        _dialogueUI.DialogueFinished -= DialogueEnded;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<NPC_Dialouge>(out NPC_Dialouge dialougeData))
        {
            _dialogueUI.GetDialougeData(dialougeData.GetDialogues());
            _isNPCInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<NPC_Dialouge>() != null) _isNPCInTrigger = false;
    }

    private void ActionDialouge(InputAction.CallbackContext context)
    {
        if (_isNPCInTrigger)
        {
            _playerInput.Player.Movement.Disable();
            _dialogueUI.UpdateDialogueUI();
        }
    }

    private void DialogueEnded()
    {
        _playerInput.Player.Movement.Enable();
    }
}
