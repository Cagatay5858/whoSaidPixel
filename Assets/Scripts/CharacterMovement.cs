using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public List<GameObject> weapons; // Inspector'dan eklenebilir
    private int currentWeaponIndex = 0;
    private float maxHealth = 100f;
    private float currentHealth;
    public TextMeshProUGUI healthText;

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
        healthText.text = "Health : " + currentHealth;
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

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Hasar alındı: " + amount + " | Kalan can: " + currentHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Karakter öldü!");
        // Ölüm animasyonu, sahne sıfırlama vs.
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            currentHealth -= 5f;
            if (currentHealth <= 0)
            {
                //öldün
                Destroy(gameObject);
            }
        }

        if (other.CompareTag("Door"))
        {
            SceneController.Instance.LoadNextScene();
        }
    }

}
