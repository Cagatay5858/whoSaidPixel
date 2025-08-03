using UnityEngine;

public class Robot : RangedEnemys
{
    public Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(Target != null)
        {
            if (Vector2.Distance(Target.position, transform.position) >= distanceToStop)
            {
                rb.linearVelocity = transform.up * speed;
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
        
    }
    private void Update()
    {
        if (!Target)
        {
            GetTarget();
        }
        else
        {
            RotateTowardsTarget();
        }

        if(Target != null && Vector2.Distance(Target.position, transform.position) <= distanceToShoot)
        {
            Shoot();
        }
    }
}
