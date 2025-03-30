[System.Serializable]
public class GameData 
{
    public CreatureNeeds creatureNeeds;
    public string lastSaveTime;

    public GameData(CreatureNeeds needs)
    {
        creatureNeeds = needs;
        lastSaveTime = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
    }
}
