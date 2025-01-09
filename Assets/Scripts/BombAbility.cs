using UnityEngine;

public class BombAbility : MonoBehaviour
{
    public GameObject bombPrefab; // Prefab bomby
    public int gemsCost = 3; // Koszt użycia bomby

    // Pole do bezpośredniego odwołania do WavesController
    private WavesController wavesController;

    private void Start()
    {
        // Znajdź WavesController na scenie
        wavesController = FindObjectOfType<WavesController>();

        if (wavesController == null)
        {
            Debug.LogError("WavesController nie został znaleziony na scenie!");
        }
    }

    public void UseAbility(Vector3 position)
    {
        if (wavesController != null)
        {
            if (wavesController.gems >= gemsCost)
            {
                // Odejmij koszty Gems
                wavesController.gems -= gemsCost;
                wavesController.UpdateGemsCounter();

                // Utwórz bombę w podanej pozycji
                Instantiate(bombPrefab, position, Quaternion.identity);
                Debug.Log($"Bomba użyta! Pozostałe Gems: {wavesController.gems}");
            }
            else
            {
                Debug.Log("Nie masz wystarczającej ilości Gems, aby użyć bomby!");
            }
        }
        else
        {
            Debug.LogError("WavesController jest nullem, nie można użyć bomby.");
        }
    }
}