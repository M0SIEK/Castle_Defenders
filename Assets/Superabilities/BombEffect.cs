using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffect : MonoBehaviour
{
    public GameObject explosionEffectPrefab;  // Prefab eksplozji, kt�ry jest wywo�ywany po detonacji
    public float effectDuration = 0.5f;  // Czas, po kt�rym efekt eksplozji zniknie (w sekundach)

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzenie, czy obiekt, kt�ry wszed� w obszar, to przeciwnik
        if (other.CompareTag("Enemy"))
        {
            // Wywo�anie eksplozji
            Explode();
        }
    }

    private void Explode()
    {
        // Tworzenie efektu eksplozji w miejscu bomby
        if (explosionEffectPrefab != null)
        {
            GameObject explosionEffect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

            // Zniszczenie efektu eksplozji po 0.5 sekundy
            Destroy(explosionEffect, effectDuration);
        }

        // Zniszczenie bomby po eksplozji
        Destroy(gameObject);
    }
}

