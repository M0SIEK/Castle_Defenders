using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour
{
    public GameObject settingsPanel;
    public Toggle soundToggle;
    public Toggle musicToggle;
    public GameObject wavesController;
    public AudioSource musicSource2;
    public static bool SettingsPanelActive { get; private set; } = false;

    private bool soundOn;
    private bool musicOn;

    private void Start()
    {
        settingsPanel.SetActive(false);

        // Pobierz ustawienia z PlayerPrefs
        soundOn = PlayerPrefs.GetInt("sound", 1) == 1; // Domy�lnie w��czone
        musicOn = PlayerPrefs.GetInt("music", 1) == 1; // Domy�lnie w��czone

        // Zsynchronizuj prze��czniki z ustawieniami
        soundToggle.isOn = soundOn;
        musicToggle.isOn = musicOn;

        // Ustaw stan muzyki na podstawie ustawie�
        UpdateMusicState();
    }

    public void ShowSettingsPanel()
    {
        settingsPanel.SetActive(true);
        SettingsPanelActive = true; // Ustaw flag�
        PauseGame();
    }

    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
        SettingsPanelActive = false; // Wy��cz flag�
        if (wavesController.GetComponent<WavesController>().GetGameStarted())
        {
            ResumeGame();
        }
    }

    public void RestartLevel()
    {
        // Wy��cz panel ustawie� przed restartem
        settingsPanel.SetActive(false);
        SettingsPanelActive = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log("Load " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void OnSoundToggle()
    {
        soundOn = soundToggle.isOn;
        PlayerPrefs.SetInt("sound", soundOn ? 1 : 0);
    }

    public void OnMusicToggle()
    {
        musicOn = musicToggle.isOn;
        PlayerPrefs.SetInt("music", musicOn ? 1 : 0);
        UpdateMusicState(); // Aktualizuj muzyk�
    }

    private void UpdateMusicState()
    {
        if (musicSource2 != null)
        {
            musicSource2.mute = !musicOn; // Wycisz lub w��cz MusicSource2
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
    }
}
