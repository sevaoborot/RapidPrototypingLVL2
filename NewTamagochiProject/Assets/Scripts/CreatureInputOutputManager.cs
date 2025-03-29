using System.IO;
using UnityEngine;

public class CreatureInputOutputManager : MonoBehaviour
{
    private CreatureNeeds _needs;

    private string _jsonPath;

    public void OnInitialize(CreatureNeeds needs)
    {
        _jsonPath = Path.Combine(Application.persistentDataPath, "petData.json");
        _needs = needs;

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
        _needs.SetCreatureNeedsValues(90f, 100f, 100f, 100f);
        SaveData();
    }

    private void SaveData()
    {
        string json = _needs.ToJSON();
        File.WriteAllText(_jsonPath, json);
    }

    private void LoadData()
    {
        if (File.Exists(_jsonPath))
        {
            string json = File.ReadAllText(_jsonPath);
            _needs.SetCreatureNeedsValues(json);
            Debug.Log(_needs.ToString());
        }
        else
        {
            CreateStartCreatureNeeds();
        }
    }
}
