using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuController : MonoBehaviour
{
    public GameObject MainMenu;
    public Toggle soundToggle;  // Prze³¹cznik dŸwiêku
    public Toggle musicToggle;  // Prze³¹cznik muzyki
    public AudioSource musicSource1; // Zmieniono na MusicSource1

    private bool soundOn;
    private bool musicOn;

    private void Start()
    {
        MainMenu.SetActive(true);  // Upewnij siê, ¿e menu jest aktywne

        // Pobierz ustawienia z PlayerPrefs
        soundOn = PlayerPrefs.GetInt("sound", 1) == 1; // Domyœlnie w³¹czone
        musicOn = PlayerPrefs.GetInt("music", 1) == 1; // Domyœlnie w³¹czone

        // Zsynchronizuj prze³¹czniki z ustawieniami
        soundToggle.isOn = soundOn;
        musicToggle.isOn = musicOn;

        // Ustaw stan muzyki na podstawie zapisanych ustawieñ
        UpdateMusicState();
    }

    public void NewGame()
    {
        // Za³aduj pierwsz¹ scenê z gry
        SceneManager.LoadScene("Demo3Scene");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        // Wy³¹cz tryb Play w edytorze Unity
        EditorApplication.isPlaying = false;
#else
        // Wy³¹cz aplikacjê w œrodowisku uruchomieniowym
        Application.Quit();
#endif
    }

    // Prze³¹cznik dŸwiêku - kontroluje globaln¹ g³oœnoœæ gry
    public void OnSoundToggle()
    {
        soundOn = soundToggle.isOn;
        PlayerPrefs.SetInt("sound", soundOn ? 1 : 0);
    }

    // Prze³¹cznik muzyki - kontroluje wyciszenie muzyki
    public void OnMusicToggle()
    {
        musicOn = musicToggle.isOn;
        PlayerPrefs.SetInt("music", musicOn ? 1 : 0);
        // Zaktualizuj stan muzyki
        UpdateMusicState();
    }

    // Funkcja do kontrolowania stanu muzyki
    private void UpdateMusicState()
    {
        if (musicSource1 != null)
        {
            musicSource1.mute = !musicOn; // Wycisz lub w³¹cz MusicSource1
        }
    }
}
