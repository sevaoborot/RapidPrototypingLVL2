using UnityEngine;

public class CreatureNeeds 
{
    private float _health;
    private float _hunger;
    private float _happiness;
    private float _sleep;

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
        return JsonUtility.ToJson(new
        {
            health = _health,
            hunger = _hunger,
            happiness = _happiness,
            sleep = _sleep
        });
    }

    public void FromJSON(string json)
    {
        CreatureNeedsJSON data = JsonUtility.FromJson<CreatureNeedsJSON>(json);
        _health = Mathf.Clamp(data.health, 0, 100);
        _hunger = Mathf.Clamp(data.hunger, 0, 100);
        _happiness = Mathf.Clamp(data.happiness, 0, 100);
        _sleep = Mathf.Clamp(data.sleep, 0, 100);
    }

    public override string ToString()
    {
        return $"Health={_health}, hunger={_hunger}, happiness={_happiness}, sleep={_sleep}";
    }
}

public class CreatureNeedsJSON
{
    public float health;
    public float hunger;
    public float happiness;
    public float sleep;
}
