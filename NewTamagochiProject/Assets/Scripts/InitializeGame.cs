using UnityEngine;

public class InitializeGame : MonoBehaviour
{
    [SerializeField] private CreatureInputOutputManager _creatureIO;
    [SerializeField] private CreatureNeedsUIManager _creatureUI;

    private CreatureNeeds _needs;

    private void Awake()
    {
        _needs = new CreatureNeeds();
        _creatureIO.OnInitialize(_needs);
        _creatureUI.OnInitialize(_needs);
        _needs.InvokeAllNeedsNethods();
    }
}
