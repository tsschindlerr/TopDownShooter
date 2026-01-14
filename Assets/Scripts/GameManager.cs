using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    private GameUIHandler gameUIHandler;
    private void Start()
    {
        UpdateScore(0);
    }

    private void Update()
    {
        if (gameUIHandler.gameOver)
        {
            if (score > SaveData.instance.GetHighscore())
            {
                SaveData.instance.SaveHighscore(score);
            }
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
}
