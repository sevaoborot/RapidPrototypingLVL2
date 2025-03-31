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

    public void OnInitialize(CreatureNeeds needs)
    {
        _needs = needs;
        StartCoroutine(NeedChange(
            () => _needs.health,
            value => _needs.health = value,
            _healthUpdateInterval
            ));
        StartCoroutine(NeedChange(
            () => _needs.hunger,
            value => _needs.hunger = value,
            _hungerUpdateInterval
            ));
        StartCoroutine(NeedChange(
            () => _needs.happiness,
            value => _needs.happiness = value,
            _happinessUpdateInterval
            ));
        StartCoroutine(NeedChange(
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
            Debug.Log($"Значение уменьшилось на 5. Новое значение - {needGetter}");
        }
    }
}
