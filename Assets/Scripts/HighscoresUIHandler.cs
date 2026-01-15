using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighscoresUIHandler : MonoBehaviour
{
    public TextMeshProUGUI highscoreCurrentText;
    private void Start()
    {                
        highscoreCurrentText.text = $"Current highscore: {SaveData.instance.highscore} by {SaveData.instance.highscoreName}";
        if (SaveData.instance.highscoreName == "")
        {
            highscoreCurrentText.text = $"Current highscore: {SaveData.instance.highscore} by ???";
        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
