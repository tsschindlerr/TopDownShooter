using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameUIHandler : MonoBehaviour
{
    //pause menu
    public GameObject pauseMenu;
    [SerializeField] private bool paused;

    //game over screen
    public TextMeshProUGUI gameOverText;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
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
}
