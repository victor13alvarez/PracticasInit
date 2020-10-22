using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreGird : MonoBehaviour
{
    public GameObject baseTextPanel;
    public GameObject baseTextPanelChild;

    public void CreateScorePanel(int rounds, PlayerInfo [] players)
    {
        for (int i = 0; i <= rounds+1; i++) //COLUMNAS
        {
            GameObject panel = Instantiate(baseTextPanel, this.transform);
            if (i == 0) ManageNamesPanel(players, panel);
            else if (i == rounds+1) ManageFinalScorePanels(players , panel);
            else ManageScorePanels(players, i, panel);
        }
    }

    private void ManageScorePanels(PlayerInfo[] players, int round, GameObject panel)
    {
        for (int i = 0; i <= players.Length; i++) //FILAS
        {
            GameObject childPanel = Instantiate(baseTextPanelChild, panel.transform);
            Text text = childPanel.GetComponent<Text>();
            if (i == 0) {
                text.fontStyle = FontStyle.Bold;
                text.text = "ROUND " + round;
            }
            else {
                text.fontStyle = FontStyle.Normal;
                text.text = players[i - 1].roundScore[round-1].ToString();
            }
        }
    }

    private void ManageFinalScorePanels(PlayerInfo[] players, GameObject panel)
    {
        for (int i = 0; i <= players.Length; i++) //FILAS
        {
            GameObject childPanel = Instantiate(baseTextPanelChild, panel.transform);
            Text text = childPanel.GetComponent<Text>();
            text.fontStyle = FontStyle.Bold;
            if (i == 0) text.text = "TOTAL";
            else text.text = players[i - 1].finalScore.ToString();
        }
    }

    private void ManageNamesPanel(PlayerInfo[] players, GameObject panel)
    {
        for (int i = 0; i <= players.Length; i++) //FILAS
        {
            GameObject childPanel = Instantiate(baseTextPanelChild, panel.transform);
            Text text = childPanel.GetComponent<Text>();
            text.fontStyle = FontStyle.Bold;
            if (i == 0) text.text = "PLAYER"; 
            else text.text = players[i-1].playerName.ToUpper();
        }
    }
}
