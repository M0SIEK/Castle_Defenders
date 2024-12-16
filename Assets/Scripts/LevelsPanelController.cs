using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsPanelController : MonoBehaviour
{
    public GameObject MainMenu;       // Referencja do g³ównego menu
    public GameObject LevelsPanel;    // Referencja do panelu wyboru poziomów
    public CanvasGroup MainMenuCanvasGroup; // CanvasGroup g³ównego menu (do blokowania interakcji)

    public void StartLevel1()
    {
        SceneManager.LoadScene("Level 1"); // Za³aduj scenê Level1Scene
    }

    public void StartLevel2()
    {
        SceneManager.LoadScene("Level 2"); // Za³aduj scenê Level2Scene
    }

    public void StartLevel3()
    {
        SceneManager.LoadScene("Level3Scene"); // Za³aduj scenê Level3Scene
    }

    public void StartLevel4()
    {
        SceneManager.LoadScene("Level4Scene"); // Za³aduj scenê Level4Scene
    }

    public void StartLevel5()
    {
        SceneManager.LoadScene("Level5Scene"); // Za³aduj scenê Level5Scene
    }

    public void ReturnToMainMenu()
    {
        LevelsPanel.SetActive(false);              // Ukryj panel poziomów
        MainMenuCanvasGroup.interactable = true;   // Odblokuj interakcje z g³ównym menu
        MainMenuCanvasGroup.blocksRaycasts = true; // Przywróæ rejestrowanie klikniêæ
    }
}
