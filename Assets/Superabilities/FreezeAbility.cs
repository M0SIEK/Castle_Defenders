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
            HandleFreezePlacement();
        }
    }

    public void ActivateFreezeAbility()
    {
        // Sprawdzenie, czy tryb umieszczania nie jest ju¿ aktywny
        if (!isPlacingFreezeArea)
        {
            // Utworzenie przezroczystego obiektu pod kursorem
            freezeAreaInstance = Instantiate(freezeAreaPrefab);
            freezeAreaInstance.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f); // Ustawienie przezroczystoœci
            isPlacingFreezeArea = true;
        }
    }

    private void HandleFreezePlacement()
    {
        // Przesuwanie obiektu pod kursorem
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Aby obiekt by³ w p³aszczyŸnie 2D
        freezeAreaInstance.transform.position = mousePosition;

        if (Input.GetMouseButtonDown(0)) // Lewy przycisk myszy: umieszczenie obiektu
        {
            Debug.Log("Freeze area placed!");
            isPlacingFreezeArea = false;
            freezeAreaInstance.GetComponent<SpriteRenderer>().color = Color.white; // Ustawienie pe³nej widocznoœci
            // Mo¿esz dodaæ tutaj inne akcje, np. aktywowanie logiki dzia³ania obszaru
        }

        if (Input.GetMouseButtonDown(1)) // Prawy przycisk myszy: anulowanie
        {
            Debug.Log("Freeze area placement canceled.");
            isPlacingFreezeArea = false;
            Destroy(freezeAreaInstance);
        }
    }
}
