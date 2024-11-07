using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectionMenu : MonoBehaviour
{
    public GameObject menuPanel; // Panel menu wyboru
    private EmptyField selectedField; // Wybrane pole, na którym postawimy wie¿ê

    // Prefabrykaty wie¿
    public GameObject tower1Prefab;
    public GameObject tower2Prefab;
    public GameObject tower3Prefab;

    public float focusedHeightOffset = 60f; // Wysokoœæ panelu w trybie Play Focused
    public float maximizedHeightOffset = 90f; // Wysokoœæ panelu w trybie Play Maximized

    public void Open_Close(EmptyField field)
    {
        if (menuPanel.activeSelf)
        {
            Close(); // Jeœli panel jest otwarty, zamknij go
        }
        else
        {
            Open(field); // Jeœli panel jest zamkniêty, otwórz go
        }
    }

    public void Open(EmptyField field)
    {
        selectedField = field;
        menuPanel.SetActive(true);

        // Ustal wysokoœæ offsetu na podstawie trybu wyœwietlania
        float heightOffset = (Screen.width > 1000 && Screen.height > 600) ? maximizedHeightOffset : focusedHeightOffset;

        // Ustawienie pozycji panelu nad klikniêtym polem w przestrzeni ekranu
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(field.transform.position);
        screenPosition.y += heightOffset; // Dostosowanie wysokoœci, aby panel by³ nad polem

        RectTransform menuRectTransform = menuPanel.GetComponent<RectTransform>();
        float menuHeight = menuRectTransform.rect.height;
        float menuWidth = menuRectTransform.rect.width;

        // Sprawdzenie, czy panel wychodzi poza górn¹ krawêdŸ ekranu
        if (screenPosition.y + menuHeight > Screen.height)
        {
            // Jeœli wychodzi poza górn¹ krawêdŸ, ustaw pozycjê pod polem
            screenPosition.y = Camera.main.WorldToScreenPoint(field.transform.position).y - (menuHeight + 5);
        }

        // Ograniczenie pozycji panelu do krawêdzi ekranu
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

        // Tworzenie wie¿y na wybranym polu
        if (selectedTower != null && selectedField != null)
        {
            Vector3 towerPosition = selectedField.transform.position;
            towerPosition.y -= 0.5f; // Ustawienie odpowiedniej wysokoœci
            GameObject towerInstance = Instantiate(selectedTower, towerPosition, Quaternion.identity);

            // Przypisanie pola do nowo utworzonej wie¿y
            Tower towerScript = towerInstance.GetComponent<Tower>();
            if (towerScript != null)
            {
                towerScript.SetOriginalField(selectedField);
            }

            selectedField.gameObject.SetActive(false); // Ukryj pole po postawieniu wie¿y
            Close(); // Zamknij panel po wybraniu wie¿y
        }
    }
}
