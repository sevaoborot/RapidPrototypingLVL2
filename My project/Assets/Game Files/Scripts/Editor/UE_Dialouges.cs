using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SO_Dialogues))]
public class UE_Dialouges : Editor
{
    private SO_Dialogues dialogueData;
    private SerializedProperty elementsProp;

    private void OnEnable()
    {
        dialogueData = (SO_Dialogues)target;
        elementsProp = serializedObject.FindProperty("elements");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Dialogue Elements", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        for (int i = 0; i < elementsProp.arraySize; i++)
        {
            SerializedProperty elementProp = elementsProp.GetArrayElementAtIndex(i);
            var element = dialogueData.elements[i];

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.LabelField($"Element {i}", EditorStyles.boldLabel);

            if (element is DialogueData)
            {
                SerializedProperty dialogueTextProp = elementProp.FindPropertyRelative("dialogueText");
                SerializedProperty spriteProp = elementProp.FindPropertyRelative("dialogueNPCSprite");
                SerializedProperty nextElementIDProp = elementProp.FindPropertyRelative("nextElementID");

                EditorGUILayout.LabelField("Type: DialogueData");
                EditorGUILayout.PropertyField(dialogueTextProp, new GUIContent("Dialogue Text"));
                EditorGUILayout.PropertyField(spriteProp, new GUIContent("NPC Sprite"));
                EditorGUILayout.PropertyField(nextElementIDProp, new GUIContent("Next element ID"));
            }
            else if (element is ReplyOptions)
            {
                SerializedProperty optionsProp = elementProp.FindPropertyRelative("options");

                EditorGUILayout.LabelField("Type: ReplyOptions");
                EditorGUILayout.PropertyField(optionsProp, new GUIContent("Reply Options"), true);
            }
            else
            {
                EditorGUILayout.HelpBox("Unknown element type", MessageType.Warning);
            }

            if (GUILayout.Button("Remove"))
            {
                elementsProp.DeleteArrayElementAtIndex(i);
                break;
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Add New Element", EditorStyles.boldLabel);

        if (GUILayout.Button("Add DialogueData"))
        {
            DialogueData newElement = new DialogueData();
            dialogueData.elements.Add(newElement);
        }

        if (GUILayout.Button("Add ReplyOptions"))
        {
            ReplyOptions newElement = new ReplyOptions { options = new List<ReplyOption>() };
            dialogueData.elements.Add(newElement);
        }

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed) 
        {
            EditorUtility.SetDirty(dialogueData);
        }
    }
}
