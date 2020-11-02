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
            //playerInputsPanelA.gameObject.GetComponentInParent<HorizontalLayoutGroup>().childForceExpandHeight = false;
            //f(playerPanels.Count ==  1) playerInputsPanelA.gameObject.GetComponentInParent<VerticalLayoutGroup>().childForceExpandHeight = false;
            newGamePanel = Instantiate(playerPanel, playerInputsPanelA.transform);
            if(playerPanels.Count == 1)
            {
                playerInputsPanelA.gameObject.GetComponentInParent<HorizontalLayoutGroup>().childControlWidth = true;
                playerInputsPanelA.gameObject.GetComponentInParent<HorizontalLayoutGroup>().childForceExpandWidth = true;
            }

        }
        else
        {
            //playerInputsPanelB.gameObject.GetComponentInParent<HorizontalLayoutGroup>().childForceExpandHeight = true;
            newGamePanel = Instantiate(playerPanel, playerInputsPanelB.transform);
            if (playerPanels.Count == 3)
            {
                playerInputsPanelB.gameObject.GetComponentInParent<HorizontalLayoutGroup>().childControlWidth = true;
                playerInputsPanelB.gameObject.GetComponentInParent<HorizontalLayoutGroup>().childForceExpandWidth = true;
            }
        }
        playerPanels.Add(new PlayerInfo(newGamePanel , playerPanels.Count));
        newGamePanel.name += playerPanels.Count;
        Instantiate(playerIcon, playersIconPanel.transform);
    }
    public void RemovePlayer()
    {
        if (playerPanels.Count == minPlayers)
            return;
        if (playerPanels.Count <= 2)
        {

            playerInputsPanelA.gameObject.GetComponentInParent<HorizontalLayoutGroup>().childControlWidth = false;
            playerInputsPanelA.gameObject.GetComponentInParent<HorizontalLayoutGroup>().childForceExpandWidth = false;
        }
        else
        {
            playerInputsPanelB.gameObject.GetComponentInParent<HorizontalLayoutGroup>().childControlWidth = false;
            playerInputsPanelB.gameObject.GetComponentInParent<HorizontalLayoutGroup>().childForceExpandWidth = false;
        }

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
        provisionalManger.SetPlayerRoundScore();
        DontDestroyOnLoad(archeryGameManager);
    }
}

public class PlayerInfo
{
    public List<int> roundScore { get; set; }
    public int finalScore { get; set; }
    public int reaminingTurns { get; set; }
    public string arrowColor { get; set; }
    public GameObject playerPanel { get; }
    public string playerName { get; set; }

    public PlayerInfo(GameObject ppanel, int currentPlayer)
    {
        roundScore = new List<int>();
        finalScore = 0;
        reaminingTurns = 3;
        arrowColor = "red";
        playerPanel = ppanel;
        playerName = "Player " + (currentPlayer+1).ToString() ;
    }
}
