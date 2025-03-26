using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/Dialogues Data")]
public class SO_Dialogues : ScriptableObject
{
    [SerializeReference]
    public List<DialogueElement> elements = new List<DialogueElement>();
}

[System.Serializable]
public abstract class DialogueElement { }

[System.Serializable]
public class DialogueData : DialogueElement
{
    [Multiline(4)]
    public string dialogueText;
    public Sprite dialogueNPCSprite;
    public int nextElementID;
}

[System.Serializable]
public class ReplyOptions : DialogueElement
{
    public List<ReplyOption> options = new();
}

[System.Serializable]
public struct ReplyOption
{
    public string replyText;
    public int nextElementID;
}

