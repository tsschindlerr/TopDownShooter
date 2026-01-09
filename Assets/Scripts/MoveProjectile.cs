using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    private float speed = 60f;
    private float projectileTopBound = 60f;
    private float projectileSideBound = 60f;
     
    void Update()
    {
        ProjectileMovement();
    }

    private void ProjectileMovement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (transform.position.x > projectileSideBound)
        {
            Destroy(gameObject);
        }

        if (transform.position.x < -projectileSideBound)
        {
            Destroy(gameObject);
        }

        if (transform.position.z > projectileTopBound)
        {
            Destroy(gameObject);
        }

        if (transform.position.z < -projectileTopBound)
        {
            Destroy(gameObject);
        }
    }
}
