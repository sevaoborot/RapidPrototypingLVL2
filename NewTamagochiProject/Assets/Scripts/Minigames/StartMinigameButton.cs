using System.IO;
using UnityEngine;

public class StartMinigameButton : MonoBehaviour
{
    [SerializeField] private Minigame _minigame;
    [SerializeField] private GameObject _window;
    [SerializeField] private GameObject _description;
    [SerializeField] private GameObject _endDescription;

    public void StartButton()
    {
        File.WriteAllText(Application.persistentDataPath + "/clicktest.txt", "hello");
        _minigame.OnInitialize(EndGameScreen);
        _description.SetActive(false);
        _window.SetActive(false);
    }

    private void EndGameScreen()
    {
        _window.SetActive(true);
        _endDescription.SetActive(true);
    }
}
