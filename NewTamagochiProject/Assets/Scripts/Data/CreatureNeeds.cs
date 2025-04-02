using System;
using UnityEngine;

[System.Serializable]
public class CreatureNeeds 
{
    [SerializeField] private float _health;
    [SerializeField] private float _hunger;
    [SerializeField] private float _happiness;
    [SerializeField] private float _sleep;

    public event Action<float> OnHealthChanged;
    public event Action<float> OnHungerChanged;
    public event Action<float> OnHappinessChanged;
    public event Action<float> OnSleepChanged;

    public float health
    {
        get => _health;
        set
        {
            if (Mathf.Approximately(_health, value)) return; 
            _health = value;
            OnHealthChanged?.Invoke(_health);
        }
    }
    public float hunger
    {
        get => _hunger;
        set
        {
            if (Mathf.Approximately(_hunger, value)) return;
            _hunger = value;
            OnHungerChanged?.Invoke(hunger);
        }
    }
    public float happiness
    {
        get => _happiness;
        set
        {
            if (Mathf.Approximately(_happiness, value)) return;
            _happiness = value;
            OnHappinessChanged?.Invoke(_happiness);
        }
    }
    public float sleep
    {
        get => _sleep;
        set
        {
            if (Mathf.Approximately(_sleep, value)) return;
            _sleep = value;
            OnSleepChanged?.Invoke(_sleep);
        }
    }

    public CreatureNeeds()
    {
        //Debug.Log("Нужды проинициализированы");
    }

    public void SetCreatureNeedsValues(float health, float hunger, float happiness, float sleep)
    {
        this.health = Mathf.Clamp(health, 0, 100);
        this.hunger = Mathf.Clamp(hunger, 0, 100);
        this.happiness = Mathf.Clamp(happiness, 0, 100);
        this.sleep = Mathf.Clamp(sleep, 0, 100);
    }

    public void SetCreatureNeedsValues(CreatureNeeds needs)
    {
        this.health = needs.health;
        this.hunger = needs.hunger; 
        this.happiness= needs.happiness;
        this.sleep = needs.sleep;
    }

    public void InvokeAllNeedsNethods()
    {
        OnHealthChanged?.Invoke(_health);
        OnHungerChanged?.Invoke(_hunger);
        OnHappinessChanged?.Invoke(_happiness);
        OnSleepChanged?.Invoke(_sleep);
    }

    public override string ToString()
    {
        return $"Health={health}, hunger={hunger}, happiness={happiness}, sleep={sleep}";
    }
}
