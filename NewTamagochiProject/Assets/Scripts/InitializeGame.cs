using UnityEngine;

public class InitializeGame : MonoBehaviour
{
    [SerializeField] private CreatureNeedsUIManager _creatureUI;

    private CreatureNeeds _needs;
    private InputOutputManager _inputManager;

    private void Awake()
    {
        _needs = new CreatureNeeds();
        _inputManager = new InputOutputManager();

        GameData loadedData = _inputManager.LoadData();
        if (loadedData != null ) _needs.SetCreatureNeedsValues(loadedData.creatureNeeds);
        else _needs.SetCreatureNeedsValues(100f, 100f, 100f, 100f);

        _creatureUI.OnInitialize(_needs);
        _needs.InvokeAllNeedsNethods();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) _inputManager.SaveData(_needs);
    }

    private void OnApplicationQuit()
    {
        _inputManager.SaveData(_needs);
    }
}
