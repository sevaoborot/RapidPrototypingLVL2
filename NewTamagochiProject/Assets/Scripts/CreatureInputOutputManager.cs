using System.IO;
using UnityEngine;

public class CreatureInputOutputManager : MonoBehaviour
{
    private CreatureNeeds _needs;

    private string _jsonPath;

    private void Start()
    {
        _jsonPath = Path.Combine(Application.persistentDataPath, "petData.json");

        if (!PlayerPrefs.HasKey("FirstGameLaunch"))
        {
            Debug.Log("Первый запуск игры!");
            CreateStartCreatureNeeds();
            PlayerPrefs.SetInt("FirstGameLaunch", 1);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("Уже не первый запуск!");
            LoadData();
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) SaveData();
    }

    private void CreateStartCreatureNeeds()
    {
        _needs = new CreatureNeeds(100f, 100f, 100f, 100f);
        SaveData();
    }

    private void SaveData()
    {
        Debug.Log(_needs.ToString());
        string json = _needs.ToJSON();
        Debug.Log(json);
        File.WriteAllText(_jsonPath, json);
        Debug.Log($"Данные сохранены: {_jsonPath}");
    }

    private void LoadData()
    {
        if (File.Exists(_jsonPath))
        {
            string json = File.ReadAllText(_jsonPath);
            _needs = new CreatureNeeds(json);
            _needs.ToString();
        }
        else
        {
            Debug.Log("Файл сохранения не найден, создаём новые данные.");
            CreateStartCreatureNeeds();
        }
    }
}
