using UnityEngine;

public class Robot : AbsEnemys
{
    [SerializeField] private float maxHealth = 5f;

    private float currentHealth;
    public Rigidbody2D rb;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player");// Ba�lang��ta hedef al�nmal�
    }

    private void FixedUpdate()
    {
        // S�rekli kontrol et
        MakeRay();
        if (getHasOnSight())
        {
            float distance = Vector2.Distance(Target.transform.position, transform.position);

            if (distance >= distanceToStop)
            {
                // Hedefe do�ru y�nelip ilerle
                Vector2 direction = (Target.transform.position - transform.position).normalized;
                rb.linearVelocity = direction * speed * Time.fixedDeltaTime;
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void Update()
    {
        if (Target != null)
        {
            RotateTowardsTarget();

            float distance = Vector2.Distance(Target.transform.position, transform.position);
            if (distance <= distanceToShoot)
            {
                Shoot();
            }
        }
    }

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
