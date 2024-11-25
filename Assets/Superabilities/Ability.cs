using UnityEngine;

public class Ability : MonoBehaviour
{
    public GameObject abilityAreaPrefab; // Prefab obszaru dzia�ania umiej�tno�ci
    public float abilityDuration = 5f;  // Czas trwania dzia�ania umiej�tno�ci

    private GameObject abilityAreaInstance; // Tymczasowy obiekt podczas umieszczania
    private bool isPlacingAbilityArea = false; // Czy tryb umieszczania jest aktywny

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
        Debug.Log("ActivateAbility called!"); // Informacja, �e funkcja zosta�a wywo�ana

        // Sprawdzenie, czy tryb umieszczania nie jest ju� aktywny
        if (!isPlacingAbilityArea)
        {
            Debug.Log("Starting ability area placement."); // Rozpocz�cie umieszczania
            abilityAreaInstance = Instantiate(abilityAreaPrefab);
            abilityAreaInstance.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f); // Ustawienie przezroczysto�ci
            Debug.Log($"Ability area prefab instantiated at: {abilityAreaInstance.transform.position}"); // Pozycja pocz�tkowa
            isPlacingAbilityArea = true;
        }
        else
        {
            Debug.LogWarning("Ability placement is already active!"); // Ostrze�enie, je�li funkcja zosta�a wywo�ana ponownie
        }
    }

    private void HandleAbilityPlacement()
    {
        // Przesuwanie obiektu pod kursorem
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Aby obiekt by� w p�aszczy�nie 2D
        abilityAreaInstance.transform.position = mousePosition;

        Debug.Log($"Ability area position updated to: {mousePosition}"); // Debug aktualnej pozycji

        if (Input.GetMouseButtonDown(0)) // Lewy przycisk myszy: umieszczenie obiektu
        {
            Debug.Log("Ability area placed at: " + mousePosition); // Informacja o umieszczeniu obszaru
            isPlacingAbilityArea = false;
            abilityAreaInstance.GetComponent<SpriteRenderer>().color = Color.white; // Ustawienie pe�nej widoczno�ci

            // Uruchom timer, aby usun�� obiekt po czasie dzia�ania
            StartCoroutine(RemoveAbilityAreaAfterDuration(abilityAreaInstance));
        }

        if (Input.GetMouseButtonDown(1)) // Prawy przycisk myszy: anulowanie
        {
            Debug.Log("Ability area placement canceled at: " + abilityAreaInstance.transform.position); // Informacja o anulowaniu
            isPlacingAbilityArea = false;
            Destroy(abilityAreaInstance);
        }
    }

    private System.Collections.IEnumerator RemoveAbilityAreaAfterDuration(GameObject abilityArea)
    {
        Debug.Log($"Ability area will be removed after {abilityDuration} seconds.");
        yield return new WaitForSeconds(abilityDuration); // Czekaj przez okre�lony czas
        Debug.Log("Removing ability area...");
        Destroy(abilityArea); // Usu� obiekt
    }
}
