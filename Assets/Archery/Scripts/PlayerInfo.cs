using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : IComparable<PlayerInfo> , IComparer<PlayerInfo>
{
    public List<int> roundScore { get; set; }
    public int finalScore { get; set; }
    public int reaminingTurns { get; set; }
    public Color playerColor { get; set; }
    public GameObject playerPanel { get; }
    public string playerName { get; set; }

    public PlayerInfo(GameObject ppanel)
    {
        roundScore = new List<int>();
        playerColor = Color.black;
        playerPanel = ppanel;
        playerName = "Player " + (ArcheryGameManager._players.Count + 1);
    }

    public int Compare(PlayerInfo x, PlayerInfo y)
    {
        return x.finalScore.CompareTo(y.finalScore);
    }

    public int CompareTo(PlayerInfo other)
    {
        return finalScore >= other.finalScore ? 0 : 1;
    }
}
