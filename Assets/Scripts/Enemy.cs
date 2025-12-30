using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float bottomBound;
    public float rotationSpeed;
    private Rigidbody enemyRb;
    private GameObject player;
    private Vector3 lookDirection;

    //animation
    public bool isEnemyMoving;
    private Animator animator;

    void Start()
    {
        //access essential components for enemy movement
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        animator = GetComponentInChildren<Animator>();
    }


    void FixedUpdate()
    {
        //create a vector to face the player + add speed
        if (player != null)
        {
            lookDirection = (player.transform.position - transform.position).normalized;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, lookDirection.normalized, rotationSpeed * Time.fixedDeltaTime, 0f);
            Quaternion rotation = Quaternion.LookRotation(newDirection);
            enemyRb.MoveRotation(rotation);
            enemyRb.linearVelocity = new Vector3(newDirection.x * speed, enemyRb.linearVelocity.y,newDirection.z * speed);
            isEnemyMoving = true;
        }
        else
        {
            lookDirection = transform.position;
            isEnemyMoving = false;
        }

        //if enemy falls under the map = destroy
        if (transform.position.y < bottomBound)
        {
            Destroy(gameObject);
        }

        AnimationTrigger();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            Debug.Log("Enemy collided with Projectile");
        }
    }

    void AnimationTrigger()
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
}
