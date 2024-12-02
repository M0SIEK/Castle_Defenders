using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Toggle soundToggle;          // Prze³¹cznik dŸwiêku
    public AudioSource[] soundEffects; // Tablica AudioSource dla efektów dŸwiêkowych (bez muzyki)

    private bool isSoundOn = true;      // Flaga dla stanu dŸwiêku

    private void Start()
    {
        // Wczytaj zapisany stan dŸwiêku
        isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        soundToggle.isOn = isSoundOn;

        // Zaktualizuj stan dŸwiêku
        UpdateSoundState();

        // Dodaj listener do prze³¹cznika
        soundToggle.onValueChanged.AddListener(delegate { ToggleSound(); });
    }

    public void ToggleSound()
    {
        // Zmieñ stan dŸwiêku na podstawie wartoœci prze³¹cznika
        isSoundOn = soundToggle.isOn;

        // Zapisz nowy stan
        PlayerPrefs.SetInt("SoundOn", isSoundOn ? 1 : 0);
        PlayerPrefs.Save();

        // Zaktualizuj stan efektów dŸwiêkowych
        UpdateSoundState();
    }

    private void UpdateSoundState()
    {
        // Wy³¹cz lub w³¹cz wszystkie efekty dŸwiêkowe
        foreach (var audioSource in soundEffects)
        {
            if (audioSource != null)
            {
                audioSource.mute = !isSoundOn;
            }
        }
    }
}
