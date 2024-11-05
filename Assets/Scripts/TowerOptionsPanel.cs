using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerOptionsPanel : MonoBehaviour
{
    public GameObject optionsPanel; // Panel z przyciskami "Upgrade" i "Delete"
    private Tower selectedTower; // Aktualnie wybrana wie�a

    // Otwieranie panelu opcji dla wybranej wie�y
    public void Open(Tower tower)
    {
        selectedTower = tower;
        optionsPanel.SetActive(true);

        // Ustawienie pozycji panelu nad klikni�t� wie��
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(tower.transform.position);
        screenPosition.y += 60; // Dostosowanie wysoko�ci panelu
        optionsPanel.transform.position = screenPosition;
    }

    // Zamkni�cie panelu opcji
    public void Close()
    {
        optionsPanel.SetActive(false);
        selectedTower = null;
    }

    // Funkcja wywo�ywana przez przycisk "Delete"
    public void DeleteTower()
    {
        if (selectedTower != null)
        {
            selectedTower.DeleteTower();
        }
    }

    // Funkcja wywo�ywana przez przycisk "Upgrade"
    public void UpgradeTower()
    {
        if (selectedTower != null)
        {
            selectedTower.UpgradeTower();
        }
    }
}
