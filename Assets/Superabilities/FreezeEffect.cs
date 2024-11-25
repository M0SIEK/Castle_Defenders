using UnityEngine;
using System.Collections;

public class FreezeEffect : MonoBehaviour
{
    public float freezeDuration = 5f; // Ca³kowity czas trwania zamro¿enia
    private float currentFreezeTime; // Pozosta³y czas zamro¿enia

    private void Start()
    {
        currentFreezeTime = freezeDuration; // Ustaw pocz¹tkowy czas zamro¿enia
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzenie, czy obiekt to przeciwnik (tag "Enemy")
        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"Enemy {other.name} entered freeze area.");
            EnemyController enemyController = other.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                // Rozpocznij zamra¿anie przeciwnika
                StartCoroutine(FreezeEnemy(enemyController));
            }
        }
    }

    private IEnumerator FreezeEnemy(EnemyController enemyController)
    {
        Debug.Log($"Freezing enemy {enemyController.name} for {currentFreezeTime} seconds.");

        // Zapisz oryginaln¹ prêdkoœæ przeciwnika
        float originalSpeed = enemyController.speed;

        // Zmniejsz prêdkoœæ przeciwnika piêciokrotnie
        enemyController.speed = originalSpeed / 5f;

        // Rozpocznij timer zamro¿enia
        float freezeTimeRemaining = currentFreezeTime;

        while (freezeTimeRemaining > 0)
        {
            yield return null; // Czekaj na ka¿d¹ klatkê
            freezeTimeRemaining -= Time.deltaTime; // Zmniejsz pozosta³y czas
        }

        // Przywróæ oryginaln¹ prêdkoœæ
        enemyController.speed = originalSpeed;
        Debug.Log($"Enemy {enemyController.name} thawed and can move again.");

        // Zniszcz obiekt FreezeEffect po zakoñczeniu dzia³ania
        Destroy(gameObject);
    }
}
