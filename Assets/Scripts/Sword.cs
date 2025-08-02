using UnityEngine;

public class Sword : InterfaceGuns
{
    private Animator animator;
    private float damage = 50.0f;

    private SpriteRenderer sprite;
    public Collider2D collider;
    public string attackTriggerName = "SwordAttack"; // Animator'daki trigger parametresi

    public override void Attack()
    {
        if (Input.GetMouseButtonDown(0)) // Sol t�k kontrol�
        {
            if (animator != null)
            {
                animator.SetTrigger(attackTriggerName);
            }

            collider.enabled = true;

            if (collider.CompareTag("Enemy"))
            {
                DamageTaken(damage);
            }
        }
    }

    void DisableCollider()
    {
        collider.enabled = false;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Aim();      // InterfaceGuns i�indeki virtual Aim fonksiyonu
        Attack();   // Override edilmi� versiyonu
    }

    void DamageTaken(float damage)
    {
        Debug.Log("Enemy took " + damage + " damage.");
    }
}
