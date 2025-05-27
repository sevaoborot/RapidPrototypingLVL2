using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class StartMinigameButton : MonoBehaviour
{
    [SerializeField] private Minigame _minigame;
    [SerializeField] private GameObject _window;
    [SerializeField] private GameObject _description;
    [SerializeField] private GameObject _endDescription;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(StartButton);
    }

    public void StartButton()
    {
        File.WriteAllText(Application.persistentDataPath + "/clicktest.txt", "hello");
        _minigame.OnInitialize(foo);
        _description.SetActive(false);
        _window.SetActive(false);
    }

    private void foo()
    {
        _window.SetActive(true);
        _endDescription.SetActive(true);
    }
}
