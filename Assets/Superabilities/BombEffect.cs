using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffect : MonoBehaviour
{
    public GameObject explosionEffectPrefab;  // Prefab eksplozji, który jest wywo³ywany po detonacji
    public float effectDuration = 0.5f;  // Czas, po którym efekt eksplozji zniknie (w sekundach)

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzenie, czy obiekt, który wszed³ w obszar, to przeciwnik
        if (other.CompareTag("Enemy"))
        {
            // Wywo³anie eksplozji
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

