using UnityEngine;

public class WallController : MonoBehaviour
{
    public Texture2D[] textures; // Tablica tekstur do przypisania w Inspectorze
    private SpriteRenderer spriteRenderer; // Komponent SpriteRenderer

    void Start()
    {
        // Pobierz komponent SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Je�li komponent SpriteRenderer jest obecny i mamy tekstury
        if (spriteRenderer != null && textures.Length > 0)
        {
            // Przypisz pierwsz� tekstur� z tablicy
            ChangeTexture(0);
        }
        else
        {
            Debug.LogError("Brak komponentu SpriteRenderer lub brak tekstur!");
        }
    }

    // Funkcja do zmiany tekstury na wybran�
    public void ChangeTexture(int index)
    {
        if (index >= 0 && index < textures.Length)
        {
            // Tworzymy Sprite z wybranej tekstury
            Sprite newSprite = Sprite.Create(textures[index], new Rect(0, 0, textures[index].width, textures[index].height), new Vector2(0.5f, 0.5f));

            // Dopasowujemy tekstur� do rozmiaru sprite'a
            AdjustTextureScale(newSprite);

            // Ustawiamy nowy sprite
            spriteRenderer.sprite = newSprite;
            Debug.Log("Tekstura zmieniona na: " + textures[index].name);
        }
        else
        {
            Debug.LogError("Niepoprawny indeks tekstury!");
        }
    }

    // Funkcja dopasowuj�ca tekstur� do rozmiaru sprite'a
    void AdjustTextureScale(Sprite sprite)
    {
        // Pobieramy rozmiar sprite'a
        Vector2 spriteSize = sprite.bounds.size;

        // Pobieramy wymiary tekstury
        float textureWidth = sprite.texture.width;
        float textureHeight = sprite.texture.height;

        // Obliczamy skalowanie tekstury na podstawie rozmiaru sprite'a
        float scaleX = spriteSize.x / textureWidth;
        float scaleY = spriteSize.y / textureHeight;

        // Ustawiamy tryb rysowania na "Simple", �eby tekstura by�a widoczna tylko w obr�bie sprite'a
        spriteRenderer.drawMode = SpriteDrawMode.Simple;
        spriteRenderer.size = spriteSize; // Ustawiamy rozmiar sprite'a na jego rzeczywisty rozmiar

        // Skalujemy tekstur� w SpriteRenderer
        spriteRenderer.material.mainTextureScale = new Vector2(scaleX, scaleY);
    }
}
