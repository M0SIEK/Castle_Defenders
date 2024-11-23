using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Runtime.Serialization;
using UnityEngine.SceneManagement;
using System;

[DataContract]
public class Scoreboard
{
    [DataMember]
    public string LevelName { get; set; }

    [DataMember]
    public int[] scores { get; set; }
}

public class ScoreboardController : MonoBehaviour
{
    public GameObject scoreboardPanel;
    public TextMeshProUGUI scoreboardTableText;
    public TMP_Dropdown selectLevelDropdown;

    private string scoreboardTableHeader;
    private string scoreboardTableContent;
    private Scoreboard scoreboard;
    private string selectedLvlName;
    private void Start()
    {
        scoreboardPanel.SetActive(false);

        selectedLvlName = SceneManager.GetActiveScene().name;
        SelectCurrentLevelScoreboard(selectedLvlName);
        scoreboard = ReadFromJsonFile(selectedLvlName);

        AddNewScore(1000);
        AddNewScore(1200);
        AddNewScore(900);

        scoreboardTableHeader = "Nr.\t\tWynik\n";
        scoreboardTableContent = GetScoreboardTableContent(scoreboard.scores);
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

    public void OnSelectLevelDropdownValueChanged()
    {
        selectedLvlName = selectLevelDropdown.captionText.text;
        scoreboard = ReadFromJsonFile(selectedLvlName);
        scoreboardTableContent = GetScoreboardTableContent(scoreboard.scores);
        scoreboardTableText.GetComponent<TextMeshProUGUI>().text = scoreboardTableHeader + scoreboardTableContent;
    }

    public void AddNewScore(int newScore)
    {
        int minScoreIndex = GetMinScoreIndex(scoreboard.scores);
        int minScore = scoreboard.scores[minScoreIndex];
        if (newScore > minScore)
        {
            scoreboard.scores[minScoreIndex] = newScore;
        }
        WriteToJsonFile(scoreboard);
    }

    private int GetMinScoreIndex(int[] scores)
    {
        int minScore = scores[0];
        int minIndex = 0;
        int i = 0;
        for (i = 1; i < scores.Length; i++)
        {
            if(scores[i] < minScore )
            {
                minScore = scores[i];
                minIndex = i;
            }
        }
        Debug.Log(i.ToString() + " " + minScore.ToString());
        return minIndex;
    }

    private string GetScoreboardTableContent(int[] scores)
    {
        string content = string.Empty;
        Array.Sort(scores, (a, b) => b.CompareTo(a));
        for (int i = 0; i<5; i++)
        {
            content += (i+1).ToString() + ". ";
            if (scores[i] == -1)
            {
                content += ".............................................\n";
            } else
            {
                content += scores[i].ToString() + "\n";
            }
        }
        return content;
    }

    private void SelectCurrentLevelScoreboard(string currentLevel)
    {
        for (int i = 0; i < selectLevelDropdown.options.Count; i++)
        {
            if (selectLevelDropdown.options[i].text == currentLevel)
            {
                selectLevelDropdown.value = i;
                selectLevelDropdown.RefreshShownValue();
                return;
            }
        }
    }

    private void WriteToJsonFile(Scoreboard sb)
    {
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Scoreboard));
        using (MemoryStream ms = new MemoryStream())
        {
            serializer.WriteObject(ms, sb);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            File.WriteAllText(sb.LevelName + ".json", jsonString);
        }
    }

    private Scoreboard ReadFromJsonFile(string fileName)
    {
        try
        {
            string jsonString = File.ReadAllText(fileName + ".json");

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Scoreboard));

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                Scoreboard scoreboard = (Scoreboard)serializer.ReadObject(ms);
                return scoreboard;
            }
        } catch(Exception)
        {
            return new Scoreboard { LevelName = selectedLvlName, scores = new int[]{ -1, -1, -1, -1, -1 } };
        }
    }
}
