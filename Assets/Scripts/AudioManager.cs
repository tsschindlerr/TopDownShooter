using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    //main menu
    private AudioSource gameAudio;
    public AudioClip buttonSFX;

    //gameplay sfx
    public AudioClip powerupSFXDOT;
    public AudioClip powerupSFXKA;
    public AudioClip powerupSFXSB;
    public AudioClip shootSFX;
    public AudioClip gameOverSFX;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        gameAudio = GetComponent<AudioSource>();
    }

    public void PlayButtonSFX()
    {
        gameAudio.PlayOneShot(buttonSFX, 1.0f);
    }

    public void PlayShootSFX()
    {
        gameAudio.PlayOneShot(shootSFX, 1.0f);
    }

    public void PlayPowerupDOTSFX()
    {
        gameAudio.PlayOneShot(powerupSFXDOT, 1.0f);
    }

    public void PlayPowerupKASFX()
    {
        gameAudio.PlayOneShot(powerupSFXKA, 1.0f);
    }
    public void PlayPowerupSBSFX()
    {
        gameAudio.PlayOneShot(powerupSFXSB, 1.0f);
    }

    public void PlayGameOverSFX()
    {
        gameAudio.PlayOneShot(gameOverSFX, 1.0f);
    }
}