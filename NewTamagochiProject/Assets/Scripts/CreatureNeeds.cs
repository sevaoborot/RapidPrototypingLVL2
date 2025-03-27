using UnityEngine;

public class CreatureNeeds 
{
    [SerializeField] private float _health;
    [SerializeField] private float _hunger;
    [SerializeField] private float _happiness;
    [SerializeField] private float _sleep;

    public CreatureNeeds(float health, float hunger, float happiness, float sleep)
    {
        _health = Mathf.Clamp(health, 0, 100);
        _hunger = Mathf.Clamp(hunger, 0, 100);
        _happiness = Mathf.Clamp(happiness, 0, 100);
        _sleep = Mathf.Clamp(sleep, 0, 100);
    }

    public CreatureNeeds(string json)
    {
        FromJSON(json);
    }

    public string ToJSON()
    {
        return JsonUtility.ToJson(this);
    }

    public void FromJSON(string json)
    {
        CreatureNeeds data = JsonUtility.FromJson<CreatureNeeds>(json);
        _health = Mathf.Clamp(data._health, 0, 100);
        _hunger = Mathf.Clamp(data._hunger, 0, 100);
        _happiness = Mathf.Clamp(data._happiness, 0, 100);
        _sleep = Mathf.Clamp(data._sleep, 0, 100);
    }

    public override string ToString()
    {
        return $"Health={_health}, hunger={_hunger}, happiness={_happiness}, sleep={_sleep}";
    }
}
