using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class ArcheryGameSetupManager : MonoBehaviour
{
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
        currentRounds = 0;
        playerPanels.Clear();
        AddPlayer();
        for (int i = 0; i < 3; i++)
            AddRound();
    }


    public void AddPlayer()
    {
        if (playerPanels.Count == maxPlayers)
            return;
        GameObject newGamePanel;
        if (playerPanels.Count < 2)
        {
            playerInputsPanelA.gameObject.GetComponentInParent<VerticalLayoutGroup>().childForceExpandHeight = false;
            newGamePanel = Instantiate(playerPanel, playerInputsPanelA.transform);

        }
        else
        {
            playerInputsPanelB.gameObject.GetComponentInParent<VerticalLayoutGroup>().childForceExpandHeight = true;
            newGamePanel = Instantiate(playerPanel, playerInputsPanelB.transform);
        }
        playerPanels.Add(new PlayerInfo(newGamePanel , playerPanels.Count));
        newGamePanel.name += playerPanels.Count;
        Instantiate(playerIcon, playersIconPanel.transform);
    }
    public void RemovePlayer()
    {
        if (playerPanels.Count == minPlayers)
            return;
        if (playerPanels.Count < 4)
            playerInputsPanelA.gameObject.GetComponentInParent<VerticalLayoutGroup>().childForceExpandHeight = false;

        else
            playerInputsPanelB.gameObject.GetComponentInParent<VerticalLayoutGroup>().childForceExpandHeight = true;

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
        playerPanels[player - 1].arrowColor = color;
    }
    public void SetNameOfPlayer(string name, int player)
    {
        playerPanels[player - 1].playerName = name;
    }
    public void SetUpGameScene()
    {
        GameObject archeryGameManager = Instantiate(new GameObject());
        archeryGameManager.gameObject.name = "ArcheryGameManager";
        ArcheryGameManager provisionalManger = archeryGameManager.AddComponent<ArcheryGameManager>();
        provisionalManger.players = playerPanels.ToArray();
        provisionalManger.totalRounds = currentRounds;
        DontDestroyOnLoad(archeryGameManager);
    }
}
[System.Serializable]
public class PlayerInfo
{
    public int roundScore { get; set; }
    public int finalScore { get; set; }
    public int reaminingTurns { get; set; }
    public string arrowColor { get; set; }
    public GameObject playerPanel { get; }
    public string playerName { get; set; }

    public PlayerInfo(GameObject playerPanel, int currentPlayer)
    {
        this.roundScore = 0;
        this.finalScore = 0;
        this.reaminingTurns = 3;
        this.arrowColor = "red";
        this.playerPanel = playerPanel;
        this.playerName = "Player " + (currentPlayer+1).ToString() ;

    }
}
