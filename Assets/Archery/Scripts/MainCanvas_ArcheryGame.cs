using System;
using UnityEngine;
using TMPro;
public class MainCanvas_ArcheryGame : MonoBehaviour
{

    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerRoundScoreText;
    public TextMeshProUGUI currentRoundText;

    public TextMeshProUGUI playerPanelAnimText;
    public TextMeshProUGUI roundPanelAnimText;

    const string playerRound = "'S\nTURN";
    const string currentRound = "ROUND ";
    Transform[] childButtons;

    Animator _cAnim;
    SceneChange _sceneChange;
    string _sceneToChange;

    public Action everythingIsPrepared;

    public GameObject canvasLocateIndicator, canvasDisplayCurrentGameInfo, canvasEndGame, buttons;

    private void Awake()
    {
        _cAnim = GetComponent<Animator>();
        _sceneChange = GetComponent<SceneChange>();
        ResetCanvas();
    }

    private void Start()
    {
        _cAnim.SetTrigger("GameStarted");
    }
    public void ResetCanvas()
    {
        canvasLocateIndicator.SetActive(true);
        buttons.SetActive(true);

        childButtons = buttons.GetComponentsInChildren<Transform>();
        for (int i = childButtons.Length - 1; i > 0; i--)
            childButtons[i].gameObject.SetActive(false);
    }
    public void ResetGameButton()
    {
        _cAnim.SetTrigger("GameEnded");
        _sceneToChange = "ArcheryGame";
        FindObjectOfType<ArcheryGameManager>().ResetGame();
        canvasDisplayCurrentGameInfo.SetActive(false);
    }

    public void MainMenuButton()
    {
        _cAnim.SetTrigger("GameEnded");
        _sceneToChange = "MainMenu";
        FindObjectOfType<ArcheryGameManager>().ResetGame();
        canvasDisplayCurrentGameInfo.SetActive(false);
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

    public void EndGame()
    {
        _cAnim.SetTrigger("GameEnded");
        _sceneToChange = "ArcheryGame_GameOver";
        canvasDisplayCurrentGameInfo.SetActive(false);
    }

    public void NewPlayerTurn(string playerName, int currentPlayer)
    {
        playerPanelAnimText.text = playerName.ToUpper() + playerRound;
        playerNameText.text = playerName;
        if (currentPlayer != 0)
            RoundAnimationEnded();
        UpdatePlayerScore(0);
    }

    public void NewRoundTurn(int round)
    {
        roundPanelAnimText.text = currentRound + round;
        currentRoundText.text = round + "";
        StartRound();
    }


    #region ANIMATION FLOW

    private void StartRound()
    {
        _cAnim.SetTrigger("RoundTrigger");
    }
    public void LocateAnimationEnded()
    {
        _cAnim.SetBool("PlacementBool", true);
        canvasLocateIndicator.SetActive(false);
    }
    public void RoundAnimationEnded()
    {
        _cAnim.SetTrigger("PlayerTrigger");
    }

    public void PlayerAnimationEnded()
    {
        _cAnim.SetTrigger("InfoInGameTrigger");
        _cAnim.SetTrigger("ShotTrigger");
    }
    public void InfoInGameAnimationEnded()
    {
        everythingIsPrepared.Invoke();
    }
    public void GameHasEnded()
    {
        _sceneChange.changeScene(_sceneToChange);
    }

    #endregion
}
