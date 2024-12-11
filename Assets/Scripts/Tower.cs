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

    private GameObject rangeIndicator; // Obiekt wizualizujący zasięg wieży

    private void Start()
    {
        // Tworzymy obiekt zasięgu i konfigurujemy go, ale go ukrywamy
        rangeIndicator = new GameObject("RangeIndicator");
        rangeIndicator.transform.SetParent(transform);
        rangeIndicator.transform.localPosition = Vector3.zero;

        SpriteRenderer rangeRenderer = rangeIndicator.AddComponent<SpriteRenderer>();
        rangeRenderer.sprite = CreateCircleSprite(); // Tworzenie okręgu
        rangeRenderer.color = new Color(0f, 1f, 0f, 0.08f); // Jasnozielony, półprzezroczysty kolor
        rangeRenderer.sortingOrder = 1; // Ustawienie, by okrąg był pod wieżą

        // Uwzględnienie skali wieży
        Vector2 towerScale = transform.localScale; // Skala wieży
        rangeIndicator.transform.localScale = new Vector2(range * 1.1f * towerScale.x, range * 1.1f * towerScale.y);        // Dopasowanie skali do zasięgu i skali wieży

        rangeIndicator.SetActive(false); // Domyślnie ukryty
    }


    private void OnMouseDown()
    {
        // Wyświetlenie lub ukrycie wskaźnika zasięgu po kliknięciu
        bool isActive = rangeIndicator.activeSelf;
        rangeIndicator.SetActive(!isActive);

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

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);

        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distanceToEnemy = Vector2.Distance(transform.position, collider.transform.position);

                // Uwzględnienie promienia kolidera przeciwnika
                float enemyRadius = collider.bounds.extents.magnitude;
                if (distanceToEnemy - enemyRadius < range)
                {
                    // Debugowanie: przeciwnik wchodzi w okrąg atakowania przez wieżę
                    if (distanceToEnemy < shortestDistance)
                    {
                        shortestDistance = distanceToEnemy;
                        nearestEnemy = collider.transform;
                    }
                }
            }
        }

        target = nearestEnemy != null ? nearestEnemy : null;
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

                // Debugowanie - pierwszy atak
                if (fireCountdown == 1f / fireRate) // Pierwszy strzał
                {
                    Debug.Log("Pierwszy atak wieży wystrzelony!");
                }

                projectile.Seek(target);
            }
            else
            {
                Debug.LogError("Brak skryptu 'Projectile' na pocisku!");
            }
        }
    }

    private Sprite CreateCircleSprite()
    {
        // Tworzymy okrąg jako teksturę dla wskaźnika zasięgu
        Texture2D texture = new Texture2D(128, 128);
        texture.filterMode = FilterMode.Bilinear;

        Color transparent = new Color(0, 0, 0, 0);
        Color solid = Color.white;

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                float radius = texture.width / 2;
                float distance = Vector2.Distance(new Vector2(x, y), new Vector2(radius, radius));
                texture.SetPixel(x, y, distance < radius ? solid : transparent);
            }
        }

        texture.Apply();
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}
