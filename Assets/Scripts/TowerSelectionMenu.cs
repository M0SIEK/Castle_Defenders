using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectionMenu : MonoBehaviour
{
    public GameObject menuPanel; // Panel menu wyboru
    private EmptyField selectedField; // Wybrane pole, na kt�rym postawimy wie��

    // Prefabrykaty wie�
    public GameObject tower1Prefab;
    public GameObject tower2Prefab;
    public GameObject tower3Prefab;

    public void Open_Close(EmptyField field)
    {
        if (menuPanel.activeSelf)
        {
            Close(); // Je�li panel jest otwarty, zamknij go
        }
        else
        {
            Open(field); // Je�li panel jest zamkni�ty, otw�rz go
        }
    }

    public void Open(EmptyField field)
    {
        selectedField = field;
        menuPanel.SetActive(true);

        // Ustawienie pozycji panelu nad klikni�tym polem w przestrzeni ekranu
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(field.transform.position);
        screenPosition.y += 60; // Dostosowanie wysoko�ci, aby panel by� nad polem

        RectTransform menuRectTransform = menuPanel.GetComponent<RectTransform>();
        float menuHeight = menuRectTransform.rect.height;
        float menuWidth = menuRectTransform.rect.width;

        // Sprawdzenie, czy panel wychodzi poza g�rn� kraw�d� ekranu
        if (screenPosition.y + menuHeight > Screen.height)
        {
            // Je�li wychodzi poza g�rn� kraw�d�, ustaw pozycj� pod polem
            screenPosition.y = Camera.main.WorldToScreenPoint(field.transform.position).y - (menuHeight + 5);
        }

        // Ograniczenie pozycji panelu do kraw�dzi ekranu
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

        // Wyb�r prefabrykatu na podstawie indeksu
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

        // Tworzenie wie�y na wybranym polu
        if (selectedTower != null && selectedField != null)
        {
            // Ustawienie wie�y na pole z odpowiedni� wysoko�ci�
            Vector3 towerPosition = selectedField.transform.position;
            towerPosition.y -= 0.5f; // Ustawienie odpowiedniej wysoko�ci
            Instantiate(selectedTower, towerPosition, Quaternion.identity);

            selectedField.gameObject.SetActive(false); // Ukryj pole po postawieniu wie�y
            Close(); // Zamknij panel po wybraniu wie�y
        }
    }
}
