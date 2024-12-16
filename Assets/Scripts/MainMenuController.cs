using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuController : MonoBehaviour
{
    public GameObject MainMenu;       // G≥Ûwne menu
    public GameObject LevelsPanel;   // Panel poziomÛw
    public CanvasGroup MainMenuCanvasGroup; // CanvasGroup g≥Ûwnego menu (do blokowania interakcji)
    public Toggle soundToggle;       // Prze≥πcznik düwiÍku
    public Toggle musicToggle;       // Prze≥πcznik muzyki
    public AudioSource musicSource1; // èrÛd≥o muzyki

    private bool soundOn;
    private bool musicOn;

    private void Start()
    {
        MainMenu.SetActive(true);  // W≥πcz g≥Ûwne menu
        LevelsPanel.SetActive(false); // Ukryj panel poziomÛw

        // Pobierz ustawienia z PlayerPrefs
        soundOn = PlayerPrefs.GetInt("sound", 1) == 1; // Domyúlnie w≥πczone
        musicOn = PlayerPrefs.GetInt("music", 1) == 1; // Domyúlnie w≥πczone

        // Zsynchronizuj prze≥πczniki z ustawieniami
        soundToggle.isOn = soundOn;
        musicToggle.isOn = musicOn;

        // Ustaw stan muzyki na podstawie zapisanych ustawieÒ
        UpdateMusicState();
    }

    public void NewGame()
    {
        // Za≥aduj pierwszπ scenÍ z gry
        SceneManager.LoadScene("Level 1");
    }

    public void OpenLevelsPanel()
    {
        Debug.Log("OpenLevelsPanel called"); // Sprawdü, czy funkcja dzia≥a
        LevelsPanel.SetActive(true);               // Pokaø panel poziomÛw
        Debug.Log("LevelsPanel active after: " + LevelsPanel.activeSelf);
        MainMenuCanvasGroup.interactable = false;  // Zablokuj interakcje z g≥Ûwnym menu
        MainMenuCanvasGroup.blocksRaycasts = false; // PrzestaÒ rejestrowaÊ klikniÍcia dla menu
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        // Wy≥πcz tryb Play w edytorze Unity
        EditorApplication.isPlaying = false;
#else
        // Wy≥πcz aplikacjÍ w úrodowisku uruchomieniowym
        Application.Quit();
#endif
    }

    // Prze≥πcznik düwiÍku
    public void OnSoundToggle()
    {
        soundOn = soundToggle.isOn;
        PlayerPrefs.SetInt("sound", soundOn ? 1 : 0);
    }

    // Prze≥πcznik muzyki
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
            musicSource1.mute = !musicOn; // Wycisz lub w≥πcz muzykÍ
        }
    }
}
