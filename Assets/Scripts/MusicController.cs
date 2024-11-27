using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public AudioSource musicSource1; // Muzyka w menu g³ównym
    public AudioSource musicSource2; // Muzyka w grze

    private void Start()
    {
        // W³¹cz odpowiedni¹ muzykê w zale¿noœci od sceny
        UpdateMusicForScene();

        // Dodaj listener do wykrywania zmiany sceny
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnDestroy()
    {
        // Usuñ listener, aby unikn¹æ wycieków pamiêci
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        // Aktualizuj muzykê po zmianie sceny
        UpdateMusicForScene();
    }

    private void UpdateMusicForScene()
    {
        // Pobierz aktualn¹ scenê
        string currentScene = SceneManager.GetActiveScene().name;

        // W³¹cz `musicSource1` tylko w menu g³ównym, `musicSource2` w grze
        if (currentScene == "MainMenuScene")
        {
            PlayMusic(musicSource1, true);
            PlayMusic(musicSource2, false);
        }
        else
        {
            PlayMusic(musicSource1, false);
            PlayMusic(musicSource2, true);
        }
    }

    private void PlayMusic(AudioSource source, bool play)
    {
        if (source != null)
        {
            source.mute = !play;
            if (play && !source.isPlaying)
            {
                source.Play();
            }
            else if (!play && source.isPlaying)
            {
                source.Stop();
            }
        }
    }
}
