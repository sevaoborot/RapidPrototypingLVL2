using UnityEngine;

public class GM_Bootstrap : MonoBehaviour
{
    [SerializeField] private PL_Movement _playerMovement;
    [SerializeField] private PL_DialougeActivation _playerDialouge;
    [SerializeField] private UI_Dialouges _dialougesUI;

    private PlayerControls _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerControls();
        _playerMovement.InitializeMovement(_playerInput);
        _dialougesUI.InitializeDialougesUI();
        _playerDialouge.InitializeDialogues(_playerInput, _dialougesUI);
    }
}
