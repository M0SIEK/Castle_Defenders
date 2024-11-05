using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerOptionsPanel : MonoBehaviour
{
    public GameObject optionsPanel; // Panel z przyciskami "Upgrade" i "Delete"
    private Tower selectedTower; // Aktualnie wybrana wie¿a

    // Otwieranie panelu opcji dla wybranej wie¿y
    public void Open(Tower tower)
    {
        selectedTower = tower;
        optionsPanel.SetActive(true);

        // Ustawienie pozycji panelu nad klikniêt¹ wie¿¹
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(tower.transform.position);
        screenPosition.y += 60; // Dostosowanie wysokoœci panelu
        optionsPanel.transform.position = screenPosition;
    }

    // Zamkniêcie panelu opcji
    public void Close()
    {
        optionsPanel.SetActive(false);
        selectedTower = null;
    }

    // Funkcja wywo³ywana przez przycisk "Delete"
    public void DeleteTower()
    {
        if (selectedTower != null)
        {
            selectedTower.DeleteTower();
        }
    }

    // Funkcja wywo³ywana przez przycisk "Upgrade"
    public void UpgradeTower()
    {
        if (selectedTower != null)
        {
            selectedTower.UpgradeTower();
        }
    }
}
