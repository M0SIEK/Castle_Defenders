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
        // Za�aduj pierwsz� scen� z gry
        SceneManager.LoadScene("EndOfRoundScene");
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
}
