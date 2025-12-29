using UnityEngine;
using UnityEngine.UIElements;
public enum PowerupType
{
    None, Immortality, AddLife, SuddenDeath
}

public class Powerup : MonoBehaviour
{
    public float powerupRotationSpeed;
    public PowerupType powerupType;
    void Start()
    {
        
    }

    
    void Update()
    {
        TurnAround();
    }

    private void TurnAround()
    {
        transform.Rotate(0, powerupRotationSpeed * Time.deltaTime, 0);
        transform.Rotate(0, 0, powerupRotationSpeed * Time.deltaTime);
    }
}
