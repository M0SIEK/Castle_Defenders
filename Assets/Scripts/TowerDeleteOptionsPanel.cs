using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDeleteOptionsPanel : MonoBehaviour
{
    public GameObject deleteOptionsPanel; // Panel z opcj� usuni�cia wie�y na poziomie 3
    private Tower selectedTower; // Aktualnie wybrana wie�a

    // Otwieranie panelu opcji dla wie�y poziomu 3
    public void Open(Tower tower)
    {
        if (selectedTower == tower && deleteOptionsPanel.activeSelf)
        {
            Close(); // Je�li klikni�to na t� sam� wie��, zamknij panel
            return;
        }

        selectedTower = tower;
        deleteOptionsPanel.SetActive(true);

        // Ustawienie pozycji panelu nad klikni�t� wie��
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(tower.transform.position);
        screenPosition.y += 98; // Dostosowanie wysoko�ci panelu

        // Pobierz rozmiary panelu
        RectTransform panelDeleteRectTransform = deleteOptionsPanel.GetComponent<RectTransform>();
        float panelHeight = panelDeleteRectTransform.rect.height;
        float panelWidth = panelDeleteRectTransform.rect.width;

        // Sprawdzenie, czy panel wychodzi poza g�rn� kraw�d� ekranu
        if (screenPosition.y + panelHeight > Screen.height)
        {
            // Je�li wychodzi poza g�rn� kraw�d�, ustaw pozycj� pod wie��
            screenPosition.y = Camera.main.WorldToScreenPoint(tower.transform.position).y - (panelHeight * 0.6f);
        }

        // Ograniczenie pozycji panelu do kraw�dzi ekranu
        screenPosition.x = Mathf.Clamp(screenPosition.x, panelWidth / 2, Screen.width - panelWidth / 2);

        // Ustaw finaln� pozycj� panelu
        deleteOptionsPanel.transform.position = screenPosition;
    }

    // Zamkni�cie panelu opcji
    public void Close()
    {
        deleteOptionsPanel.SetActive(false);
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
}
