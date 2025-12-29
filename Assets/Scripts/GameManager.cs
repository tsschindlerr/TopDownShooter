using UnityEngine;

public class GameManager : MonoBehaviour
{// add/take lives, add/take score, UI, strat/restart, pause/start
    private SpawnManager spawnManager;
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    
    void Update()
    {
       
    }
}
