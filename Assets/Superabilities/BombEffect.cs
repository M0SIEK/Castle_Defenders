using UnityEngine;

public class BombEffect : MonoBehaviour
{
    public GameObject explosionEffectPrefab; // Prefab eksplozji
    public float effectDuration = 0.5f; // Czas trwania efektu eksplozji

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        if (explosionEffectPrefab != null)
        {
            GameObject explosionEffect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            Destroy(explosionEffect, effectDuration);
        }

        Destroy(gameObject);
    }
}