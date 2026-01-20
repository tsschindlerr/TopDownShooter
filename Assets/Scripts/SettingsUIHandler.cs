using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsUIHandler : MonoBehaviour
{
    public void BackToMenu()
    {
        AudioManager.instance.PlayButtonSFX();
        SceneManager.LoadScene(0);
    }
}