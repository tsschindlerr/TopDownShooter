using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    public string highscoreName;
    private GameUIHandler gameUIHandler;
    private void Start()
    {
        gameUIHandler = FindAnyObjectByType<GameUIHandler>();
        UpdateScore(0);
        highscoreName = SaveData.instance.playerName;
    }

    private void Update()
    {
        if (gameUIHandler.gameOver)
        {
            if (score > SaveData.instance.GetHighscore())
            {
                SaveData.instance.SaveHighscore(score, highscoreName);
            }
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
}
