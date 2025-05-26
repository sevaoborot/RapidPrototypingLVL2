using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void OpenCloseMenu(GameObject menu) => menu.SetActive(!menu.activeSelf);
}
