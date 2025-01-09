using UnityEngine;
using System.Collections;

public class PoisonEffect : MonoBehaviour
{
    public float poisonDuration = 5f; // Czas trwania trucizny
    public float poisonDamage = 10f;  // Ilo�� obra�e� zadawanych co p� sekundy

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzenie, czy obiekt to przeciwnik (tag "Enemy")
        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"Enemy {other.name} entered poison area.");
            EnemyController enemyController = other.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                // Rozpocznij zadawanie obra�e� trucizn�
                StartCoroutine(ApplyPoisonDamage(enemyController));
            }
        }
    }

    private IEnumerator ApplyPoisonDamage(EnemyController enemyController)
    {
        Debug.Log($"Poisoning enemy {enemyController.name} for {poisonDuration} seconds.");

        // Zapisz oryginaln� ilo�� �ycia
        float originalHitPoints = enemyController.hitPoints;
        float currentPoisonTime = poisonDuration;

        // Co p� sekundy zadaj obra�enia
        while (currentPoisonTime > 0)
        {
            // Zadaj obra�enia trucizn�
            enemyController.hitPoints -= poisonDamage;
            enemyController.hitPointsBarController.UpdateHitPointsBar(enemyController.hitPoints, enemyController.maxHitPoints);
            Debug.Log($"Enemy {enemyController.name} took {poisonDamage} poison damage. Current HP: {enemyController.hitPoints}");

            // Zmniejsz pozosta�y czas trucizny
            currentPoisonTime -= 0.5f;

            // Czekaj p� sekundy przed kolejn� iteracj�
            yield return new WaitForSeconds(0.5f);
        }

        // Po up�ywie czasu trucizny, zako�cz efekt
        Debug.Log($"Poison effect on {enemyController.name} has ended.");
        Destroy(gameObject); // Zniszcz obiekt PoisonEffect po zako�czeniu dzia�ania
    }
}