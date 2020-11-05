using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcheryGameManager : MonoBehaviour
{
    public PlayerInfo [] players { get; set; }
    public int totalRounds;
    ArrowManager arrowManager;
    CanvasInputArcheryGame canvasInputArcheryGame;
    WindSimulation windSimulation;
    int currentPlayer;
    int currentRounds;
    bool playerChanged = false;

    private void Awake()
    {
        currentPlayer = 0;
        currentRounds = 0;
    }
    public void SetPlayerRoundScore()
    {
        foreach (PlayerInfo player in players)
        {
            for (int i = 0; i < totalRounds; i++)
                player.roundScore.Add(0);
        }
    }
    void ManageRounds()
    {
        if (currentRounds == totalRounds)
            EndGame();
        else
        {
            currentRounds++;
            canvasInputArcheryGame.NewRound(currentRounds.ToString());
            foreach (PlayerInfo player in players)
            {
                player.reaminingTurns = 3;
            }
            int aux = Mathf.RoundToInt(UnityEngine.Random.value * 10);
            if (aux % 2 == 0)
                windSimulation.RoundHasWind();
            else
                windSimulation.RoundIsQuiet();
        }
    }

    private void EndGame()
    {
        canvasInputArcheryGame.EndGame(totalRounds , players);
        Destroy(arrowManager.transform.parent.gameObject);
    }

    public void ArrowThrowed(int score)
    {
        if (score == 0)
            arrowManager.DestroyArrow();
        else
            arrowManager.FreezeCurrentArrow();
        if(ManagePlayer(score))
            ManageRounds();
        canvasInputArcheryGame.SetGameInfoText(players[currentPlayer].playerName, players[currentPlayer].roundScore[currentRounds-1], currentRounds);
        if(playerChanged)
            StartCoroutine(SpawnArrowDelay());
        else
            arrowManager.SpawnNewArrow(this);

    }
    bool ManagePlayer(int score)
    {
        ManagePlayerScore(score);
        players[currentPlayer].reaminingTurns -= 1;
        if (players[currentPlayer].reaminingTurns == 0)
        {
            playerChanged = true;
            if (currentPlayer == players.Length - 1)
            {
                currentPlayer = 0;
                arrowManager.DestroyCurrentArrows();
                return true;
            }
            currentPlayer += 1;
            canvasInputArcheryGame.NewPlayerTurn(players[currentPlayer].playerName);
            arrowManager.DestroyCurrentArrows();

        }
        return false;
    }

    private void ManagePlayerScore(int score)
    {
        players[currentPlayer].roundScore[currentRounds-1] += score;
        players[currentPlayer].finalScore += score;
    }
    public void GameHasStarted()
    {
        arrowManager = FindObjectOfType<ArrowManager>();
        windSimulation = FindObjectOfType<WindSimulation>();
        canvasInputArcheryGame = FindObjectOfType<CanvasInputArcheryGame>();
        arrowManager.arrowWind = windSimulation;
        StartCoroutine(SpawnArrowDelay());
        ManageRounds();
        canvasInputArcheryGame.NewPlayerTurn(players[currentPlayer].playerName);
        canvasInputArcheryGame.SetGameInfoText(players[currentPlayer].playerName, players[currentPlayer].roundScore[currentRounds-1], currentRounds);
    }

    public void ResetGame()
    {
        foreach (PlayerInfo player in players)
        {
            player.reaminingTurns = 3;
            player.roundScore.Clear();
            player.finalScore = 0;
        }
        SetPlayerRoundScore();
        currentPlayer = 0;
        currentRounds = 0;
        canvasInputArcheryGame.GetComponent<SceneChange>().changeScene("ArcheryGame");
    }

    IEnumerator SpawnArrowDelay()
    {
        playerChanged = false;
        yield return new WaitForSeconds(2.5f);
        arrowManager.SpawnNewArrow(this);
    }
}
