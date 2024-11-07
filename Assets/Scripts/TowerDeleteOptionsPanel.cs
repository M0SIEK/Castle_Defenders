using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDeleteOptionsPanel : MonoBehaviour
{
    public GameObject deleteOptionsPanel; // Panel z opcj¹ usuniêcia wie¿y na poziomie 3
    private Tower selectedTower; // Aktualnie wybrana wie¿a

    // Otwieranie panelu opcji dla wie¿y poziomu 3
    public void Open(Tower tower)
    {
        if (selectedTower == tower && deleteOptionsPanel.activeSelf)
        {
            Close(); // Jeœli klikniêto na tê sam¹ wie¿ê, zamknij panel
            return;
        }

        selectedTower = tower;
        deleteOptionsPanel.SetActive(true);

        // Ustawienie pozycji panelu nad klikniêt¹ wie¿¹
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(tower.transform.position);
        screenPosition.y += 98; // Dostosowanie wysokoœci panelu

        // Pobierz rozmiary panelu
        RectTransform panelDeleteRectTransform = deleteOptionsPanel.GetComponent<RectTransform>();
        float panelHeight = panelDeleteRectTransform.rect.height;
        float panelWidth = panelDeleteRectTransform.rect.width;

        // Sprawdzenie, czy panel wychodzi poza górn¹ krawêdŸ ekranu
        if (screenPosition.y + panelHeight > Screen.height)
        {
            // Jeœli wychodzi poza górn¹ krawêdŸ, ustaw pozycjê pod wie¿¹
            screenPosition.y = Camera.main.WorldToScreenPoint(tower.transform.position).y - (panelHeight * 0.6f);
        }

        // Ograniczenie pozycji panelu do krawêdzi ekranu
        screenPosition.x = Mathf.Clamp(screenPosition.x, panelWidth / 2, Screen.width - panelWidth / 2);

        // Ustaw finaln¹ pozycjê panelu
        deleteOptionsPanel.transform.position = screenPosition;
    }

    // Zamkniêcie panelu opcji
    public void Close()
    {
        deleteOptionsPanel.SetActive(false);
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
}
