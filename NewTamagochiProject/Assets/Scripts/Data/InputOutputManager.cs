using System.IO;
using UnityEngine;

public class InputOutputManager
{
    private string _fileName;
    private string _jsonPath;

    public InputOutputManager(string fileName)
    {
        _fileName = fileName;
        _jsonPath = Path.Combine(Application.persistentDataPath, _fileName);
    }

    public void SaveToFile<T>(T data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_jsonPath, json);
    }

    public T LoadFromFile<T>() where T: class
    {
        if (File.Exists(_jsonPath))
        {
            string json = File.ReadAllText(_jsonPath);
            return JsonUtility.FromJson<T>(json);
        }
        else
        {
            Debug.LogWarning("Файла сохранения не существует!");
            return null;
        }
    }
}
