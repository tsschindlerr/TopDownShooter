using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager instance;
    public ParticleSystem deathParticle;
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

    public void PlayDeathParticle()
    {
        deathParticle.Play();
    }
}
