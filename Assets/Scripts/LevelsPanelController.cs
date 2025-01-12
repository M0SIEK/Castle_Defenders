using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsPanelController : MonoBehaviour
{
    public GameObject MainMenu;       // Referencja do g³ównego menu
    public GameObject LevelsPanel;    // Referencja do panelu wyboru poziomów
    public CanvasGroup MainMenuCanvasGroup; // CanvasGroup g³ównego menu (do blokowania interakcji)

    public void StartLevel1()
    {
        StartLevel("Level 1");
    }

    public void StartLevel2()
    {
        StartLevel("Level 2");
    }

    public void StartLevel3()
    {
        StartLevel("Level 3");
    }

    public void StartLevel4()
    {
        StartLevel("Level 4");
    }

    public void StartLevel5()
    {
        StartLevel("Level 5");
    }

    private void StartLevel(string levelName)
    {
        SettingsPanelController.SettingsPanelActive = false;

        // Zapisz nazwê poziomu do PlayerPrefs
        PlayerPrefs.SetString("CurrentLevelName", levelName);

        // Za³aduj wybrany poziom
        SceneManager.LoadScene(levelName);
    }

    public void ReturnToMainMenu()
    {
        LevelsPanel.SetActive(false);              // Ukryj panel poziomów
        MainMenuCanvasGroup.interactable = true;   // Odblokuj interakcje z g³ównym menu
        MainMenuCanvasGroup.blocksRaycasts = true; // Przywróæ rejestrowanie klikniêæ
    }
}
