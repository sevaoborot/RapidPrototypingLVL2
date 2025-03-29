using UnityEngine;

public class CreatureNeedsUIManager : MonoBehaviour
{
    [SerializeField] private HealthUIElement _healthUI;
    [SerializeField] private HungerUIElement _hungerUI;
    [SerializeField] private HappinessUIElement _happinessUI;
    [SerializeField] private SleepUIElement _sleepUI;

    private CreatureNeeds _needs;

    public void OnInitialize(CreatureNeeds needs)
    {
        _needs = needs;

        _healthUI.OnInitialize(_needs);
        _hungerUI.OnInitialize(_needs);
        _happinessUI.OnInitialize(_needs);
        _sleepUI.OnInitialize(_needs);

        Debug.Log("UI отображение потребностей проинициализированно!");
    }

    private void UpdateNeedUI()
    {

    }

    void Update()
    {
        
    }
}
