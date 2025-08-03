using UnityEngine;
using System.Collections.Generic;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public List<GameObject> weapons; // Inspector'dan eklenebilir
    private int currentWeaponIndex = 0;
    private float maxHealth = 100f;
    private float currentHealth;

    private float horizontal;
    private float vertical;
    public float speed = 100.0f;
    public Vector2 movement;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchWeapon();
        }
    }

    private void FixedUpdate()
    {
        movement = new Vector2(horizontal, vertical);
        rigidbody.linearVelocity = new Vector2(horizontal * speed, vertical * speed) * Time.fixedDeltaTime;
    }

    private void SwitchWeapon()
    {
        // Mevcut silah� kapat
        weapons[currentWeaponIndex].SetActive(false);

        // S�radaki silah� aktif et
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
        weapons[currentWeaponIndex].SetActive(true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            currentHealth -= 5f;
            if(currentHealth <= 0)
            {
                //öldün
                Destroy(gameObject);
            }
        }
    }
}
