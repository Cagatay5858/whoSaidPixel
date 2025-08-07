using UnityEngine;

public class BulletEnemy : MonoBehaviour
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
            //particles

            //sound

            //screen shake

            //damage enemy
            if (other.gameObject.CompareTag("Player"))
            {
                MakeDamage(other.gameObject.GetComponent<CharacterMovement>());
            }
            

            Destroy(gameObject);
        }
    }
    private void SetStraightVelocity()
    {
        rb.linearVelocity = transform.right * bulletSpeed;
    }
    private void SetDestroyTime()
    {
        Destroy(gameObject, destroyTime);
    }
    private void MakeDamage(CharacterMovement giveDamage)
    {
        giveDamage.TakeDamage(bulletDamage);
    }
}
