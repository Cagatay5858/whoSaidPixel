using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public float damageAmount = 10f;
    public float damageInterval = 1f;

    private Coroutine damageCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Coroutine baþlat
            damageCoroutine = StartCoroutine(DamageOverTime(other.GetComponent<CharacterMovement>()));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Coroutine durdur
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private System.Collections.IEnumerator DamageOverTime(CharacterMovement health)
    {
        while (true)
        {
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
