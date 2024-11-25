using UnityEngine;
using System.Collections;

public class PoisonEffect : MonoBehaviour
{
    public float poisonDuration = 5f; // Czas trwania trucizny
    public float poisonDamage = 10f;  // Iloœæ obra¿eñ zadawanych co pó³ sekundy

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzenie, czy obiekt to przeciwnik (tag "Enemy")
        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"Enemy {other.name} entered poison area.");
            EnemyController enemyController = other.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                // Rozpocznij zadawanie obra¿eñ trucizn¹
                StartCoroutine(ApplyPoisonDamage(enemyController));
            }
        }
    }

    private IEnumerator ApplyPoisonDamage(EnemyController enemyController)
    {
        Debug.Log($"Poisoning enemy {enemyController.name} for {poisonDuration} seconds.");

        // Zapisz oryginaln¹ iloœæ ¿ycia
        float originalHitPoints = enemyController.hitPoints;
        float currentPoisonTime = poisonDuration;

        // Co pó³ sekundy zadaj obra¿enia
        while (currentPoisonTime > 0)
        {
            // Zadaj obra¿enia trucizn¹
            enemyController.hitPoints -= poisonDamage;
            enemyController.hitPointsBarController.UpdateHitPointsBar(enemyController.hitPoints, enemyController.maxHitPoints);
            Debug.Log($"Enemy {enemyController.name} took {poisonDamage} poison damage. Current HP: {enemyController.hitPoints}");

            // Zmniejsz pozosta³y czas trucizny
            currentPoisonTime -= 0.5f;

            // Czekaj pó³ sekundy przed kolejn¹ iteracj¹
            yield return new WaitForSeconds(0.5f);
        }

        // Po up³ywie czasu trucizny, zakoñcz efekt
        Debug.Log($"Poison effect on {enemyController.name} has ended.");
        Destroy(gameObject); // Zniszcz obiekt PoisonEffect po zakoñczeniu dzia³ania
    }
}
