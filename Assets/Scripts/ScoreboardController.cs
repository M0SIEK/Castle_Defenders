using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardController : MonoBehaviour
{
    public GameObject scoreboardPanel;
    public TextMeshProUGUI scoreboardTableText;
    public int numberOfLevels;

    private string scoreboardTableHeader;
    private string scoreboardTableContent;
    private void Start()
    {
        scoreboardPanel.SetActive(false);
        scoreboardTableHeader = "Nr.\t\tWynik\n";
        scoreboardTableContent = GetScoreboardTableContent();
        scoreboardTableText.GetComponent<TextMeshProUGUI>().text = scoreboardTableHeader + scoreboardTableContent;
    }
    public void ShowScoreboardPanel()
    {
        scoreboardPanel.SetActive(true);

    }

    public void CloseScoreboardPanel()
    {
        scoreboardPanel.SetActive(false);
    }

    private string GetScoreboardTableContent()
    {
        string content = string.Empty;
        for (int i = 0; i<numberOfLevels; i++)
        {
            content += (i+1).ToString() + ". .............................................\n";
        }
        return content;
    }
}
