using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuController : MonoBehaviour
{
    public GameObject MainMenu;       // G��wne menu
    public GameObject LevelsPanel;   // Panel poziom�w
    public CanvasGroup MainMenuCanvasGroup; // CanvasGroup g��wnego menu (do blokowania interakcji)
    public Toggle soundToggle;       // Prze��cznik d�wi�ku
    public Toggle musicToggle;       // Prze��cznik muzyki
    public AudioSource musicSource1; // �r�d�o muzyki

    private bool soundOn;
    private bool musicOn;

    private void Start()
    {
        MainMenu.SetActive(true);  // W��cz g��wne menu
        LevelsPanel.SetActive(false); // Ukryj panel poziom�w

        // Pobierz ustawienia z PlayerPrefs
        soundOn = PlayerPrefs.GetInt("sound", 1) == 1; // Domy�lnie w��czone
        musicOn = PlayerPrefs.GetInt("music", 1) == 1; // Domy�lnie w��czone

        // Zsynchronizuj prze��czniki z ustawieniami
        soundToggle.isOn = soundOn;
        musicToggle.isOn = musicOn;

        // Ustaw stan muzyki na podstawie zapisanych ustawie�
        UpdateMusicState();
    }

    public void NewGame()
    {
        // Za�aduj pierwsz� scen� z gry
        SceneManager.LoadScene("Level 1");
    }

    public void OpenLevelsPanel()
    {
        Debug.Log("OpenLevelsPanel called"); // Sprawd�, czy funkcja dzia�a
        LevelsPanel.SetActive(true);               // Poka� panel poziom�w
        Debug.Log("LevelsPanel active after: " + LevelsPanel.activeSelf);
        MainMenuCanvasGroup.interactable = false;  // Zablokuj interakcje z g��wnym menu
        MainMenuCanvasGroup.blocksRaycasts = false; // Przesta� rejestrowa� klikni�cia dla menu
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        // Wy��cz tryb Play w edytorze Unity
        EditorApplication.isPlaying = false;
#else
        // Wy��cz aplikacj� w �rodowisku uruchomieniowym
        Application.Quit();
#endif
    }

    // Prze��cznik d�wi�ku
    public void OnSoundToggle()
    {
        soundOn = soundToggle.isOn;
        PlayerPrefs.SetInt("sound", soundOn ? 1 : 0);
    }

    // Prze��cznik muzyki
    public void OnMusicToggle()
    {
        musicOn = musicToggle.isOn;
        PlayerPrefs.SetInt("music", musicOn ? 1 : 0);
        UpdateMusicState();
    }

    private void UpdateMusicState()
    {
        if (musicSource1 != null)
        {
            musicSource1.mute = !musicOn; // Wycisz lub w��cz muzyk�
        }
    }
}
