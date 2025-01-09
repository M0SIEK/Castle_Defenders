using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDeleteOptionsPanel : MonoBehaviour
{
    public GameObject deleteOptionsPanel; // Panel z opcją usunięcia wieży na poziomie 3
    private Tower selectedTower; // Aktualnie wybrana wieża
    private WavesController wavesController; // Referencja do WavesController

    public float focusedHeightOffset = 100f; // Wysokość panelu w trybie Play Focused
    public float maximizedHeightOffset = 150f; // Wysokość panelu w trybie Play Maximized
    public float goldRefundPercentage = 0.5f; // Procent zwrotu złota po usunięciu wieży

    void Start()
    {
        // Pobranie referencji do WavesController
        wavesController = GameObject.FindGameObjectWithTag("WavesController").GetComponent<WavesController>();
    }

    // Otwieranie panelu opcji dla wieży poziomu 3
    public void Open(Tower tower)
    {
        if (selectedTower == tower && deleteOptionsPanel.activeSelf)
        {
            Close(); // Jeśli kliknięto na tę samą wieżę, zamknij panel
            return;
        }

        selectedTower = tower;
        deleteOptionsPanel.SetActive(true);

        // Ustal wysokość offsetu na podstawie trybu wyświetlania
        float heightOffset = (Screen.width > 1000 && Screen.height > 600) ? maximizedHeightOffset : focusedHeightOffset;

        // Ustawienie pozycji panelu nad klikniętą wieżą
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(tower.transform.position);
        screenPosition.y += heightOffset; // Dostosowanie wysokości panelu

        // Pobierz rozmiary panelu
        RectTransform panelDeleteRectTransform = deleteOptionsPanel.GetComponent<RectTransform>();
        float panelHeight = panelDeleteRectTransform.rect.height;
        float panelWidth = panelDeleteRectTransform.rect.width;

        // Sprawdzenie, czy panel wychodzi poza górną krawędź ekranu
        if (screenPosition.y + panelHeight > Screen.height)
        {
            // Jeśli wychodzi poza górną krawędź, ustaw pozycję pod wieżą
            screenPosition.y = Camera.main.WorldToScreenPoint(tower.transform.position).y - (panelHeight + 7f);
        }

        // Ograniczenie pozycji panelu do krawędzi ekranu
        screenPosition.x = Mathf.Clamp(screenPosition.x, panelWidth / 2, Screen.width - panelWidth / 2);

        // Ustaw finalną pozycję panelu
        deleteOptionsPanel.transform.position = screenPosition;
    }

    // Zamknięcie panelu opcji
    public void Close()
    {
        deleteOptionsPanel.SetActive(false);
        selectedTower = null;
    }

    // Funkcja wywoływana przez przycisk "Delete"
    public void DeleteTower()
    {
        if (selectedTower != null)
        {
            // Oblicz zwrot złota
            int goldRefund = Mathf.FloorToInt(selectedTower.buildCost * goldRefundPercentage);
            wavesController.gold += goldRefund; // Dodaj złoto do WavesController
            wavesController.UpdateGoldCounter(); // Zaktualizuj złoto w interfejsie użytkownika

            Debug.Log($"Wieża usunięta! Zwrot złota: {goldRefund}");

            selectedTower.DeleteTower(); // Usuń wieżę
        }
    }
}