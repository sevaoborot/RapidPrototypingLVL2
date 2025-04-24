using UnityEngine;

public class MaingameInputOutputData
{
    private InputOutputManager _ioManager;

    public MaingameInputOutputData(string fileName)
    {
        _ioManager = new InputOutputManager(fileName);
    }

    public void SaveData(CreatureNeeds needs, bool isSleeping)
    {
        PlayerPrefs.SetString("LastSaveTime", System.DateTime.Now.ToString());
        PlayerPrefs.Save();
        GameData gameData = new GameData(needs, isSleeping);
        _ioManager.SaveToFile<GameData>(gameData);
    }

    public GameData LoadData() => _ioManager.LoadFromFile<GameData>();
}
