using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class NPC_Dialouge : MonoBehaviour
{
    [SerializeField] private SO_Dialouges[] dialouges;

    private bool _wasTriggeredBefore = false;

    public IReadOnlyList<DialougesData> GetDialogues()
    {
        if (!_wasTriggeredBefore)
        {
            _wasTriggeredBefore = true;
            return dialouges[0].DialougesData;
        }
        else
        {
            return dialouges[1].DialougesData;
        }
    }
}
