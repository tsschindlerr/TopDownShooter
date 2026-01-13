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

    //player name
    [SerializeField] private TextMeshProUGUI playerNameText;

    //score
    [SerializeField] private TextMeshProUGUI scoreText;
    private GameManager gameManager;


    private void Start()
    {
        if(SaveData.instance != null)
        {
            playerNameText.text = SaveData.instance.playerName;
            gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        }
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
        scoreText.text = "Score: " + gameManager.score.ToString();
    }

    private void Pause()
    {
        if(!paused)
        {
            paused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1;
    }
}
