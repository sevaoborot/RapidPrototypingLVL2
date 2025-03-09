using UnityEngine;

public class UI_Buttons : MonoBehaviour
{
    private string _buttonGameObjName;

    private void Awake()
    {
        _buttonGameObjName = gameObject.name;
    }

    public void ButtonDebug()
    {
        //Debug.LogWarning($"{_buttonGameObjName} is pressed");
    }
}