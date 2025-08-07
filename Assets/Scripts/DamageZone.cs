using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public float damageAmount = 10f;
    public float damageInterval = 1f;

    private Coroutine damageCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Coroutine baþlat
            damageCoroutine = StartCoroutine(DamageOverTime(collision.gameObject.GetComponent<CharacterMovement>()));
        }
    }

    private void OnTriggernExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Coroutine durdur
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private System.Collections.IEnumerator DamageOverTime(CharacterMovement currentHealth)
    {
        while (true)
        {
            if (currentHealth != null)
            {
                currentHealth.TakeDamage(damageAmount);
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
