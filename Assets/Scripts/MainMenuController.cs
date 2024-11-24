using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void NewGame()
    {
        // Za³aduj pierwsz¹ scenê z gry
        SceneManager.LoadScene("EndOfRoundScene");
    }
}
