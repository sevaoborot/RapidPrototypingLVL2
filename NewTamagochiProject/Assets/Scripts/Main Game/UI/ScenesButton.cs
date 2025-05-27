using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesButton : MonoBehaviour
{
    public void ChangeScene(int scene) => SceneManager.LoadScene(scene);
}
