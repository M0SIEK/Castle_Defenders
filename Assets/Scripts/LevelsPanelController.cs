using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsPanelController : MonoBehaviour
{
    public GameObject MainMenu;       // Referencja do g��wnego menu
    public GameObject LevelsPanel;    // Referencja do panelu wyboru poziom�w
    public CanvasGroup MainMenuCanvasGroup; // CanvasGroup g��wnego menu (do blokowania interakcji)

    public void StartLevel1()
    {
        SceneManager.LoadScene("Level 1"); // Za�aduj scen� Level1Scene
    }

    public void StartLevel2()
    {
        SceneManager.LoadScene("Level 2"); // Za�aduj scen� Level2Scene
    }

    public void StartLevel3()
    {
        SceneManager.LoadScene("Level3Scene"); // Za�aduj scen� Level3Scene
    }

    public void StartLevel4()
    {
        SceneManager.LoadScene("Level4Scene"); // Za�aduj scen� Level4Scene
    }

    public void StartLevel5()
    {
        SceneManager.LoadScene("Level5Scene"); // Za�aduj scen� Level5Scene
    }

    public void ReturnToMainMenu()
    {
        LevelsPanel.SetActive(false);              // Ukryj panel poziom�w
        MainMenuCanvasGroup.interactable = true;   // Odblokuj interakcje z g��wnym menu
        MainMenuCanvasGroup.blocksRaycasts = true; // Przywr�� rejestrowanie klikni��
    }
}
