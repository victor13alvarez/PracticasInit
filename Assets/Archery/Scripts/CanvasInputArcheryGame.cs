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

    const string playerInfo = "CURRENT PLAYER : ";
    const string playerScoreInfo = "PLAYER ROUND SCORE: ";
    const string currentRoundInfo = "CURRENT ROUND : ";
    const string playerRound = " IS NOW PLAYING";
    const string currentRound = "ROUND ";
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
        playerPanelAnim.SetActive(false);
        roundPanelAnim.SetActive(false);

        childButtons = buttons.GetComponentsInChildren<Transform>();
        for (int i = childButtons.Length - 1; i > 0; i--)
            childButtons[i].gameObject.SetActive(false);

        playerPanelAnim.transform.localScale = Vector3.zero;
        roundPanelAnim.transform.localScale = Vector3.zero;
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

    public void EndGame(int totalRounds, PlayerInfo [] players)
    {
        canvasDisplayCurrentGameInfo.SetActive(false);
        canvasEndGame.SetActive(true);
        canvasEndGame.GetComponentInChildren<ScoreGird>().CreateScorePanel(totalRounds, players);
    }
    public void NewPlayerTurn(string text)
    {
        playerPanelAnimText.text = text + playerRound;
        StartCoroutine(AnimatePanel(playerPanelAnim));
    }
    public void NewRound(string text)
    {
        roundPanelAnimText.text = currentRound + text;
        StartCoroutine(AnimatePanel(roundPanelAnim));
    }
    IEnumerator AnimatePanel(GameObject panel)
    {
        float elapsedTime = 0;
        panel.SetActive(true);
        Vector3 scale = panel.transform.localScale;

        while (elapsedTime < 2.5f)
        {
            scale = Vector3.Lerp(scale, Vector3.one, (elapsedTime / 2.5f));
            panel.transform.localScale = scale;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        panel.transform.localScale = Vector3.zero;
        panel.SetActive(false);
    }
}
