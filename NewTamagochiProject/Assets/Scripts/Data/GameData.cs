[System.Serializable]
public class GameData 
{
    public CreatureNeeds creatureNeeds;
    public bool isSleeping;

    public GameData(CreatureNeeds needs, bool isSleeping = false)
    {
        creatureNeeds = needs;
        this.isSleeping = isSleeping;
    }
}
