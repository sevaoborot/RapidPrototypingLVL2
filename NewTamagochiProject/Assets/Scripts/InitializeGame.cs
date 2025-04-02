using UnityEngine;

public class InitializeGame : MonoBehaviour
{
    [SerializeField] private CreatureNeedsUIManager _creatureUI;
    [SerializeField] private CreatureNeedsChange _creatureNeedsChange;

    private CreatureNeeds _needs;
    private InputOutputManager _inputManager;

    private void Awake()
    {
        _needs = new CreatureNeeds();
        _inputManager = new InputOutputManager();
        GameData loadedData = new GameData(_needs);

        if (!PlayerPrefs.HasKey("FirstGameRun"))
        {
            Debug.Log("Первый запуск игры!");
            PlayerPrefs.SetInt("FirstGameRun", 1);
            PlayerPrefs.SetString("LastSaveTime", System.DateTime.Now.ToString());
            _needs.SetCreatureNeedsValues(100f, 100f, 100f, 100f);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("Уже не первый раз заходите, не так ли?");
            loadedData = _inputManager.LoadData();
            if (loadedData != null) _needs.SetCreatureNeedsValues(loadedData.creatureNeeds);
            else _needs.SetCreatureNeedsValues(100f, 100f, 100f, 100f);
        }     
        
        Debug.Log(_needs.ToString());

        _creatureUI.OnInitialize(_needs);
        _needs.InvokeAllNeedsNethods();
        _creatureNeedsChange.OnInitialize(_needs);
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
