using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuController : MonoBehaviour
{
    public void NewGame()
    {
        // Za³aduj pierwsz¹ scenê z gry
        SceneManager.LoadScene("EndOfRoundScene");
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
}
