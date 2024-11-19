using UnityEngine;

public class FreezeAbility : MonoBehaviour
{
    public GameObject freezeAreaPrefab; // Prefab obszaru dzia�ania
    public float freezeDuration = 5f;  // Czas trwania dzia�ania umiej�tno�ci (opcjonalnie do logiki w przysz�o�ci)

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
        // Sprawdzenie, czy tryb umieszczania nie jest ju� aktywny
        if (!isPlacingFreezeArea)
        {
            // Utworzenie przezroczystego obiektu pod kursorem
            freezeAreaInstance = Instantiate(freezeAreaPrefab);
            freezeAreaInstance.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f); // Ustawienie przezroczysto�ci
            isPlacingFreezeArea = true;
        }
    }

    private void HandleFreezePlacement()
    {
        // Przesuwanie obiektu pod kursorem
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Aby obiekt by� w p�aszczy�nie 2D
        freezeAreaInstance.transform.position = mousePosition;

        if (Input.GetMouseButtonDown(0)) // Lewy przycisk myszy: umieszczenie obiektu
        {
            Debug.Log("Freeze area placed!");
            isPlacingFreezeArea = false;
            freezeAreaInstance.GetComponent<SpriteRenderer>().color = Color.white; // Ustawienie pe�nej widoczno�ci
            // Mo�esz doda� tutaj inne akcje, np. aktywowanie logiki dzia�ania obszaru
        }

        if (Input.GetMouseButtonDown(1)) // Prawy przycisk myszy: anulowanie
        {
            Debug.Log("Freeze area placement canceled.");
            isPlacingFreezeArea = false;
            Destroy(freezeAreaInstance);
        }
    }
}
