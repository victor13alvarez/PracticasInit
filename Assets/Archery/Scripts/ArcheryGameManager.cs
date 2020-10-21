using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcheryGameManager : MonoBehaviour
{
    public PlayerInfo [] players { get; set; }
    public int totalRounds { get; set; }
    ArrowManager arrowManager;
    CanvasInputArcheryGame canvasInputArcheryGame;
    WindSimulation windSimulation;
    int currentPlayer;
    int currentRounds;


    private void Awake()
    {
        windSimulation = this.gameObject.AddComponent<WindSimulation>();
        currentPlayer = 0;
        currentRounds = 0;
    }

    void ManageRounds()
    {
        if (currentRounds == totalRounds)
            EndGame();
        else
        {
            currentRounds++;
            foreach (PlayerInfo player in players)
            {
                player.reaminingTurns = 3;
                player.roundScore = 0;
            }
            print("RANDOM INC");
            int aux = Mathf.RoundToInt(UnityEngine.Random.value * 10);
            print(aux);
            if (aux % 2 == 0)
                windSimulation.RoundHasWind();
            else
                windSimulation.RoundIsQuiet();


        }
    }

    private void EndGame()
    {
        canvasInputArcheryGame.EndGame();
        Destroy(arrowManager.transform.parent.gameObject);
    }

    public void ArrowThrowed(int score)
    {
        arrowManager.DestroyCurrentArrow();
        if(ManagePlayer(score))
            ManageRounds();
        canvasInputArcheryGame.SetGameInfoText(players[currentPlayer].playerName, players[currentPlayer].roundScore, currentRounds);
        arrowManager.SpawnNewArrow(this);
    }
    bool ManagePlayer(int score)
    {
        ManagePlayerScore(score);
        players[currentPlayer].reaminingTurns -= 1;
        if (players[currentPlayer].reaminingTurns == 0)
        {
            if (currentPlayer == players.Length - 1)
            {
                currentPlayer = 0;
                return true;
            }
            else currentPlayer += 1;
        }
        return false;
    }

    private void ManagePlayerScore(int score)
    {
        players[currentPlayer].roundScore += score;
        players[currentPlayer].finalScore += score;
    }
    public void GameHasStarted()
    {
        arrowManager = FindObjectOfType<ArrowManager>();
        arrowManager.SpawnNewArrow(this);
        arrowManager.arrowWind = windSimulation;
        canvasInputArcheryGame = FindObjectOfType<CanvasInputArcheryGame>();
        canvasInputArcheryGame.SetGameInfoText(players[currentPlayer].playerName, players[currentPlayer].roundScore, currentRounds);
        ManageRounds();
    }

    public void ResetGame()
    {
        foreach (PlayerInfo player in players)
        {
            player.reaminingTurns = 3;
            player.roundScore = 0;
            player.finalScore = 0;
        }
        currentPlayer = 0;
        currentRounds = 1;
        canvasInputArcheryGame.SetGameInfoText(players[currentPlayer].playerName, players[currentPlayer].roundScore, currentRounds);
        arrowManager.DestroyCurrentArrow();
        arrowManager.SpawnNewArrow(this);
    }
}
