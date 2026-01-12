using UnityEngine;
using UnityEngine.InputSystem;

public class GameUIHandler : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool paused;
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
}
