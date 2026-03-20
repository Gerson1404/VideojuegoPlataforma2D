using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [Header("Siguiente nivel")]
    public string nextLevelName;

    [Header("Opciones")]
    public float delay = 1f;

    private bool activated = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated) return;

        if (collision.CompareTag("Player"))
        {
            activated = true;
            Invoke("LoadNextLevel", delay);
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
