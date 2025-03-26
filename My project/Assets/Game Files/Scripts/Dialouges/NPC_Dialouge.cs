using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class NPC_Dialouge : MonoBehaviour
{
    [SerializeField] private SO_Dialogues[] dialouges;

    private bool _wasTriggeredBefore = false;

    public IReadOnlyList<DialogueElement> GetDialogues()
    {
        if (!_wasTriggeredBefore)
        {
            _wasTriggeredBefore = true;
            return dialouges[0].elements;
        }
        else
        {
            return dialouges[1].elements;
        }
    }
}
