using System;
using System.Collections;
using UnityEngine;

public class CreatureNeedsChange : MonoBehaviour
{
    [SerializeField] private float _healthUpdateInterval;
    [SerializeField] private float _hungerUpdateInterval;
    [SerializeField] private float _happinessUpdateInterval;
    [SerializeField] private float _sleepUpdateInterval;

    private CreatureNeeds _needs;

    private Coroutine _healthCoroutine;
    private Coroutine _hungerCoroutine;
    private Coroutine _happinessCoroutine;
    private Coroutine _sleepCoroutine;

    public void OnInitialize(CreatureNeeds needs)
    {
        _needs = needs;

        string lastSaveTimeUnparsed = PlayerPrefs.GetString("LastSaveTime");
        TimeSpan InactiveTime = DateTime.Now - DateTime.Parse(lastSaveTimeUnparsed);
        Debug.Log($"Игрок отсутствовал в игре {InactiveTime.Seconds} секунд");

        _needs.health -= InactiveTime.Seconds / _healthUpdateInterval;
        _needs.hunger -= InactiveTime.Seconds / _hungerUpdateInterval;
        _needs.happiness -= InactiveTime.Seconds / _happinessUpdateInterval;
        _needs.sleep -= InactiveTime.Seconds / _sleepUpdateInterval;

        _healthCoroutine = StartCoroutine(NeedChange(
            () => _needs.health,
            value => _needs.health = value,
            _healthUpdateInterval
            ));
        _hungerCoroutine = StartCoroutine(NeedChange(
            () => _needs.hunger,
            value => _needs.hunger = value,
            _hungerUpdateInterval
            ));
        _happinessCoroutine = StartCoroutine(NeedChange(
            () => _needs.happiness,
            value => _needs.happiness = value,
            _happinessUpdateInterval
            ));
        _sleepCoroutine = StartCoroutine(NeedChange(
            () => _needs.sleep,
            value => _needs.sleep = value,
            _sleepUpdateInterval
            ));
    }

    private IEnumerator NeedChange(Func<float> needGetter, Action<float> needSetter, float needUpdateInterval)
    {
        while (true)
        {
            yield return new WaitForSeconds(needUpdateInterval);
            float newNeedValue = needGetter() - 1;
            needSetter(newNeedValue);
        }
    }
}
