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
    public AudioSource musicSource; // Dodaj referencjê do Audio Source

    private bool soundOn;
    private bool musicOn;

    private void Start()
    {
        settingsPanel.SetActive(false);

        // Pobierz ustawienia z PlayerPrefs
        soundOn = PlayerPrefs.GetInt("sound", 1) == 1; // Domyœlnie w³¹czone
        musicOn = PlayerPrefs.GetInt("music", 1) == 1; // Domyœlnie w³¹czone

        // Zsynchronizuj prze³¹czniki z ustawieniami
        soundToggle.isOn = soundOn;
        musicToggle.isOn = musicOn;

        // Ustaw stan muzyki na podstawie ustawieñ
        UpdateMusicState();
    }

    public void ShowSettingsPanel()
    {
        settingsPanel.SetActive(true);
        PauseGame();
    }

    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
        if (wavesController.GetComponent<WavesController>().GetGameStarted())
        {
            ResumeGame();
        }
    }

    public void RestartLevel()
    {
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
        UpdateMusicState(); // Aktualizuj muzykê
    }

    private void UpdateMusicState()
    {
        if (musicSource != null)
        {
            musicSource.mute = !musicOn; // Wycisz lub w³¹cz muzykê
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
