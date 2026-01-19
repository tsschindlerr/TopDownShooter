using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour

{
    //player movement
    [SerializeField] private float speed;
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
    public bool hasPowerup;
    [SerializeField] private Material powerupIndicator;
    [SerializeField] private Material standardMaterial;
    [SerializeField] private SkinnedMeshRenderer playerMaterial;

    void Start()
    {
        animator = GameObject.Find("Player Model").GetComponent<Animator>();
        gameUIHandler = GameObject.Find("Canvas").GetComponent<GameUIHandler>();
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

    //destroy player on collision with enemy + destroy on touch powerup
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Destroy(collision.gameObject);
            Debug.Log("Player collided with " + collision.gameObject.name + " with Powerup set to " + hasPowerup);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            gameUIHandler.GameOver();
            Destroy(gameObject);
            Debug.Log("Player collided with " + collision.gameObject.name);
        }
       
    }

    //powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            Debug.Log("Player collected Powerup");
            playerMaterial.material = powerupIndicator;
            StartCoroutine(PowerupTimer());
        }
    }

    //powerup timer
    IEnumerator PowerupTimer()
    {
        yield return new WaitForSeconds(5);
        hasPowerup = false;
        playerMaterial.material = standardMaterial;
    }

    //fire a projectile on click
    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(projectilePrefab, projectileSpawnPoint.position, player.transform.rotation);
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
