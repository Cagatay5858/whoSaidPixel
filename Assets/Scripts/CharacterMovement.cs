using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField]
    private Animator healthBarAnim;

    [SerializeField]
    private Animator characterAnimator;

    public string healthBarParameterName;


    public Rigidbody2D rigidbody;
    public List<GameObject> weapons;// Inspector'dan eklenebilir
    public List<GameObject> inventory;
    private int currentWeaponIndex = 0;
    private float maxHealth = 100f;
    public float currentHealth;
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
        healthBarAnim.SetFloat(healthBarParameterName, currentHealth);
        healthText.text = ""+currentHealth;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        bool isMoving = (horizontal != 0f || vertical != 0f);
        characterAnimator.SetBool("IsMoving", isMoving);



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
        inventory[currentWeaponIndex].SetActive(false);

        // S�radaki silah� aktif et
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
        weapons[currentWeaponIndex].SetActive(true);
        inventory[currentWeaponIndex].SetActive(true);
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
