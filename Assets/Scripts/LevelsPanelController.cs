using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsPanelController : MonoBehaviour
{
    public GameObject MainMenu;       // Referencja do g��wnego menu
    public GameObject LevelsPanel;    // Referencja do panelu wyboru poziom�w
    public CanvasGroup MainMenuCanvasGroup; // CanvasGroup g��wnego menu (do blokowania interakcji)

    public void ReturnToMainMenu()
    {
        LevelsPanel.SetActive(false);              // Ukryj panel poziom�w
        MainMenuCanvasGroup.interactable = true;   // Odblokuj interakcje z g��wnym menu
        MainMenuCanvasGroup.blocksRaycasts = true; // Przywr�� rejestrowanie klikni��
    }
}
