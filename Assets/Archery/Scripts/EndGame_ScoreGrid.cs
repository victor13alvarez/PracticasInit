using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EndGame_ScoreGrid : MonoBehaviour
{
    public GameObject baseTextPanel;
    public GameObject baseTextPanelChild;

    private void Awake()
    {
        CreateScorePanel(ArcheryGameManager._totalRounds, ArcheryGameManager._players.ToArray());
    }

    //COLUMNS = TOTALROUNDS + 1 
    public void CreateScorePanel(int rounds, PlayerInfo [] players)
    {
        for (int i = 0; i <= rounds+1; i++) 
        {
            GameObject panel = Instantiate(baseTextPanel, this.transform);
            if (i == 0) ManageNamesPanel(players, panel);
            else if (i == rounds+1) ManageFinalScorePanels(players , panel);
            else ManageScorePanels(players, i, panel);
        }
    }

    //ROWS DEPENDING ON WHAT IS GOING TO BE DISPLAYED
    private void ManageScorePanels(PlayerInfo[] players, int round, GameObject panel)
    {
        for (int i = 0; i <= players.Length; i++) 
        {
            GameObject childPanel = Instantiate(baseTextPanelChild, panel.transform);
            TextMeshProUGUI text = childPanel.GetComponent<TextMeshProUGUI>();
            text.fontStyle = i == 0 ? FontStyles.Bold : FontStyles.Normal;
            if (i == 0) text.text = "ROUND " + round;
            else
            {
                text.color = players[i - 1].playerColor;
                text.text = players[i - 1].roundScore[round-1].ToString();
            }
        }
    }

    private void ManageFinalScorePanels(PlayerInfo[] players, GameObject panel)
    {
        for (int i = 0; i <= players.Length; i++) 
        {
            GameObject childPanel = Instantiate(baseTextPanelChild, panel.transform);
            TextMeshProUGUI text = childPanel.GetComponent<TextMeshProUGUI>();
            text.fontStyle = i == 0 ? FontStyles.Bold : FontStyles.Normal;
            if (i == 0) 
                text.text = "TOTAL";
            else
            {
                text.color = players[i - 1].playerColor;
                text.text = players[i - 1].finalScore.ToString();
            }
        }
    }

    private void ManageNamesPanel(PlayerInfo[] players, GameObject panel)
    {
        for (int i = 0; i <= players.Length; i++)
        {
            GameObject childPanel = Instantiate(baseTextPanelChild, panel.transform);
            TextMeshProUGUI text = childPanel.GetComponent<TextMeshProUGUI>();
            text.fontStyle = i == 0 ? FontStyles.Bold : FontStyles.Normal;
            if (i == 0) text.text = "PLAYER";
            else
            {
                text.color = players[i - 1].playerColor;
                text.text = players[i - 1].playerName.ToUpper();
            }
        }
    }
}
