using UnityEngine;

public class FreezeAbility : MonoBehaviour
{
    public GameObject freezeAreaPrefab; // Prefab obszaru dzia³ania
    public float freezeDuration = 5f;  // Czas trwania dzia³ania umiejêtnoœci (opcjonalnie do logiki w przysz³oœci)

    private GameObject freezeAreaInstance; // Tymczasowy obiekt podczas umieszczania
    private bool isPlacingFreezeArea = false; // Czy tryb umieszczania jest aktywny

    void Update()
    {
        if (isPlacingFreezeArea)
        {
            Debug.Log("Placing freeze area..."); // Debug podczas aktywnego trybu umieszczania
            HandleFreezePlacement();
        }
    }

    public void ActivateFreezeAbility()
    {
        Debug.Log("ActivateFreezeAbility called!"); // Informacja, ¿e funkcja zosta³a wywo³ana

        // Sprawdzenie, czy tryb umieszczania nie jest ju¿ aktywny
        if (!isPlacingFreezeArea)
        {
            Debug.Log("Starting freeze area placement."); // Rozpoczêcie umieszczania
            freezeAreaInstance = Instantiate(freezeAreaPrefab);
            freezeAreaInstance.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f); // Ustawienie przezroczystoœci
            Debug.Log($"Freeze area prefab instantiated at: {freezeAreaInstance.transform.position}"); // Pozycja pocz¹tkowa
            isPlacingFreezeArea = true;
        }
        else
        {
            Debug.LogWarning("Freeze ability is already active!"); // Ostrze¿enie, jeœli funkcja zosta³a wywo³ana ponownie
        }
    }

    private void HandleFreezePlacement()
    {
        // Przesuwanie obiektu pod kursorem
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Aby obiekt by³ w p³aszczyŸnie 2D
        freezeAreaInstance.transform.position = mousePosition;

        Debug.Log($"Freeze area position updated to: {mousePosition}"); // Debug aktualnej pozycji

        if (Input.GetMouseButtonDown(0)) // Lewy przycisk myszy: umieszczenie obiektu
        {
            Debug.Log("Freeze area placed at: " + mousePosition); // Informacja o umieszczeniu obszaru
            isPlacingFreezeArea = false;
            freezeAreaInstance.GetComponent<SpriteRenderer>().color = Color.white; // Ustawienie pe³nej widocznoœci
            // Mo¿esz dodaæ tutaj inne akcje, np. aktywowanie logiki dzia³ania obszaru
        }

        if (Input.GetMouseButtonDown(1)) // Prawy przycisk myszy: anulowanie
        {
            Debug.Log("Freeze area placement canceled at: " + freezeAreaInstance.transform.position); // Informacja o anulowaniu
            isPlacingFreezeArea = false;
            Destroy(freezeAreaInstance);
        }
    }
}

