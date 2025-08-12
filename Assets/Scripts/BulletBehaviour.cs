using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 15f;
    [SerializeField] private float destroyTime = 3f;
    [SerializeField] private LayerMask whatDestroysBullet;

    private float bulletDamage = 5f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetDestroyTime();
        SetStraightVelocity();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((whatDestroysBullet.value & (1 << other.gameObject.layer)) > 0)
        {

        }
            //particles

            //sound

            //screen shake

            //damage enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            Robot rangedEnemy = other.gameObject.GetComponent<Robot>();
            Mummy meleEnemy = other.gameObject.GetComponent<Mummy>();

            if(meleEnemy != null)
            {
                meleEnemy.Damage(bulletDamage);
            }
            if(rangedEnemy != null)
            {
                rangedEnemy.Damage(bulletDamage);
            }
        }



            Destroy(gameObject);
    }
    private void SetStraightVelocity()
    {
        rb.linearVelocity = transform.right * bulletSpeed;
    }
    private void SetDestroyTime()
    {
        Destroy(gameObject, destroyTime);
    }
}
