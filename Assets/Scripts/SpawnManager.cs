using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    //enemy variables
    public GameObject[] enemyPrefabs;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnPosX;
    [SerializeField] private float spawnPosZ;
    [SerializeField] private float spawnRangeX;
    [SerializeField] private float spawnRangeZ;
    [SerializeField] private float startDelay;
    [SerializeField] private float spawnInterval;

    //powerup variables
    public GameObject[] powerupPrefabs;
    [SerializeField] private float spawnPowerupDelay;
    [SerializeField] private float spawnPowerupInterval;
    [SerializeField] private float spawnRangePowerup = 20;

    void Start()
    {
        InvokeRepeating(("SpawnEnemiesUp"), startDelay, spawnInterval);
        InvokeRepeating(("SpawnEnemiesRight"), startDelay, spawnInterval);
        InvokeRepeating(("SpawnEnemiesLeft"), startDelay, spawnInterval);
        InvokeRepeating(("SpawnEnemiesDown"), startDelay, spawnInterval);
        InvokeRepeating(("SpawnRandomPowerup"), spawnPowerupDelay, spawnPowerupInterval);
    }


    void Update()
    {

    }

    void SpawnEnemiesUp()
    {
        if (player != null)
        {

            int enemyIndexUp = Random.Range(0, enemyPrefabs.Length);
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeZ, spawnRangeZ), 5, spawnPosX);
            Vector3 directionToPlayer = (player.position - spawnPos).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            Instantiate(enemyPrefabs[enemyIndexUp], spawnPos, lookRotation);
        }
        else
        {
            return;
        }
    }

    void SpawnEnemiesRight()
    {
        if (player != null)
        {
            int enemyIndexRight = Random.Range(0, enemyPrefabs.Length);
            Vector3 spawnPos = new Vector3(spawnPosX, 5, Random.Range(-spawnRangeZ, spawnRangeZ));
            Vector3 directionToPlayer = (player.position - spawnPos).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            Instantiate(enemyPrefabs[enemyIndexRight], spawnPos, lookRotation);
        }
        else
        {
            return;
        }
    }

    void SpawnEnemiesLeft()
    {
        if (player != null)
        {
            int enemyIndexLeft = Random.Range(0, enemyPrefabs.Length);
            Vector3 spawnPos = new Vector3(-spawnPosX, 5, Random.Range(-spawnRangeZ, spawnRangeZ));
            Vector3 directionToPlayer = (player.position - spawnPos).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            Instantiate(enemyPrefabs[enemyIndexLeft], spawnPos, lookRotation);
        }
        else
        {
            return;
        }
    }

    void SpawnEnemiesDown()
    {
        if (player != null)
        {
            int enemyIndexDown = Random.Range(0, enemyPrefabs.Length);
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeZ, spawnRangeZ), 5, -spawnPosX);
            Vector3 directionToPlayer = (player.position - spawnPos).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            Instantiate(enemyPrefabs[enemyIndexDown], spawnPos, lookRotation);
        }
        else
        {
            return;
        }
    }

    private Vector3 PowerupSpawnPosition()
    {
        {
            float spawnPowerupPosX = Random.Range(-spawnRangePowerup, spawnRangePowerup);
            float spawnPowerupPosZ = Random.Range(-spawnRangePowerup, spawnRangePowerup);
            Vector3 randomPowerupPos = new Vector3(spawnPowerupPosX, 1, spawnPowerupPosZ);
            return randomPowerupPos;
        }
    }

    void SpawnRandomPowerup()
    {
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerup], PowerupSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);
    }
}
