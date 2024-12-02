using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Toggle soundToggle;          // Prze��cznik d�wi�ku
    public AudioSource[] soundEffects; // Tablica AudioSource dla efekt�w d�wi�kowych (bez muzyki)

    private bool isSoundOn = true;      // Flaga dla stanu d�wi�ku

    private void Start()
    {
        // Wczytaj zapisany stan d�wi�ku
        isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        soundToggle.isOn = isSoundOn;

        // Zaktualizuj stan d�wi�ku
        UpdateSoundState();

        // Dodaj listener do prze��cznika
        soundToggle.onValueChanged.AddListener(delegate { ToggleSound(); });
    }

    public void ToggleSound()
    {
        // Zmie� stan d�wi�ku na podstawie warto�ci prze��cznika
        isSoundOn = soundToggle.isOn;

        // Zapisz nowy stan
        PlayerPrefs.SetInt("SoundOn", isSoundOn ? 1 : 0);
        PlayerPrefs.Save();

        // Zaktualizuj stan efekt�w d�wi�kowych
        UpdateSoundState();
    }

    private void UpdateSoundState()
    {
        // Wy��cz lub w��cz wszystkie efekty d�wi�kowe
        foreach (var audioSource in soundEffects)
        {
            if (audioSource != null)
            {
                audioSource.mute = !isSoundOn;
            }
        }
    }
}
