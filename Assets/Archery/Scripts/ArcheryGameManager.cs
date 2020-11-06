using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcheryGameManager : MonoBehaviour
{
    public PlayerInfo[] players { get; set; }
    public int totalRounds;
    int currentPlayer;
    int currentRound;

    ArrowManager arrowManager;
    CanvasInputArcheryGame canvasInputArcheryGame;
    WindSimulation windSimulation;


    #region VariableMethods

    public void SetUpGameInfo(PlayerInfo[] _playerInfos, int _totalRounds)
    {
        
        players = _playerInfos;
        totalRounds = _totalRounds;
        SetUpGameInfo();
    }
    private void SetUpGameInfo()
    {
        currentRound = 0;
        currentPlayer = 0;
        SetPlayerRoundScore();
    }
    void SetPlayerRoundScore()
    {
        foreach (PlayerInfo player in players)
        {
            player.reaminingTurns = 3;
            player.finalScore = 0;
            player.roundScore.Clear();
            for (int i = 0; i < totalRounds; i++)
                player.roundScore.Add(0);
        }
    }

    private void FindElementsInScene()
    {
        arrowManager = FindObjectOfType<ArrowManager>();
        windSimulation = FindObjectOfType<WindSimulation>();
        canvasInputArcheryGame = FindObjectOfType<CanvasInputArcheryGame>();
    }

    private void SetUpOtherSceneGObj()
    {
        arrowManager.arrowWind = windSimulation;
        canvasInputArcheryGame.everythingIsPrepared = new Action(SpawnArrow);
        canvasInputArcheryGame.LocateAnimationEnded();
    }
    #endregion

    #region INGAME_METHODS

    void ManageRounds()
    {
        if (currentRound == totalRounds)
            EndGame();
        else
        {
            currentRound++;
            foreach (PlayerInfo player in players)
            {
                player.reaminingTurns = 3;
            }
            int aux = Mathf.RoundToInt(UnityEngine.Random.value * 10);
            if (aux % 2 == 0)
                windSimulation.RoundHasWind();
            else
                windSimulation.RoundIsQuiet();

            //TODO POPUP nueva Ronda
            canvasInputArcheryGame.NewRoundTurn(currentRound);
        }
    }
    void ManagePlayerTurn()
    {
        players[currentPlayer].reaminingTurns -= 1;
        if (players[currentPlayer].reaminingTurns == 0)
        {
            if (currentPlayer == players.Length - 1)
                currentPlayer = 0;
            else currentPlayer += 1;
            arrowManager.DestroyCurrentArrows();
            //TODO POPUP nuevo jugador
            canvasInputArcheryGame.NewPlayerTurn(players[currentPlayer].playerName);
        }
        else
            arrowManager.SpawnNewArrow(this);
    }

    void ManagePlayerScore(int score)
    {
        players[currentPlayer].roundScore[currentRound - 1] += score;
        players[currentPlayer].finalScore += score;
        if (score == 0)
            arrowManager.DestroyArrow();
        else
            arrowManager.FreezeCurrentArrow();

        //TODO Cambiar score jugador
        canvasInputArcheryGame.UpdatePlayerScore(players[currentPlayer].roundScore[currentRound - 1]);
    }

    public void ArrowThrowed(int score)
    {
        ManagePlayerScore(score);
        ManagePlayerTurn();
        if (players[players.Length - 1].reaminingTurns == 0)
            ManageRounds();
    }


    void SpawnArrow() { arrowManager.SpawnNewArrow(this); }
    #endregion



    #region START RESET END
    public void GameHasStarted()
    {
        FindElementsInScene();
        SetUpOtherSceneGObj();
        ManageRounds();
        canvasInputArcheryGame.NewPlayerTurn(players[currentPlayer].playerName);
    }

    public void ResetGame()
    {
        SetUpGameInfo();
        canvasInputArcheryGame.GetComponent<SceneChange>().changeScene("ArcheryGame");
    }
    private void EndGame()
    {
        canvasInputArcheryGame.EndGame(totalRounds, players);
        Destroy(arrowManager.transform.parent.gameObject);
    }
    #endregion
}
