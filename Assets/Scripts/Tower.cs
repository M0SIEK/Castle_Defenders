using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerOptionsPanel towerOptionsPanel; // Odwo�anie do panelu opcji wie�y
    public GameObject upgradedTowerPrefab; // Prefabrykowana wersja ulepszonej wie�y
    private EmptyField originalField; // Pole, na kt�rym wie�a zosta�a postawiona

    private void OnMouseDown()
    {
        // Otw�rz panel opcji wie�y po klikni�ciu
        towerOptionsPanel.Open(this);
    }

    // Przypisanie pola, na kt�rym wie�a zosta�a postawiona
    public void SetOriginalField(EmptyField field)
    {
        originalField = field;
    }

    // Funkcja usuwaj�ca wie��
    public void DeleteTower()
    {
        // Aktywuj ponownie pole, na kt�rym wie�a zosta�a postawiona
        if (originalField != null)
        {
            originalField.gameObject.SetActive(true);
        }
        Destroy(gameObject); // Zniszcz obiekt wie�y
        towerOptionsPanel.Close(); // Zamknij panel opcji
    }

    // Funkcja do ulepszania wie�y
    public void UpgradeTower()
    {
        if (upgradedTowerPrefab != null)
        {
            Instantiate(upgradedTowerPrefab, transform.position, transform.rotation); // Tworzenie ulepszonej wersji wie�y
            DeleteTower(); // Usuni�cie starej wie�y
        }
    }
}
