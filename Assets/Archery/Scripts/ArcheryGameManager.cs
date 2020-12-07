using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArcheryGameManager : MonoBehaviour
{
    public static List<PlayerInfo> _players { get; set; }
    public static int _totalRounds;

    [SerializeField] int _currentPlayer;
    [SerializeField] int _currentRound;

    ArrowManager _arrowManager;
    MainCanvas_ArcheryGame _canvasInputArcheryGame;
    WindSimulation _windSimulation;

    public static string _playerHasWin = "";
    int _maximumScorePlayer = 0;


    #region VariableMethods

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _players = new List<PlayerInfo>();
        _totalRounds = 0;
        SetEverythingToNewGame();

    }
    void SetEverythingToNewGame() //Restart the game to default values
    {

        _currentRound = 0;
        _currentPlayer = 0;
        _playerHasWin = "";
        SetPlayerRoundScore();
    }

    void SetPlayerRoundScore() //Restar each player score to default values for every game start
    {
        foreach (PlayerInfo player in _players)
        {
            player.reaminingTurns = 3;
            player.finalScore = 0;
            player.roundScore.Clear();
            for (int i = 0; i < _totalRounds; i++)
                player.roundScore.Add(0);
        }
    }

    private void FindElementsInScene()
    {
        _arrowManager = FindObjectOfType<ArrowManager>();
        _windSimulation = FindObjectOfType<WindSimulation>();
        _canvasInputArcheryGame = FindObjectOfType<MainCanvas_ArcheryGame>();
    }

    private void SetUpOtherSceneGObj()
    {
        _arrowManager.arrowWind = _windSimulation;
        _canvasInputArcheryGame.everythingIsPrepared = new Action(SpawnArrow);
        _canvasInputArcheryGame.LocateAnimationEnded();
    }
    #endregion

    #region INGAME_METHODS

    void ManageRounds() //INGAME , manage every round change. A round is complete when every player has played its turn
    {
        if (_currentRound == _totalRounds)
            EndGame();

        else
        {
            _currentRound++;
            foreach (PlayerInfo player in _players)
            {
                player.reaminingTurns = 3;
            }
            int aux = Mathf.RoundToInt(UnityEngine.Random.value * 10);
            if (aux % 2 == 0)
                _windSimulation.RoundHasWind();
            else
                _windSimulation.RoundIsQuiet();

            //TODO POPUP nueva Ronda
            StartCoroutine(WaitBetweenRoundChanges()); //NEEDED FOR SMOOTH TRANSITIONS BETWEEN ANIMATIONS
        }
    }

    IEnumerator WaitBetweenRoundChanges()
    {
        yield return new WaitForSeconds(1f);
        _canvasInputArcheryGame.NewRoundTurn(_currentRound);
    }

    void ManagePlayerTurn() //INGAME , manage every player change or turn.A turn is complete when an arrow has been shoot. Player changes if currentplayer has shoot 3 times
    {
        _players[_currentPlayer].reaminingTurns -= 1;
        if (_players[_currentPlayer].reaminingTurns == 0)
        {
            if (_currentPlayer == _players.Count - 1)
                _currentPlayer = 0;
            else _currentPlayer += 1;
            //TODO POPUP nuevo jugador
            Invoke(nameof(DestroyArrows), 1f);
            StartCoroutine(WaitBetweenPlayerChanges()); //NEEDED FOR SMOOTH TRANSITIONS BETWEEN ANIMATIONS
            StartCoroutine(ChangeColorPlayer());
        }
        else
            _arrowManager.SpawnNewArrow(this);
    }

    IEnumerator WaitBetweenPlayerChanges()
    {
        yield return new WaitForSeconds(1f);
        _canvasInputArcheryGame.NewPlayerTurn(_players[_currentPlayer].playerName, _currentPlayer);
    }

    IEnumerator ChangeColorPlayer()
    {
        Color targetColor = _players[_currentPlayer].playerColor;
        float elapsedTime = 0f;
        float totalTime = 20f;
        Material mat = _arrowManager._playerModel.GetComponentInChildren<SkinnedMeshRenderer>().material;
        while (elapsedTime < totalTime)
        {
            mat.color = Color.Lerp(mat.color, targetColor, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

    void ManagePlayerScore(int score) //After an arrow is shoot, update currentplayer score
    {
        _players[_currentPlayer].roundScore[_currentRound - 1] += score;
        _players[_currentPlayer].finalScore += score;
        if (score == 0)             //If arrow has not impacted on the target , destroy it
            _arrowManager.DestroyArrow();
        else
            _arrowManager.FreezeCurrentArrow(); //Else, freeze it for better visuals

        //TODO Cambiar score jugador
        _canvasInputArcheryGame.UpdatePlayerScore(_players[_currentPlayer].roundScore[_currentRound - 1]);
        _maximumScorePlayer = _players[_currentPlayer].finalScore > _players[_maximumScorePlayer].finalScore ? _currentPlayer : _maximumScorePlayer;
    }

    void DestroyArrows() // Destroy every arrow on scenario
    {
        _arrowManager.DestroyCurrentArrows();
    }

    public void ArrowThrowed(int score) //When an arrow has been throwed, manage what happens next
    {
        ManagePlayerScore(score);
        ManagePlayerTurn();
        if (_players[_players.Count - 1].reaminingTurns == 0)
            ManageRounds();
    }


    void SpawnArrow() { _arrowManager.SpawnNewArrow(this); } //After everything needed is managed, spawn an arrow for the currentplayer
    #endregion



    #region START RESET END
    public void GameHasStarted()
    {
        SetEverythingToNewGame();
        FindElementsInScene();
        SetUpOtherSceneGObj();
        ManageRounds();
        _canvasInputArcheryGame.NewPlayerTurn(_players[_currentPlayer].playerName, _currentPlayer);
        _arrowManager._playerModel.GetComponentInChildren<SkinnedMeshRenderer>().material.color = _players[_currentPlayer].playerColor;
    }

    public void ResetGame()
    {
        SetEverythingToNewGame();
    }
    private void EndGame()
    {
        _canvasInputArcheryGame.EndGame();
        GetPlayerWin();
    }

    #endregion
    private void GetPlayerWin()
    {
        _playerHasWin = _players[_maximumScorePlayer].playerName;
    }
}
