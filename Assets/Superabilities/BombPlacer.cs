using UnityEngine;

public class BombPlacer : MonoBehaviour
{
    public GameObject bombPrefab; // Prefab bomby
    public int gemCost = 1; // Koszt postawienia bomby
    private WavesController wavesController; // Referencja do WavesController

    private void Start()
    {
        // Znajdź WavesController w scenie
        wavesController = FindObjectOfType<WavesController>();
    }

    // Metoda wywoływana przez przycisk On Click
    public void PlaceBomb()
    {
        // Sprawdź, czy gracz ma wystarczającą liczbę gemów
        if (wavesController != null)
        {
            if (wavesController.SpendGems(gemCost)) // Sprawdzenie, czy transakcja się powiodła
            {
                // Umieść bombę w podanym miejscu
                Vector3 position = GetBombPlacementPosition();
                Instantiate(bombPrefab, position, Quaternion.identity);
                Debug.Log("Bomb placed successfully!");
            }
            else
            {
                // Wyświetl komunikat o braku gemów
                Debug.Log("Not enough gems to place the bomb!");
            }
        }
    }

    // Logika ustawiania pozycji bomby
    private Vector3 GetBombPlacementPosition()
    {
        // Ustawienie bomby na pozycji wskazanej przez mysz
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0; // Ponieważ to gra 2D, ustaw Z na 0
        return position;
    }
}