using UnityEngine;
using UnityEngine.UIElements;



public enum PowerupType
{
    None, DeathOnTouch, KillemAll, SpeedBoost
}

public class Powerup : MonoBehaviour
{
    [SerializeField] private float powerupRotationSpeed;
    public PowerupType powerupType;
       
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