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

        // Ustawienie pozycji panelu nad klikniêtym polem
        Vector3 newPosition = field.transform.position;
        newPosition.y += 1.5f; // Dostosuj wysokoœæ, aby panel by³ nad polem
        menuPanel.transform.position = newPosition; // Ustawienie pozycji
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
            Instantiate(selectedTower, selectedField.transform.position, Quaternion.identity);
            selectedField.gameObject.SetActive(false); // Ukryj pole po postawieniu wie¿y
            Close(); // Zamknij panel po wybraniu wie¿y
        }
    }
}
