using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplode : MonoBehaviour
{
    public float explosionRadius = 5f; // Zasi�g obszaru bomby
    public float explosionDamage = 50f; // Jednorazowe obra�enia

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzamy, czy obiekt, kt�ry wszed� w obszar, to przeciwnik
        if (other.CompareTag("Enemy"))
        {
            // Wykryj wszystkich przeciwnik�w w obr�bie eksplozji
            Collider2D[] enemiesInRadius = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

            // Iteracja po wszystkich obiektach, kt�re znajduj� si� w obszarze eksplozji
            foreach (Collider2D collider in enemiesInRadius)
            {
                // Sprawdzamy, czy obiekt to przeciwnik
                if (collider.CompareTag("Enemy"))
                {
                    // Pobieramy komponent EnemyController
                    EnemyController enemyController = collider.GetComponent<EnemyController>();
                    if (enemyController != null)
                    {
                        // Zadajemy obra�enia
                        Debug.Log($"Dealing {explosionDamage} damage to {collider.name}");
                        enemyController.OnDamage(explosionDamage);
                    }
                }
            }

            // Po wybuchu nie musimy ju� niszczy� bomby, je�li chcesz mo�esz to zrobi�:
            // Destroy(gameObject); 
        }
    }

    // Wizualizacja obszaru eksplozji w edytorze
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius); // Zasi�g obszaru
    }
}
