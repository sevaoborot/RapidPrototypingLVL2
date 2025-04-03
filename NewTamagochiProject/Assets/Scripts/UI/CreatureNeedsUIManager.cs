using System;
using TMPro;
using UnityEngine;

public class CreatureNeedsUIManager : MonoBehaviour
{
    [SerializeField] private HealthUIElement _healthUI;
    [SerializeField] private HungerUIElement _hungerUI;
    [SerializeField] private HappinessUIElement _happinessUI;
    [SerializeField] private SleepUIElement _sleepUI;

    [Header("Debug (should be removed/replaced later)")]
    [SerializeField] private TextMeshProUGUI _sleepButtonText;

    private CreatureNeeds _needs;

    public event Action<bool> OnSleepButtonPressed;

    private bool _wasPressed = false;

    public void OnInitialize(CreatureNeeds needs)
    {
        _needs = needs;

        _healthUI.OnInitialize(_needs);
        _hungerUI.OnInitialize(_needs);
        _happinessUI.OnInitialize(_needs);
        _sleepUI.OnInitialize(_needs);
    }

    public void SleepButton()
    {
        if (!_wasPressed)
        {
            _sleepButtonText.text = "Sleep: on";
            _wasPressed = true;
            OnSleepButtonPressed?.Invoke(true);
        } else
        {
            _sleepButtonText.text = "Sleep: off";
            _wasPressed = false;
            OnSleepButtonPressed?.Invoke(false);
        }
    }
}
