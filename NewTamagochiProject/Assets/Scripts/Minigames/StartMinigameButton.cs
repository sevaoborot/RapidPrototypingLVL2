using UnityEngine;

public class StartMinigameButton : MonoBehaviour
{
    [SerializeField] private Minigame _minigame;
    [SerializeField] private GameObject _window;

    public void StartButton()
    {
        _minigame.OnInitialize();
        _window.SetActive(false);
    }
}
