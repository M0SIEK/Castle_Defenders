using UnityEngine;

public class Ability : MonoBehaviour
{
    public GameObject abilityAreaPrefab; // Prefab obszaru dzia³ania umiejêtnoœci
    public float abilityDuration = 5f;  // Czas trwania dzia³ania umiejêtnoœci
    public int gemsCost = 1; // Koszt u¿ycia umiejêtnoœci w gemach

    private GameObject abilityAreaInstance; // Tymczasowy obiekt podczas umieszczania
    private bool isPlacingAbilityArea = false; // Czy tryb umieszczania jest aktywny
    private WavesController wavesController; // Referencja do WavesController

    void Start()
    {
        // ZnajdŸ WavesController na scenie
        wavesController = FindObjectOfType<WavesController>();

        if (wavesController == null)
        {
            Debug.LogError("WavesController not found! Make sure it exists in the scene.");
        }
    }

    void Update()
    {
        if (isPlacingAbilityArea)
        {
            Debug.Log("Placing ability area..."); // Debug podczas aktywnego trybu umieszczania
            HandleAbilityPlacement();
        }
    }

    public void ActivateAbility()
    {
        Debug.Log("ActivateAbility called!"); // Informacja, ¿e funkcja zosta³a wywo³ana

        // SprawdŸ, czy gracz ma wystarczaj¹co gemów
        if (wavesController != null && !wavesController.SpendGems(gemsCost))
        {
            Debug.LogWarning("Nie masz wystarczaj¹co gemów, aby u¿yæ umiejêtnoœci!");
            return;
        }

        // Sprawdzenie, czy tryb umieszczania nie jest ju¿ aktywny
        if (!isPlacingAbilityArea)
        {
            Debug.Log("Starting ability area placement."); // Rozpoczêcie umieszczania
            abilityAreaInstance = Instantiate(abilityAreaPrefab);
            abilityAreaInstance.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f); // Ustawienie przezroczystoœci
            Debug.Log($"Ability area prefab instantiated at: {abilityAreaInstance.transform.position}"); // Pozycja pocz¹tkowa
            isPlacingAbilityArea = true;
        }
        else
        {
            Debug.LogWarning("Ability placement is already active!"); // Ostrze¿enie, jeœli funkcja zosta³a wywo³ana ponownie
        }
    }

    private void HandleAbilityPlacement()
    {
        // Przesuwanie obiektu pod kursorem
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Aby obiekt by³ w p³aszczyŸnie 2D
        abilityAreaInstance.transform.position = mousePosition;

        Debug.Log($"Ability area position updated to: {mousePosition}"); // Debug aktualnej pozycji

        if (Input.GetMouseButtonDown(0)) // Lewy przycisk myszy: umieszczenie obiektu
        {
            Debug.Log("Ability area placed at: " + mousePosition); // Informacja o umieszczeniu obszaru
            isPlacingAbilityArea = false;
            abilityAreaInstance.GetComponent<SpriteRenderer>().color = Color.white; // Ustawienie pe³nej widocznoœci

            // Uruchom timer, aby usun¹æ obiekt po czasie dzia³ania
            StartCoroutine(RemoveAbilityAreaAfterDuration(abilityAreaInstance));
        }

        if (Input.GetMouseButtonDown(1)) // Prawy przycisk myszy: anulowanie
        {
            Debug.Log("Ability area placement canceled at: " + abilityAreaInstance.transform.position); // Informacja o anulowaniu
            isPlacingAbilityArea = false;
            Destroy(abilityAreaInstance);

            // Dodanie 1 gema przy anulowaniu umiejêtnoœci
            if (wavesController != null)
            {
                wavesController.gems += 1; // Zwiêksz liczbê gemów
                wavesController.UpdateGemsCounter(); // Aktualizuj licznik w UI
                Debug.Log("1 gem added for canceling the ability.");
            }
        }
    }

    private System.Collections.IEnumerator RemoveAbilityAreaAfterDuration(GameObject abilityArea)
    {
        Debug.Log($"Ability area will be removed after {abilityDuration} seconds.");
        yield return new WaitForSeconds(abilityDuration); // Czekaj przez okreœlony czas
        Debug.Log("Removing ability area...");
        Destroy(abilityArea); // Usuñ obiekt
    }
}
