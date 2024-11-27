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
    public Toggle soundToggle;  // Prze��cznik d�wi�ku
    public Toggle musicToggle;  // Prze��cznik muzyki
    public AudioSource musicSource1; // Zmieniono na MusicSource1

    private bool soundOn;
    private bool musicOn;

    private void Start()
    {
        MainMenu.SetActive(true);  // Upewnij si�, �e menu jest aktywne

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
        SceneManager.LoadScene("Demo3Scene");
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

    // Prze��cznik d�wi�ku - kontroluje globaln� g�o�no�� gry
    public void OnSoundToggle()
    {
        soundOn = soundToggle.isOn;
        PlayerPrefs.SetInt("sound", soundOn ? 1 : 0);
    }

    // Prze��cznik muzyki - kontroluje wyciszenie muzyki
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
            musicSource1.mute = !musicOn; // Wycisz lub w��cz MusicSource1
        }
    }
}
