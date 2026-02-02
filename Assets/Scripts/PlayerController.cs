using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour

{
    private GameManager gameManager;

    //player movement
    [SerializeField] private float baseSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float baseSpeedMultiplier;
    private float horizontalInput;
    private float forwardInput;
    [SerializeField] private float turnSpeed;
    [SerializeField] private GameObject player;
    [SerializeField] private float topBound;
    [SerializeField] private float sideBound;

    //firing projectiles
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    //animation
    [SerializeField] private bool isMoving;
    private Animator animator;

    //game over 
    private GameUIHandler gameUIHandler;

    //powerups
    public bool hasPowerupDOT;
    public bool hasPowerupKA;
    public bool hasPowerupSB;
    [SerializeField] private Material powerupIndicatorDOT;
    [SerializeField] private Material powerupIndicatorKA;
    [SerializeField] private Material powerupIndicatorSB;
    [SerializeField] private Material standardMaterial;
    [SerializeField] private SkinnedMeshRenderer playerMaterial;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        animator = GameObject.Find("Player Model").GetComponent<Animator>();
        gameUIHandler = GameObject.Find("Canvas").GetComponent<GameUIHandler>();
        baseSpeed = speed;
        SetBoundsForLevel();
    }


    void Update()
    {
        MovePlayer();
        ConstrainPlayerMove();        
        Fire();
        AnimationTrigger();
    }

    // moves the player based on WASD/arrow input
    void MovePlayer()
    {
        if (hasPowerupSB)
        {
            speed = baseSpeed * baseSpeedMultiplier;
        }
        else
        {
            speed = baseSpeed;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(horizontalInput, 0, forwardInput);

        if (movement.magnitude > 0.01f)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);

            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    //limits player moevement to given area
    void ConstrainPlayerMove()
    {
        if (transform.position.z > topBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, topBound);
        }
        if (transform.position.z < -topBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -topBound);
        }
        if (transform.position.x > sideBound)
        {
            transform.position = new Vector3(sideBound, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -sideBound)
        {
            transform.position = new Vector3(-sideBound, transform.position.y, transform.position.z);
        }

    }

    //different bounds for different levels
    void SetBoundsForLevel()
    {
        string levelName = SceneManager.GetActiveScene().name;

        switch (levelName)
        {
            case "Level1":
                topBound = 30f;
                sideBound = 30f;
                break;

            case "Level2":
                topBound = 30f;
                sideBound = 60f;
                break;

            case "Level3":
                topBound = 30f;
                sideBound = 50f;
                break;

        }
    }

    //destroy player on collision with enemy + powerupDOT activation
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerupDOT)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                gameManager.UpdateScore(enemy.pointValue);
            }

            VFXManager.instance.PlayDeathVFX(gameObject.transform.position);
            AudioManager.instance.PlayShootSFX();
            Destroy(collision.gameObject);
            Debug.Log("Player collided with " + collision.gameObject.name + " with Powerup set to " + hasPowerupDOT);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            gameUIHandler.GameOver();
            Destroy(gameObject);
            Debug.Log("Player collided with " + collision.gameObject.name);
        }

    }

    //powerup trigger + powerupKA & powerupSB activation
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerupDOT"))
        {
            VFXManager.instance.PlayPowerupVFX(gameObject.transform.position);
            AudioManager.instance.PlayPowerupDOTSFX();
            Destroy(other.gameObject);
            hasPowerupDOT = true;
            Debug.Log("Player collected PowerupDOT");
            playerMaterial.material = powerupIndicatorDOT;
            StartCoroutine(PowerupTimerDOT());
        }
        else if (other.gameObject.CompareTag("PowerupKA"))
        {
            VFXManager.instance.PlayPowerupVFX(gameObject.transform.position);
            AudioManager.instance.PlayPowerupKASFX();
            Destroy(other.gameObject);
            hasPowerupKA = true;
            playerMaterial.material = powerupIndicatorKA;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    gameManager.UpdateScore(enemyScript.pointValue);
                }
                VFXManager.instance.PlayDeathVFX(enemy.transform.position);
                Destroy(enemy);
            }
            Debug.Log("Player collected PowerupKA");
            Debug.Log("Enemies destroyed: " + enemies.Length + "PowerupKA set to " + hasPowerupKA);
            StartCoroutine(PowerupTimerKA());
        }
        else if (other.gameObject.CompareTag("PowerupSB"))
        {
            VFXManager.instance.PlayPowerupVFX(gameObject.transform.position);
            AudioManager.instance.PlayPowerupSBSFX();
            Destroy(other.gameObject);
            hasPowerupSB = true;
            playerMaterial.material = powerupIndicatorSB;
            Debug.Log("Player collected PowerupSB");
            Debug.Log("Player speed set to " + speed);
            StartCoroutine(PowerupTimerSB());
        }
    }

    //powerup timers
    IEnumerator PowerupTimerDOT()
    {
        yield return new WaitForSeconds(5);
        hasPowerupDOT = false;
        playerMaterial.material = standardMaterial;
    }

    IEnumerator PowerupTimerKA()
    {
        yield return new WaitForSeconds(0.5f);
        hasPowerupKA = false;
        playerMaterial.material = standardMaterial;
    }

    IEnumerator PowerupTimerSB()
    {
        yield return new WaitForSeconds(5);
        hasPowerupSB = false;
        playerMaterial.material = standardMaterial;
        Debug.Log("Player speed set to " + speed);
    }

    //fire a projectile on click
    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(projectilePrefab, projectileSpawnPoint.position, player.transform.rotation);
            AudioManager.instance.PlayShootSFX();
        }
    }

    //trigger animation on movement (workaround)
    void AnimationTrigger()
    {
        if (isMoving)
        {
            animator.SetFloat("Speed_f", 1);
        }
        else
        {
            animator.SetFloat("Speed_f", 0);
        }
    }
}