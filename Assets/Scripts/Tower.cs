using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerOptionsPanel towerOptionsPanel; // Panel opcji wieży
    public TowerDeleteOptionsPanel towerDeleteOptionsPanel; // Panel usuwania wieży
    private EmptyField originalField; // Pole, na którym wieża została postawiona
    private bool isUpgraded = false; // Flaga do sprawdzania, czy wieża została ulepszona
    private MonoBehaviour activeOptionsPanel; // Przechowuje aktywny panel (TowerOptionsPanel lub TowerDeleteOptionsPanel)
    public int level = 1; // Zmienna reprezentująca poziom wieży, domyślnie ustawiona na 1
    public GameObject upgradedTowerPrefab; // Prefab ulepszonej wieży (do przypisania w Unity)
    public GameObject projectilePrefab; // Prefab pocisku
    public float range = 10f; // Zasięg ataku
    public float fireRate = 1f; // Czas między strzałami (w sekundach)
    private float fireCountdown = 0f; // Licznik do kolejnego strzału
    private Transform target; // Cel dla pocisku
    public Transform firePoint;
    public bool isTowerPlaced = false; // Flaga sprawdzająca, czy wieża jest postawiona

    private void OnMouseDown()
    {
        // W zależności od poziomu wieży (isUpgraded) otwieramy odpowiedni panel
        if (level == 3)
        {
            activeOptionsPanel = towerDeleteOptionsPanel;
        }
        else if (level == 1 || level == 2)
        {
            activeOptionsPanel = towerOptionsPanel;
        }

        // Otwieramy odpowiedni panel
        if (activeOptionsPanel != null)
        {
            if (activeOptionsPanel is TowerOptionsPanel optionsPanel)
            {
                optionsPanel.Open(this);
            }
            else if (activeOptionsPanel is TowerDeleteOptionsPanel deletePanel)
            {
                deletePanel.Open(this);
            }
        }
    }

    // Przypisanie pola, na którym wieża została postawiona
    public void SetOriginalField(EmptyField field)
    {
        originalField = field;
    }

    // Funkcja usuwająca wieżę
    public void DeleteTower()
    {
        isTowerPlaced = false; // Wieża przestaje działać
        // Aktywujemy ponownie pole, na którym wieża została postawiona
        if (originalField != null)
        {
            originalField.gameObject.SetActive(true); // Pole staje się aktywne
        }

        Destroy(gameObject); // Zniszcz wieżę
        if (activeOptionsPanel != null)
        {
            activeOptionsPanel.GetType().GetMethod("Close")?.Invoke(activeOptionsPanel, null); // Zamknij odpowiedni panel
        }
    }

    // Funkcja do ulepszania wieży
    public void UpgradeTower()
    {
        // Tworzymy ulepszoną wersję wieży na tej samej pozycji
        if (upgradedTowerPrefab != null)
        {
            GameObject upgradedTower = Instantiate(upgradedTowerPrefab, transform.position, transform.rotation);

            // Przypisujemy pole do nowej wieży i ustawiamy ją jako ulepszoną
            Tower towerScript = upgradedTower.GetComponent<Tower>();
            if (towerScript != null)
            {
                towerScript.SetOriginalField(originalField);
                towerScript.level = level + 1; // Zwiększamy poziom wieży
                towerScript.isTowerPlaced = true; // Ustawiamy nową wieżę jako postawioną
            }

            Destroy(gameObject); // Zniszcz stary obiekt wieży
            if (towerOptionsPanel != null)
            {
                towerOptionsPanel.Close(); // Zamknij panel opcji
            }
        }
    }

    private void Update()
    {
        // Logika działa tylko, gdy wieża jest postawiona
        if (isTowerPlaced)
        {
            UpdateTarget();

            if (target != null && fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate; // Ustawienie czasu do kolejnego strzału
            }

            fireCountdown -= Time.deltaTime; // Odliczanie czasu
        }
    }

    void UpdateTarget()
    {
        if (!isTowerPlaced) return; // Nie aktualizuj celu, jeśli wieża nie jest postawiona

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range); // Użyj OverlapCircleAll dla fizyki 2D

        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = collider.transform;
                }
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy;
        }
        else
        {
            target = null;
        }
    }

    void Shoot()
    {
        // Logika strzelania działa tylko, gdy wieża jest postawiona
        if (isTowerPlaced && projectilePrefab != null && target != null)
        {
            GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Projectile projectile = projectileGO.GetComponent<Projectile>();

            if (projectile != null)
            {
                Debug.Log("Utworzono pocisk: " + projectileGO.name);
                projectile.Seek(target);
            }
            else
            {
                Debug.LogError("Brak skryptu 'Projectile' na pocisku!");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Wizualizacja zasięgu wieży
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
