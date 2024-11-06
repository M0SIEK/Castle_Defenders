using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerOptionsPanel towerOptionsPanel; // Panel opcji wie�y
    public TowerDeleteOptionsPanel towerDeleteOptionsPanel; // Panel usuwania wie�y
    private EmptyField originalField; // Pole, na kt�rym wie�a zosta�a postawiona
    private bool isUpgraded = false; // Flaga do sprawdzania, czy wie�a zosta�a ulepszona
    private MonoBehaviour activeOptionsPanel; // Przechowuje aktywny panel (TowerOptionsPanel lub TowerDeleteOptionsPanel)
    public int level = 1; // Zmienna reprezentuj�ca poziom wie�y, domy�lnie ustawiona na 1
    public GameObject upgradedTowerPrefab; // Prefab ulepszonej wie�y (do przypisania w Unity)

    private void OnMouseDown()
    {
        // W zale�no�ci od poziomu wie�y (isUpgraded) otwieramy odpowiedni panel
        if (level == 3)
        {
            activeOptionsPanel = towerDeleteOptionsPanel;
        }
        else if (level == 1 || level == 2)
        {
            activeOptionsPanel = towerOptionsPanel;
        }

        // Otwieramy odpowiedni panel
        if (activeOptionsPanel != null)
        {
            if (activeOptionsPanel is TowerOptionsPanel optionsPanel)
            {
                optionsPanel.Open(this);
            }
            else if (activeOptionsPanel is TowerDeleteOptionsPanel deletePanel)
            {
                deletePanel.Open(this);
            }
        }
    }

    // Przypisanie pola, na kt�rym wie�a zosta�a postawiona
    public void SetOriginalField(EmptyField field)
    {
        originalField = field;
    }

    // Funkcja usuwaj�ca wie��
    public void DeleteTower()
    {
        // Aktywujemy ponownie pole, na kt�rym wie�a zosta�a postawiona
        if (originalField != null)
        {
            originalField.gameObject.SetActive(true); // Pole staje si� aktywne
        }

        Destroy(gameObject); // Zniszcz wie��
        if (activeOptionsPanel != null)
        {
            activeOptionsPanel.GetType().GetMethod("Close")?.Invoke(activeOptionsPanel, null); // Zamknij odpowiedni panel
        }
    }

    // Funkcja do ulepszania wie�y
    public void UpgradeTower()
    {
        // Tworzymy ulepszon� wersj� wie�y na tej samej pozycji
        if (upgradedTowerPrefab != null)
        {
            GameObject upgradedTower = Instantiate(upgradedTowerPrefab, transform.position, transform.rotation);

            // Przypisujemy pole do nowej wie�y i ustawiamy j� jako ulepszon�
            Tower towerScript = upgradedTower.GetComponent<Tower>();
            if (towerScript != null)
            {
                towerScript.SetOriginalField(originalField);
                towerScript.level = level + 1; // Zwi�kszamy poziom wie�y
            }

            Destroy(gameObject); // Zniszcz stary obiekt wie�y
            if (towerOptionsPanel != null)
            {
                towerOptionsPanel.Close(); // Zamknij panel opcji
            }
        }
    }
}
