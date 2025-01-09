using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectionMenu : MonoBehaviour
{
    public GameObject menuPanel; // Panel menu wyboru
    private EmptyField selectedField; // Wybrane pole, na którym postawimy wieżę
    private WavesController wavesController; // Referencja do WavesController

    // Prefabrykaty wież
    public GameObject tower1Prefab;
    public GameObject tower2Prefab;
    public GameObject tower3Prefab;

    public float focusedHeightOffset = 60f; // Wysokość panelu w trybie Play Focused
    public float maximizedHeightOffset = 180f; // Wysokość panelu w trybie Play Maximized

    void Start()
    {
        // Pobranie referencji do WavesController
        wavesController = GameObject.FindGameObjectWithTag("WavesController").GetComponent<WavesController>();
    }

    public void Open_Close(EmptyField field)
    {
        if (menuPanel.activeSelf)
        {
            Close(); // Jeśli panel jest otwarty, zamknij go
        }
        else
        {
            Open(field); // Jeśli panel jest zamknięty, otwórz go
        }
    }

    public void Open(EmptyField field)
    {
        selectedField = field;
        menuPanel.SetActive(true);

        // Ustal wysokość offsetu na podstawie trybu wyświetlania
        float heightOffset = (Screen.width > 1000 && Screen.height > 600) ? maximizedHeightOffset : focusedHeightOffset;

        // Ustawienie pozycji panelu nad klikniętym polem w przestrzeni ekranu
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(field.transform.position);
        screenPosition.y += heightOffset; // Dostosowanie wysokości, aby panel był nad polem

        RectTransform menuRectTransform = menuPanel.GetComponent<RectTransform>();
        float menuHeight = menuRectTransform.rect.height;
        float menuWidth = menuRectTransform.rect.width;

        // Sprawdzenie, czy panel wychodzi poza górną krawędź ekranu
        if (screenPosition.y + menuHeight > Screen.height)
        {
            // Jeśli wychodzi poza górną krawędź, ustaw pozycję pod polem
            screenPosition.y = Camera.main.WorldToScreenPoint(field.transform.position).y - (menuHeight + 30f);
        }

        // Ograniczenie pozycji panelu do krawędzi ekranu
        screenPosition.x = Mathf.Clamp(screenPosition.x, menuWidth / 2, Screen.width - menuWidth / 2);

        menuPanel.transform.position = screenPosition;
    }

    public void Close()
    {
        menuPanel.SetActive(false);
        selectedField = null;
    }

    public void SelectTower(int towerIndex)
    {
        GameObject selectedTower = null;

        // Wybór prefabrykatu na podstawie indeksu
        switch (towerIndex)
        {
            case 1:
                selectedTower = tower1Prefab;
                break;
            case 2:
                selectedTower = tower2Prefab;
                break;
            case 3:
                selectedTower = tower3Prefab;
                break;
        }

        if (selectedTower != null && selectedField != null)
        {
            Tower towerScript = selectedTower.GetComponent<Tower>();
            if (towerScript != null && wavesController.gold >= towerScript.buildCost)
            {
                wavesController.gold -= towerScript.buildCost; // Odejmij koszt budowy
                wavesController.UpdateGoldCounter(); // Zaktualizuj złoto w UI

                Vector3 towerPosition = selectedField.transform.position;
                towerPosition.y -= 0.5f; // Ustawienie odpowiedniej wysokości
                GameObject towerInstance = Instantiate(selectedTower, towerPosition, Quaternion.identity);

                // Przypisanie pola do nowo utworzonej wieży
                towerScript = towerInstance.GetComponent<Tower>();
                if (towerScript != null)
                {
                    towerScript.SetOriginalField(selectedField);
                }

                selectedField.gameObject.SetActive(false); // Ukryj pole po postawieniu wieży
                Close(); // Zamknij panel po wybraniu wieży
            }
            else
            {
                Debug.Log("Nie masz wystarczająco złota, aby zbudować tę wieżę!");
            }
        }
    }
}