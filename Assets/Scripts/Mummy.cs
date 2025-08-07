using UnityEngine;

public class Mummy : AbsEnemys
{
    [SerializeField] private float maxHealth = 5f;

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
