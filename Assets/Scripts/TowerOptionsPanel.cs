using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerOptionsPanel : MonoBehaviour
{
    public GameObject optionsPanel; // Panel z przyciskami "Upgrade" i "Delete"
    private Tower selectedTower; // Aktualnie wybrana wie¿a

    public float focusedHeightOffset = 100f; // Wysokoœæ panelu w trybie Play Focused
    public float maximizedHeightOffset = 150f; // Wysokoœæ panelu w trybie Play Maximized

    // Otwieranie panelu opcji dla wybranej wie¿y
    public void Open(Tower tower)
    {
        if (selectedTower == tower && optionsPanel.activeSelf)
        {
            Close(); // Jeœli klikniêto na tê sam¹ wie¿ê, zamknij panel
            return;
        }

        selectedTower = tower;
        optionsPanel.SetActive(true);

        // Ustal wysokoœæ offsetu na podstawie trybu wyœwietlania
        float heightOffset = (Screen.width > 1000 && Screen.height > 600) ? maximizedHeightOffset : focusedHeightOffset;

        // Ustawienie pozycji panelu nad klikniêt¹ wie¿¹
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(tower.transform.position);
        screenPosition.y += heightOffset; // Dostosowanie wysokoœci panelu

        // Pobierz rozmiary panelu
        RectTransform optionsRectTransform = optionsPanel.GetComponent<RectTransform>();
        float panelHeight = optionsRectTransform.rect.height;
        float panelWidth = optionsRectTransform.rect.width;

        // Sprawdzenie, czy panel wychodzi poza górn¹ krawêdŸ ekranu
        if (screenPosition.y + panelHeight > Screen.height)
        {
            // Jeœli wychodzi poza górn¹ krawêdŸ, ustaw pozycjê pod wie¿¹
            screenPosition.y = Camera.main.WorldToScreenPoint(tower.transform.position).y - (panelHeight + 7f);
        }

        // Ograniczenie pozycji panelu do krawêdzi ekranu
        screenPosition.x = Mathf.Clamp(screenPosition.x, panelWidth / 2, Screen.width - panelWidth / 2);

        // Ustaw finaln¹ pozycjê panelu
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
