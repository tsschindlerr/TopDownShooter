using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField InputField;
    public void GrabPlayerName()
    {
        string playerName;
        playerName = InputField.text;
        SaveData.instance.playerName = playerName;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void Settings()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitApp()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
