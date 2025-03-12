using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Dialouges : MonoBehaviour
{
    [SerializeField] private Image _NPCImage;
    [SerializeField] private TextMeshProUGUI _NPCDialougeSpace;

    public event Action DialogueFinished;

    private Canvas _dialogueCanvas;
    private Queue<DialougesData> _dialougesQueue;

    public void InitializeDialougesUI()
    {
        _dialogueCanvas = GetComponent<Canvas>();
        _dialogueCanvas.enabled = false;
        _dialougesQueue = new Queue<DialougesData>();
        Debug.Log("Dialouges UI is initialized successfully!");
    }

    public void GetDialougeData(IReadOnlyList<DialougesData> dialougeData)
    {
        foreach (DialougesData dialougeDataUnit in dialougeData)
        {
            _dialougesQueue.Enqueue(dialougeDataUnit);
        }
    }

    public void UpdateDialogueUI()
    {
        if (!_dialogueCanvas.isActiveAndEnabled)
        {
            _dialogueCanvas.enabled = true;
        }
        if (_dialougesQueue.Count > 0)
        {
            DialougesData currentDialogue = _dialougesQueue.Dequeue();
            _NPCDialougeSpace.text = currentDialogue.dialougeText;
        } else
        {
            _dialogueCanvas.enabled = false;
            DialogueFinished?.Invoke();
        }
    }
}
