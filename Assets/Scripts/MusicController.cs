using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public Toggle musicToggle; // Przypisz prze³¹cznik w menu ustawieñ
    private AudioSource musicSource;

    private void Start()
    {
        // Pobierz AudioSource z tego obiektu
        musicSource = GetComponent<AudioSource>();

        // Zainicjalizuj stan muzyki na podstawie zapisanych ustawieñ
        bool musicOn = PlayerPrefs.GetInt("music", 1) == 1;
        musicSource.mute = !musicOn;
        musicToggle.isOn = musicOn;

        // Dodaj listener do obs³ugi zmian prze³¹cznika
        musicToggle.onValueChanged.AddListener(OnMusicToggleChanged);
    }

    private void OnMusicToggleChanged(bool isOn)
    {
        // W³¹czaj/wy³¹czaj muzykê
        musicSource.mute = !isOn;

        // Zapisz ustawienie
        PlayerPrefs.SetInt("music", isOn ? 1 : 0);
    }
}