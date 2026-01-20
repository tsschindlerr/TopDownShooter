using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameUIHandler : MonoBehaviour
{
    //pause menu
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private bool paused;

    //game over screen
    [SerializeField] private TextMeshProUGUI gameOverText;
    public bool gameOver = false;

    //player name
    [SerializeField] private TextMeshProUGUI playerNameText;

    //score
    [SerializeField] private TextMeshProUGUI scoreText;
    private GameManager gameManager;

    private void Start()
    {
        if (SaveData.instance != null)
        {
            playerNameText.text = SaveData.instance.playerName;
            gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        }
    }
    private void Update()
    {
        if (!paused && Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
        else if (paused && Input.GetKeyDown(KeyCode.P))
        {
            AudioManager.instance.PlayButtonSFX();
            paused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }

            scoreText.text = "Score: " + gameManager.score.ToString();
    }

    private void Pause()
    {
        if (!paused)
        {
            AudioManager.instance.PlayButtonSFX();
            paused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void BackToMenu()
    {
        AudioManager.instance.PlayButtonSFX();
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        AudioManager.instance.PlayGameOverSFX();
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0;
        gameOver = true;
    }

    public void Restart()
    {
        AudioManager.instance.PlayButtonSFX();
        SceneManager.LoadScene(3);
        Time.timeScale = 1;
        gameOver = false;
    }
}