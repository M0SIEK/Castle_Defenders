using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public AudioSource musicSource1; // Muzyka w menu g��wnym
    public AudioSource musicSource2; // Muzyka w grze

    private void Start()
    {
        // W��cz odpowiedni� muzyk� w zale�no�ci od sceny
        UpdateMusicForScene();

        // Dodaj listener do wykrywania zmiany sceny
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnDestroy()
    {
        // Usu� listener, aby unikn�� wyciek�w pami�ci
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        // Aktualizuj muzyk� po zmianie sceny
        UpdateMusicForScene();
    }

    private void UpdateMusicForScene()
    {
        // Pobierz aktualn� scen�
        string currentScene = SceneManager.GetActiveScene().name;

        // W��cz `musicSource1` tylko w menu g��wnym, `musicSource2` w grze
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
