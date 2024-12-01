using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider2D))]
public class WallEditor : MonoBehaviour
{
    public float gridSize = 1f;       // Rozmiar siatki
    public float snapDistance = 0.1f; // Maksymalna odleg�o�� do po��czenia z inn� �cian�

    private BoxCollider2D wallCollider;

    private void Awake()
    {
        // Upewnij si�, �e obiekt ma BoxCollider2D
        wallCollider = GetComponent<BoxCollider2D>();
        if (!wallCollider)
        {
            wallCollider = gameObject.AddComponent<BoxCollider2D>();
        }
    }

    private void OnDrawGizmos()
    {
        // Rysowanie siatki w celu wizualizacji
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize, gridSize, 0));
    }

    public void SnapToGrid()
    {
        // Upewnij si�, �e gridSize jest poprawny
        if (gridSize <= 0)
        {
            Debug.LogError("gridSize musi by� wi�ksze od 0!");
            return;
        }

        // Obliczanie pozycji dopasowanej do siatki
        float x = Mathf.Round(transform.position.x / gridSize) * gridSize;
        float y = Mathf.Round(transform.position.y / gridSize) * gridSize;

        // Sprawdzanie poprawno�ci oblicze�
        if (float.IsNaN(x) || float.IsNaN(y))
        {
            Debug.LogError("Obliczona pozycja jest nieprawid�owa (NaN). Sprawd� pozycj� obiektu i gridSize.");
            return;
        }

        // Przypisanie pozycji do transform
        transform.position = new Vector3(x, y, transform.position.z);
    }

    public void SnapToNearbyWalls()
    {
        // Przyci�ganie do pobliskich �cian
        WallEditor[] allWalls = FindObjectsOfType<WallEditor>();

        foreach (var wall in allWalls)
        {
            if (wall != this)
            {
                float distance = Vector3.Distance(transform.position, wall.transform.position);

                if (distance < snapDistance)
                {
                    // Sprawdzenie, czy kolizja b�dzie zachodzi�
                    if (!CheckCollisionWithWall(wall))
                    {
                        // Przypisanie pozycji najbli�szej �ciany
                        transform.position = wall.transform.position;
                    }
                    break;
                }
            }
        }
    }

    private bool CheckCollisionWithWall(WallEditor otherWall)
    {
        // Sprawdzenie potencjalnej kolizji
        Bounds thisBounds = wallCollider.bounds;
        Bounds otherBounds = otherWall.wallCollider.bounds;

        return thisBounds.Intersects(otherBounds);
    }

    private void Update()
    {
        // Funkcje aktywne tylko w trybie edytora
        if (!Application.isPlaying)
        {
            // Zabezpieczenie przed b��dami pozycji
            if (float.IsNaN(transform.position.x) || float.IsNaN(transform.position.y))
            {
                Debug.LogError("Pozycja obiektu jest nieprawid�owa (NaN). Resetowanie do zera.");
                transform.position = Vector3.zero;
            }

            // Dopasowanie do siatki i przyci�ganie do �cian
            SnapToGrid();
            SnapToNearbyWalls();
        }
    }
}
