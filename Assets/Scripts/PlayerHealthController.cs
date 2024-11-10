using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public float playerHealth = 100f;
    public float maxHealth = 100f;
    public Image healthBar;

    void Start()
    {
        UpdateHealthBar();
    }

    public void DecreaseHealth(float damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            GameOver();
        }
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = playerHealth / maxHealth;
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        // Logika zakoñczenia gry
    }
}

