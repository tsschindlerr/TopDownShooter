using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    private void Start()
    {
        UpdateScore(0);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
}
