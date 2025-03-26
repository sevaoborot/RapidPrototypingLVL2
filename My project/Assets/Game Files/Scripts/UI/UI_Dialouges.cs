using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Dialouges : MonoBehaviour
{
    [SerializeField] private Image _NPCImage;
    [SerializeField] private TextMeshProUGUI _NPCDialougeSpace;
    [SerializeField] private UI_Buttons _dialogueButtons;
    [SerializeField] private Canvas _dialogueButtonsCanvas;

    public event Action DialogueFinished;

    private Canvas _dialogueCanvas;
    private List<DialogueElement> _dialougeElements;
    private int _currentTextlineID = 0;

    public void InitializeDialougesUI()
    {
        _dialogueCanvas = GetComponent<Canvas>();
        _dialogueCanvas.enabled = false;
        _dialogueButtonsCanvas.enabled = false;
        _dialougeElements = new List<DialogueElement>();
        Debug.Log("Dialouges UI is initialized successfully!");
    }

    public void GetDialougeData(IReadOnlyList<DialogueElement> dialougeData)
    {
        _currentTextlineID = 0;
        _dialougeElements.Clear();
        _dialougeElements = dialougeData.ToList();
    }

    public void UpdateDialogueUI()
    {
        if (!_dialogueCanvas.isActiveAndEnabled) _dialogueCanvas.enabled = true;
        if (_currentTextlineID != _dialougeElements.Count)
        {
            switch (_dialougeElements[_currentTextlineID])
            {
                //заменить стратегией?

                case DialogueElement element when _dialougeElements[_currentTextlineID] is DialogueData:
                    DialogueData currentDialogue = (DialogueData)_dialougeElements[_currentTextlineID];
                    _dialogueButtonsCanvas.enabled = false;
                    _NPCDialougeSpace.text = currentDialogue.dialogueText;
                    _currentTextlineID = currentDialogue.nextElementID;
                    break;
                case DialogueElement element when _dialougeElements[_currentTextlineID] is ReplyOptions:
                    ReplyOptions currentOptions = (ReplyOptions)_dialougeElements[_currentTextlineID];
                    _dialogueButtonsCanvas.enabled = true;
                    _NPCDialougeSpace.text = "";
                    //
                    _currentTextlineID = currentOptions.options[0].nextElementID;
                    break;
            }
        }
        else
        {
            _dialogueCanvas.enabled = false;
            DialogueFinished?.Invoke();
        }
    }
}

