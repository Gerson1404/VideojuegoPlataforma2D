using UnityEngine;

public class FireDamage : MonoBehaviour
{
    public int damage = 1;
    public float damageCooldown = 1f;

    private float lastDamageTime;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Time.time < lastDamageTime + damageCooldown)
                return;

            PlayerHealth player = collision.GetComponent<PlayerHealth>();

            if (player != null)
            {
                player.TakeDamage(damage);
                lastDamageTime = Time.time;
            }
        }
    }
}
