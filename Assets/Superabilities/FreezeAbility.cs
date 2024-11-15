using UnityEngine;

public class FreezeAbility : MonoBehaviour
{
    public Texture2D cursorImage; // Obraz kursora
    public GameObject freezeEffectPrefab; // Prefab efektu Freeze

    private bool isActive = false;

    void Update()
    {
        if (isActive)
        {
            // Debug: potwierdzenie aktywno�ci
            Debug.Log("Freeze ability is active.");

            // Ustawienie kursora na pozycj� myszy
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0; // Reset osi Z dla 2D

                Debug.Log("Mouse clicked at: " + mousePosition);

                // Tworzenie efektu w miejscu klikni�cia
                if (freezeEffectPrefab != null)
                {
                    Instantiate(freezeEffectPrefab, mousePosition, Quaternion.identity);
                    Debug.Log("Freeze effect instantiated.");
                }
                else
                {
                    Debug.LogError("FreezeEffectPrefab is not assigned!");
                }

                // Wy��czenie umiej�tno�ci
                DeactivateAbility();
            }
        }
    }

    public void ActivateAbility()
    {
        if (cursorImage != null)
        {
            Cursor.SetCursor(cursorImage, new Vector2(cursorImage.width / 2, cursorImage.height / 2), CursorMode.Auto);
            isActive = true;
            Debug.Log("Freeze ability activated.");
        }
        else
        {
            Debug.LogError("Cursor image is not assigned!");
        }
    }

    public void DeactivateAbility()
    {
        isActive = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Debug.Log("Freeze ability deactivated.");
    }
}
