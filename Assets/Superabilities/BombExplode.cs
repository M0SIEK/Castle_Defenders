using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplode : MonoBehaviour
{
    public float explosionRadius = 5f; // Zasiêg obszaru bomby
    public float explosionDamage = 50f; // Jednorazowe obra¿enia

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzamy, czy obiekt, który wszed³ w obszar, to przeciwnik
        if (other.CompareTag("Enemy"))
        {
            // Wykryj wszystkich przeciwników w obrêbie eksplozji
            Collider2D[] enemiesInRadius = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

            // Iteracja po wszystkich obiektach, które znajduj¹ siê w obszarze eksplozji
            foreach (Collider2D collider in enemiesInRadius)
            {
                // Sprawdzamy, czy obiekt to przeciwnik
                if (collider.CompareTag("Enemy"))
                {
                    // Pobieramy komponent EnemyController
                    EnemyController enemyController = collider.GetComponent<EnemyController>();
                    if (enemyController != null)
                    {
                        // Zadajemy obra¿enia
                        Debug.Log($"Dealing {explosionDamage} damage to {collider.name}");
                        enemyController.OnDamage(explosionDamage);
                    }
                }
            }

            // Po wybuchu nie musimy ju¿ niszczyæ bomby, jeœli chcesz mo¿esz to zrobiæ:
            // Destroy(gameObject); 
        }
    }

    // Wizualizacja obszaru eksplozji w edytorze
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius); // Zasiêg obszaru
    }
}
