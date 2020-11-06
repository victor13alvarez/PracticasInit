using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasInputArcheryGame : MonoBehaviour
{

    [SerializeField] Text playerNameText;
    [SerializeField] Text playerRoundScoreText;
    [SerializeField] Text currentRoundText;
    [SerializeField] GameObject playerPanelAnim;
    [SerializeField] Text playerPanelAnimText;
    [SerializeField] GameObject roundPanelAnim;
    [SerializeField] Text roundPanelAnimText;

    const string playerInfo = "CURRENT PLAYER:\n";
    const string playerScoreInfo = "PLAYER ROUND SCORE:";
    const string currentRoundInfo = "CURRENT ROUND : ";
    const string playerRound = " 'S TURN";
    const string currentRound = "ROUND ";
    Transform[] childButtons;

    Animator c_Animator;

    public Action everythingIsPrepared;

    public GameObject canvasLocateIndicator, canvasDisplayCurrentGameInfo, canvasEndGame, buttons;

    private void Awake()
    {
        c_Animator = GetComponent<Animator>();
        ResetCanvas();
        
    }
    public void ResetCanvas()
    {
        canvasLocateIndicator.SetActive(true);

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

    public void UpdatePlayerScore(int score)
    {
        playerRoundScoreText.text = score.ToString();
    }

    public void EndGame(int totalRounds, PlayerInfo [] players)
    {
        canvasDisplayCurrentGameInfo.SetActive(false);
        canvasEndGame.SetActive(true);
        canvasEndGame.GetComponentInChildren<ScoreGird>().CreateScorePanel(totalRounds, players);
    }
    public void NewPlayerTurn(string playerName)
    {
        playerPanelAnimText.text = playerName.ToUpper() + playerRound;
        playerNameText.text = playerName;
        UpdatePlayerScore(0);
    }
    public void NewRoundTurn(int round)
    {
        roundPanelAnimText.text = currentRound + round;
        currentRoundText.text = round + "";
        c_Animator.SetTrigger("RoundTrigger");
    }


    #region ANIMATIONS
    public void LocateAnimationEnded()
    {
        c_Animator.SetBool("PlacementBool", true);
        canvasLocateIndicator.SetActive(false);
        //canvasDisplayCurrentGameInfo.SetActive(true);
    }
    public void RoundAnimationEnded()
    {
        c_Animator.SetTrigger("PlayerTrigger");
    }

    public void PlayerAnimationEnded()
    {
        c_Animator.SetTrigger("InfoInGameTrigger");
    }
    public void InfoInGameAnimationEnded()
    {
        everythingIsPrepared.Invoke();
    }

    #endregion
}
