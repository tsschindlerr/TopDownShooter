using UnityEngine;
using UnityEngine.SceneManagement;

public class HighscoresUIHandler : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
