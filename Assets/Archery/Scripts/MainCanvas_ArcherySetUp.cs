using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class MainCanvas_ArcherySetUp : MonoBehaviour
{
    const int maxPlayers = 4;
    const int minPlayers = 1;
    const int minRounds = 1;
    const int maxRounds = 5;

    public GameObject playerInputsPanelA;
    public GameObject playerInputsPanelB;
    public GameObject playerPanel;
    public GameObject playersIconPanel;

    public GameObject roundsIconPanel;
    public GameObject playerIcon;
    public GameObject roundIcon;



    // Start is called before the first frame update
    void Start()
    {
        ArcheryGameManager._players.Clear();
        AddPlayer();
        AddRound();
    }


    public void AddPlayer()
    {
        if (ArcheryGameManager._players.Count == maxPlayers)
            return;
        GameObject newGamePanel;
        if (ArcheryGameManager._players.Count < 2)
            newGamePanel = Instantiate(playerPanel, playerInputsPanelA.transform);

        else
            newGamePanel = Instantiate(playerPanel, playerInputsPanelB.transform);

        ArcheryGameManager._players.Add(new PlayerInfo(newGamePanel));

        Instantiate(playerIcon, playersIconPanel.transform);
    }
    public void RemovePlayer()
    {
        if (ArcheryGameManager._players.Count == minPlayers)
            return;
        ArcheryGameManager._players.Last<PlayerInfo>().playerPanel.GetComponent<PlayerPanel_ArcherySetUp>().DestroyPanel();
        ArcheryGameManager._players.Remove(ArcheryGameManager._players.Last<PlayerInfo>());
        Destroy(playersIconPanel.transform.GetChild(0).gameObject);

    }

    public void AddRound()
    {
        if (ArcheryGameManager._totalRounds == maxRounds)
            return;
        ArcheryGameManager._totalRounds++;
        Instantiate(roundIcon, roundsIconPanel.transform);
    }
    public void DeleteRound()
    {
        if (ArcheryGameManager._totalRounds == minRounds)
            return;
        ArcheryGameManager._totalRounds--;
        Destroy(roundsIconPanel.transform.GetChild(0).gameObject);
    }
}