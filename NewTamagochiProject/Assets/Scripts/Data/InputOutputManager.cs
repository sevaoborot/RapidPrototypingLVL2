using System.IO;
using UnityEngine;

public class InputOutputManager
{
    private string _jsonPath;

    public InputOutputManager()
    {
        _jsonPath = Path.Combine(Application.persistentDataPath, "petData.json");
    }

    public void SaveData(CreatureNeeds needs)
    {
        PlayerPrefs.SetString("LastSaveTime", System.DateTime.Now.ToString());
        PlayerPrefs.Save();
        GameData gameData = new GameData(needs);
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(_jsonPath, json);
        Debug.Log("Сохранено в " + _jsonPath);
    }

    public GameData LoadData()
    {
        if (File.Exists(_jsonPath))
        {
            string json = File.ReadAllText(_jsonPath);
            return JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            Debug.LogWarning("Файла сохранения не существует!");
            return null;
        }
    }
}
