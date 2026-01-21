using UnityEngine;

public class Enemy : MonoBehaviour
{
    //enemy movement
    public float speed;
    public float bottomBound;
    public float rotationSpeed;
    private Rigidbody enemyRb;
    private GameObject player;
    private Vector3 lookDirection;

    //animation
    public bool isEnemyMoving;
    private Animator animator;

    //points on kill
    public int pointValue;
    private GameManager gameManager;
    private PlayerController playerController;

    //enemy lives
    public int enemyLives;

    void Start()
    {
        //access essential components for enemy movement
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        animator = GetComponentInChildren<Animator>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }


    void FixedUpdate()
    {
        MoveEnemy();
        OutOfBounds();
        AnimationTrigger();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Projectile"))
        {
            VFXManager.instance.PlayDeathVFX(gameObject.transform.position);
            gameManager.UpdateScore(pointValue);
            Debug.Log("Enemy collided with Projectile");
            Destroy(other.gameObject);
            enemyLives -= 1;
            if (enemyLives <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void AnimationTrigger()
    {
        if (isEnemyMoving)
        {
            animator.SetFloat("Speed_f", 1);
        }
        else
        {
            animator.SetFloat("Speed_f", 0);
        }
    }

    private void MoveEnemy()
    {
        //create a vector to face the player + add speed
        if (player != null)
        {
            lookDirection = (player.transform.position - transform.position).normalized;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, lookDirection.normalized, rotationSpeed * Time.fixedDeltaTime, 0f);
            Quaternion rotation = Quaternion.LookRotation(newDirection);
            enemyRb.MoveRotation(rotation);
            enemyRb.linearVelocity = new Vector3(newDirection.x * speed, enemyRb.linearVelocity.y, newDirection.z * speed);
            isEnemyMoving = true;
        }
        else
        {
            lookDirection = transform.position;
            isEnemyMoving = false;
        }
    }

    private void OutOfBounds()
    {
        //if enemy falls under the map = destroy
        if (transform.position.y < bottomBound)
        {
            Destroy(gameObject);
        }
    }
}