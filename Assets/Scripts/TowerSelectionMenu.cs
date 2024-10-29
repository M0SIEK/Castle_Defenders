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

    public void Open(EmptyField field)
    {
        selectedField = field;
        menuPanel.SetActive(true);

        // Ustawienie pozycji panelu nad klikniêtym polem w przestrzeni ekranu
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(field.transform.position);
        screenPosition.y += 60; // Dostosuj wysokoœæ, aby panel by³ nad polem w pikselach
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

        Debug.Log("Wybrano wie¿ê o indeksie: " + towerIndex);
        Debug.Log("Wybrany prefabrykat wie¿y: " + (selectedTower != null ? selectedTower.name : "brak"));

        // Tworzenie wie¿y na wybranym polu
        if (selectedTower != null && selectedField != null)
        {
            Instantiate(selectedTower, selectedField.transform.position, Quaternion.identity);
            selectedField.gameObject.SetActive(false); // Ukryj pole po postawieniu wie¿y
            Debug.Log("Wie¿a zosta³a postawiona na polu: " + selectedField.name);
            Close(); // Zamknij panel po wybraniu wie¿y
        }
        else
        {
            Debug.LogWarning("Nie uda³o siê postawiæ wie¿y. Brak wybranej wie¿y lub pola.");
        }
    }
}

