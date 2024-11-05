using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerOptionsPanel towerOptionsPanel; // Odwo³anie do panelu opcji wie¿y
    public GameObject upgradedTowerPrefab; // Prefabrykowana wersja ulepszonej wie¿y
    private EmptyField originalField; // Pole, na którym wie¿a zosta³a postawiona

    private void OnMouseDown()
    {
        // Otwórz panel opcji wie¿y po klikniêciu
        towerOptionsPanel.Open(this);
    }

    // Przypisanie pola, na którym wie¿a zosta³a postawiona
    public void SetOriginalField(EmptyField field)
    {
        originalField = field;
    }

    // Funkcja usuwaj¹ca wie¿ê
    public void DeleteTower()
    {
        // Aktywuj ponownie pole, na którym wie¿a zosta³a postawiona
        if (originalField != null)
        {
            originalField.gameObject.SetActive(true);
        }
        Destroy(gameObject); // Zniszcz obiekt wie¿y
        towerOptionsPanel.Close(); // Zamknij panel opcji
    }

    // Funkcja do ulepszania wie¿y
    public void UpgradeTower()
    {
        if (upgradedTowerPrefab != null)
        {
            Instantiate(upgradedTowerPrefab, transform.position, transform.rotation); // Tworzenie ulepszonej wersji wie¿y
            DeleteTower(); // Usuniêcie starej wie¿y
        }
    }
}
