using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsUIHandler : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
