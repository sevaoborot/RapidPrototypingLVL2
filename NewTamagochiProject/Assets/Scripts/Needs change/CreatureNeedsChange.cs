using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureNeedsChange : MonoBehaviour
{
    [SerializeField] private float _healthUpdateInterval;
    [SerializeField] private float _hungerUpdateInterval;
    [SerializeField] private float _happinessUpdateInterval;
    [SerializeField] private float _sleepUpdateInterval;

    private CreatureNeeds _needs;
    private CreatureNeedsUIManager _uiManager;

    private List<Coroutine> _needsCoroutines = new List<Coroutine>();

    private bool _isSubscribedOnSleepButton = false;

    public void OnInitialize(CreatureNeeds needs, CreatureNeedsUIManager uiManager, bool isSleeping)
    {
        _needs = needs;
        _uiManager = uiManager;

        string lastSaveTimeUnparsed = PlayerPrefs.GetString("LastSaveTime");
        TimeSpan InactiveTime = DateTime.Now - DateTime.Parse(lastSaveTimeUnparsed);
        Debug.Log($"Игрок отсутствовал в игре {InactiveTime.Seconds} секунд");

        _needs.health -= InactiveTime.Seconds / _healthUpdateInterval;
        _needs.hunger -= InactiveTime.Seconds / _hungerUpdateInterval;
        _needs.happiness -= InactiveTime.Seconds / _happinessUpdateInterval;
        _needs.sleep -= InactiveTime.Seconds / _sleepUpdateInterval;
        OnSleep(isSleeping);

        _uiManager.OnSleepButtonPressed += OnSleep;
    }

    private void StartNeedsCoroutines(float sleepNeedValue, float defaultNeedValue)
    {
        _needsCoroutines.Add(StartCoroutine(NeedChange(
            () => _needs.health,
            value => _needs.health = value,
            _healthUpdateInterval,
            defaultNeedValue
            )));
        _needsCoroutines.Add(StartCoroutine(NeedChange(
            () => _needs.hunger,
            value => _needs.hunger = value,
            _hungerUpdateInterval,
            defaultNeedValue
            )));
        _needsCoroutines.Add(StartCoroutine(NeedChange(
            () => _needs.happiness,
            value => _needs.happiness = value,
            _happinessUpdateInterval,
            defaultNeedValue
            )));
        _needsCoroutines.Add(StartCoroutine(NeedChange(
            () => _needs.sleep,
            value => _needs.sleep = value,
            _sleepUpdateInterval,
            sleepNeedValue
            )));
    }

    private void OnEnable()
    {
        if (_uiManager != null && !_isSubscribedOnSleepButton) _uiManager.OnSleepButtonPressed += OnSleep;
    }

    private void OnDisable()
    {
        _uiManager.OnSleepButtonPressed -= OnSleep;
    }

    private IEnumerator NeedChange(
        Func<float> needGetter, 
        Action<float> needSetter, 
        float needUpdateInterval, 
        float needChangeValue)
    {
        while (true)
        {
            yield return new WaitForSeconds(needUpdateInterval);
            float newNeedValue = needGetter() + needChangeValue;
            needSetter(newNeedValue);
        }
    }

    public void OnSleep(bool isSleeping)
    {
        if (isSleeping)
        {
            foreach(Coroutine coroutine in _needsCoroutines)
            {
                StopCoroutine(coroutine);
            }
            _needsCoroutines.Clear();

            StartNeedsCoroutines(1f, -0.5f);
            Debug.Log("Существо спит!");
        } else
        {
            foreach (Coroutine coroutine in _needsCoroutines)
            {
                StopCoroutine(coroutine);
            }
            _needsCoroutines.Clear();

            StartNeedsCoroutines(-1f, -1f);
            Debug.Log("Существо проснулось!");
        }
    }
}
