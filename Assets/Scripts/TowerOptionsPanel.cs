using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerOptionsPanel : MonoBehaviour
{
    public GameObject optionsPanel; // Panel z przyciskami "Upgrade" i "Delete"
    private Tower selectedTower; // Aktualnie wybrana wieża
    private WavesController wavesController; // Referencja do WavesController

    public float focusedHeightOffset = 100f; // Wysokość panelu w trybie Play Focused
    public float maximizedHeightOffset = 150f; // Wysokość panelu w trybie Play Maximized

    void Start()
    {
        // Pobranie referencji do WavesController
        wavesController = GameObject.FindGameObjectWithTag("WavesController").GetComponent<WavesController>();
    }

    // Otwieranie panelu opcji dla wybranej wieży
    public void Open(Tower tower)
    {
        if (selectedTower == tower && optionsPanel.activeSelf)
        {
            Close(); // Jeśli kliknięto na tę samą wieżę, zamknij panel
            return;
        }

        selectedTower = tower;
        optionsPanel.SetActive(true);

        // Ustal wysokość offsetu na podstawie trybu wyświetlania
        float heightOffset = (Screen.width > 1000 && Screen.height > 600) ? maximizedHeightOffset : focusedHeightOffset;

        // Ustawienie pozycji panelu nad klikniętą wieżą
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(tower.transform.position);
        screenPosition.y += heightOffset; // Dostosowanie wysokości panelu

        // Pobierz rozmiary panelu
        RectTransform optionsRectTransform = optionsPanel.GetComponent<RectTransform>();
        float panelHeight = optionsRectTransform.rect.height;
        float panelWidth = optionsRectTransform.rect.width;

        // Sprawdzenie, czy panel wychodzi poza górną krawędź ekranu
        if (screenPosition.y + panelHeight > Screen.height)
        {
            // Jeśli wychodzi poza górną krawędź, ustaw pozycję pod wieżą
            screenPosition.y = Camera.main.WorldToScreenPoint(tower.transform.position).y - (panelHeight + 7f);
        }

        // Ograniczenie pozycji panelu do krawędzi ekranu
        screenPosition.x = Mathf.Clamp(screenPosition.x, panelWidth / 2, Screen.width - panelWidth / 2);

        // Ustaw finalną pozycję panelu
        optionsPanel.transform.position = screenPosition;
    }

    // Zamknięcie panelu opcji
    public void Close()
    {
        optionsPanel.SetActive(false);
        selectedTower = null;
    }

    // Funkcja wywoływana przez przycisk "Delete"
    public void DeleteTower()
    {
        if (selectedTower != null)
        {
            // Dodaj stałą wartość 25 złota przy usuwaniu
            wavesController.gold += 25; // Dodaj 25 złota do WavesController
            wavesController.UpdateGoldCounter(); // Zaktualizuj złoto w interfejsie użytkownika

            Debug.Log("Wieża usunięta! Dodano 25 złota.");

            selectedTower.DeleteTower(); // Usuń wieżę
        }
    }

    // Funkcja wywoływana przez przycisk "Upgrade"
    public void UpgradeTower()
    {
        if (selectedTower != null)
        {
            // Sprawdzenie, czy gracz ma wystarczająco złota na ulepszenie wieży
            if (wavesController.gold >= selectedTower.upgradeCost * 2)
            {
                wavesController.gold -= selectedTower.upgradeCost; // Odejmij koszt ulepszenia
                wavesController.UpdateGoldCounter(); // Zaktualizuj licznik złota
                selectedTower.UpgradeTower(); // Ulepsz wieżę
            }
            else
            {
                Debug.Log("Nie masz wystarczająco złota, aby ulepszyć tę wieżę!");
            }
        }
    }
}