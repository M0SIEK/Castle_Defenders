using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public Toggle musicToggle; // Przypisz prze��cznik w menu ustawie�
    private AudioSource musicSource;

    private void Start()
    {
        // Pobierz AudioSource z tego obiektu
        musicSource = GetComponent<AudioSource>();

        // Zainicjalizuj stan muzyki na podstawie zapisanych ustawie�
        bool musicOn = PlayerPrefs.GetInt("music", 1) == 1;
        musicSource.mute = !musicOn;
        musicToggle.isOn = musicOn;

        // Dodaj listener do obs�ugi zmian prze��cznika
        musicToggle.onValueChanged.AddListener(OnMusicToggleChanged);
    }

    private void OnMusicToggleChanged(bool isOn)
    {
        // W��czaj/wy��czaj muzyk�
        musicSource.mute = !isOn;

        // Zapisz ustawienie
        PlayerPrefs.SetInt("music", isOn ? 1 : 0);
    }
}