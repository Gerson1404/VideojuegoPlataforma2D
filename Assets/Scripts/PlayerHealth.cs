using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("UI")]
    public GameObject deathText;
    public UIHearts uiHearts;

    [Header("Caída")]
    public float fallLimit = -10f;

    private bool isDead = false; // 🔥 evita múltiples muertes

    void Start()
    {
        currentHealth = maxHealth;

        if (deathText != null)
            deathText.SetActive(false);

        if (uiHearts != null)
            uiHearts.UpdateHearts(currentHealth);
    }

    void Update()
    {
        // 🔥 MUERTE POR CAÍDA
        if (!isDead && transform.position.y < fallLimit)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // 🔥 evita daño después de muerto

        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;

        Debug.Log("Vida: " + currentHealth);

        if (uiHearts != null)
            uiHearts.UpdateHearts(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return; // 🔥 evita que se ejecute varias veces
        isDead = true;

        Debug.Log("El jugador murió");

        if (deathText != null)
            deathText.SetActive(true);

        if (uiHearts != null)
            uiHearts.gameObject.SetActive(false);

        GetComponent<PlayerController>().enabled = false;

        Invoke("RestartLevel", 2f);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
