using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo
{
    public List<int> roundScore { get; set; }
    public int finalScore { get; set; }
    public int reaminingTurns { get; set; }
    public Color playerColor { get; set; }
    public GameObject playerPanel { get; }
    public string playerName { get; set; }
    public int playerCount { get; private set; }

    public PlayerInfo(GameObject ppanel)
    {
        roundScore = new List<int>();
        playerColor = Color.black;
        playerPanel = ppanel;
        playerName = "Player " + (ArcheryGameManager._players.Count + 1);
        playerCount = ArcheryGameManager._players.Count + 1;
    }
}
