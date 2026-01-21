using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager instance;
    public ParticleSystem deathVFX;
    public ParticleSystem playerDeathVFX;
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
    public void PlayPlayerDeathVFX(Vector3 position)
    {
        Debug.Log("Spawning VFX at " + position);
        Instantiate(deathVFX, position, Quaternion.identity);
    }
}

