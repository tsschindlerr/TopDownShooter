using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour

{
    //player movement
    public float speed;

    private float horizontalInput;
    private float forwardInput;
    public float turnSpeed;
    public GameObject player;

    public float topBound;
    public float sideBound;
    
    //firing projectiles
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    //animation
    public bool isMoving;
    private Animator animator;

    void Start()
    {
    animator = GameObject.Find("Player Model").GetComponent<Animator>();
    
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

    //Limits player moevement to given area
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

    //destroy player on collision with enemy
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Debug.Log("Player collided with Enemy");
        }
    }

    //destroy powerup on collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            Debug.Log("Player collected Powerup");
        }
    }

    //fire a projectile on click
    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(projectilePrefab, projectileSpawnPoint.position, player.transform.rotation);
        }
    }

    void AnimationTrigger()
    {
        if(isMoving)
        {
            animator.SetFloat("Speed_f", 1);
        }
        else
        {
            animator.SetFloat("Speed_f", 0);
        }
    }
    


}
