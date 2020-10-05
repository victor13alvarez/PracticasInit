using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class ArcheryGameSetupManager : MonoBehaviour
{
    [SerializeField]int currentPlayers;
    [SerializeField] int currentRounds;
    const int maxPlayers = 4;
    const int minPlayers = 1;
    const int minRounds = 1;
    const int maxRounds = 4;
    public GameObject playerInputsPanelA;
    public GameObject playerInputsPanelB;
    public GameObject playerPanel;
    public GameObject playersIconPanel;

    public GameObject roundsIconPanel;
    public GameObject playerIcon;
    public GameObject roundIcon;
    public List<PlayerInfo> playerPanels = new List<PlayerInfo>();

    // Start is called before the first frame update
    void Start()
    {
        currentPlayers = 0;
        currentRounds = 0;
        playerPanels.Clear();
        AddPlayer();
        for (int i = 0; i < 3; i++)
            AddRound();
    }


    public void AddPlayer()
    {
        if (currentPlayers == maxPlayers)
            return;
        currentPlayers++;
        GameObject newGamePanel;
        if (currentPlayers < 3)
        {
            playerInputsPanelA.gameObject.GetComponentInParent<VerticalLayoutGroup>().childForceExpandHeight = false;
            newGamePanel = Instantiate(playerPanel, playerInputsPanelA.transform);

        }
        else
        {
            playerInputsPanelB.gameObject.GetComponentInParent<VerticalLayoutGroup>().childForceExpandHeight = true;
            newGamePanel = Instantiate(playerPanel, playerInputsPanelB.transform);
        }
        newGamePanel.name += currentPlayers;
        playerPanels.Add(new PlayerInfo(newGamePanel));
        Instantiate(playerIcon, playersIconPanel.transform);
    }
    public void RemovePlayer()
    {
        if (currentPlayers == minPlayers)
            return;
        currentPlayers--;
        if (currentPlayers < 3)
            playerInputsPanelA.gameObject.GetComponentInParent<VerticalLayoutGroup>().childForceExpandHeight = false;

        else
            playerInputsPanelB.gameObject.GetComponentInParent<VerticalLayoutGroup>().childForceExpandHeight = true;
        //Destroy(playerPanels[currentPlayers]);
        Destroy(playerPanels.Last<PlayerInfo>().playerPanel);
        playerPanels.Remove(playerPanels.Last<PlayerInfo>());
        Destroy(playersIconPanel.transform.GetChild(0).gameObject);
    }

    public void AddRound()
    {
        if (currentRounds == maxRounds)
            return;
        currentRounds++;
        Instantiate(roundIcon, roundsIconPanel.transform);
    }
    public void DeleteRound()
    {
        if (currentRounds == minRounds)
            return;
        currentRounds--;
        Destroy(roundsIconPanel.transform.GetChild(0).gameObject);

    }

    public void AddColorToPlayer(string color, int player)
    {
        print(color + " " + player);
    }

    private void OnGUI()
    {
        int x = 10;
        GUIStyle style = new GUIStyle();
        style.fontSize = 40;
        style.alignment = TextAnchor.MiddleCenter;
        foreach (var item in playerPanels)
        {
            GUI.Box(new Rect(10, x, 1000, 200), item.playerPanel.name,style);
            x += 250;
        }

        
    }

}
[System.Serializable]
public class PlayerInfo
{
    public int roundScore { get; set; }
    public int finalScore { get;}
    public int remainingArrows { get; set; }
    public int reaminingTurns { get; set; }
    public string arrowColor { get; set; }
    public GameObject playerPanel { get; }

    public PlayerInfo(GameObject playerPanel)
    {
        this.roundScore = 0;
        this.finalScore = 0;
        this.remainingArrows = 3;
        this.reaminingTurns = 3;
        this.arrowColor = "red";
        this.playerPanel = playerPanel;
    }
}
