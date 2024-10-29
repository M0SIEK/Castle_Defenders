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

        // Ustawienie pozycji panelu nad klikniêtym polem w przestrzeni ekranu
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(field.transform.position);
        screenPosition.y += 60; // Dostosowanie wysokoœci, aby panel by³ nad polem

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
            // Ustawienie wie¿y na pole z odpowiedni¹ wysokoœci¹
            Vector3 towerPosition = selectedField.transform.position;
            towerPosition.y -= 0.5f; // Ustawienie odpowiedniej wysokoœci
            Instantiate(selectedTower, towerPosition, Quaternion.identity);

            selectedField.gameObject.SetActive(false); // Ukryj pole po postawieniu wie¿y
            Close(); // Zamknij panel po wybraniu wie¿y
        }
    }
}
