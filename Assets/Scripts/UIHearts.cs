using UnityEngine;
using UnityEngine.UI;

public class UIHearts : MonoBehaviour
{
    public Image[] hearts;

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }
}
