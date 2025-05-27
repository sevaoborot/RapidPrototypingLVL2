using UnityEngine;

public class StartMinigameButton : MonoBehaviour
{
    [SerializeField] private Minigame _minigame;
    [SerializeField] private GameObject _window;
    [SerializeField] private GameObject _description;
    [SerializeField] private GameObject _endDescription;

    public void StartButton()
    {
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
