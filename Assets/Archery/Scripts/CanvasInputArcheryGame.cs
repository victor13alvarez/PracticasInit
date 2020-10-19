using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasInputArcheryGame : MonoBehaviour
{

    public Text playerNameText;
    public Text playerRoundScoreText;
    public Text currentRoundText;
    const string playerInfo = "CURRENT PLAYER : ";
    const string playerScoreInfo = "PLAYER ROUND SCORE: ";
    const string currentRoundInfo = "CURRENT ROUND : ";
    Transform[] childButtons;

    public GameObject canvasLocateIndicator, canvasDisplayCurrentGameInfo, canvasEndGame, buttons;

    private void Awake()
    {
        ResetCanvas();
    }
    public void ResetCanvas()
    {
        canvasLocateIndicator.SetActive(true);
        canvasDisplayCurrentGameInfo.SetActive(false);
        canvasEndGame.SetActive(false);
        buttons.SetActive(true);

        childButtons = buttons.GetComponentsInChildren<Transform>();
        for (int i = childButtons.Length - 1; i > 0; i--)
            childButtons[i].gameObject.SetActive(false);
    }
    public void ResetGameButton()
    {
        FindObjectOfType<ArcheryGameManager>().ResetGame();
    }

    public void OptionsButton()
    {
        for (int i = childButtons.Length - 1; i > 0; i--)
            childButtons[i].gameObject.SetActive(!childButtons[i].gameObject.activeSelf);
    }

    public void SetGameInfoText(string playerName , int currentScore, int round)
    {
        playerNameText.text = playerInfo + playerName;
        playerRoundScoreText.text = playerScoreInfo + currentScore.ToString();
        currentRoundText.text = currentRoundInfo + round.ToString();
    }

    public void IndicatorPlaced()
    {
        canvasLocateIndicator.SetActive(false);
        canvasDisplayCurrentGameInfo.SetActive(true);
    }

    public void EndGame()
    {
        canvasDisplayCurrentGameInfo.SetActive(false);
        buttons.SetActive(false);
        canvasEndGame.SetActive(true);
    }
}
