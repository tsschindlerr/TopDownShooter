using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager instance;
    public ParticleSystem deathVFX;
    public ParticleSystem powerupVFX;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);        
    }

    public void PlayDeathVFX(Vector3 position)
    {
        Debug.Log("Spawning VFX at " + position);
        Instantiate(deathVFX, position, Quaternion.identity);
    }
    public void PlayPowerupVFX(Vector3 position)
    {
        Debug.Log("Spawning VFX at " + position);
        Instantiate(powerupVFX, position, Quaternion.identity);
    }
}

