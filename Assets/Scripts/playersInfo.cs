using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playersInfo : MonoBehaviour
{
    public static int numberPlayers = 1;
    public static string namePlayer1;
    public static string namePlayer2;
    public static string namePlayer3;
    public static string namePlayer4;
    public Text player1;
    public Text player2;
    public Text player3;
    public Text player4;
    public static int scorePlayer1 = 0;
    public static int scorePlayer2 = 0;
    public static int scorePlayer3 = 0;
    public static int scorePlayer4 = 0;
    public static int hole1Player1 = 0;
    public static int hole1Player2 = 0;
    public static int hole2Player1 = 0;
    public static int hole2Player2 = 0;
    public static int hole3Player1 = 0;
    public static int hole3Player2 = 0;
    public static int hole1Player3 = 0;
    public static int hole1Player4 = 0;
    public static int hole2Player3 = 0;
    public static int hole2Player4 = 0;
    public static int hole3Player3 = 0;
    public static int hole3Player4 = 0;
    public static int finalScorePlayer1 = 0;
    public static int finalScorePlayer2 = 0;
    public static int finalScorePlayer3 = 0;
    public static int finalScorePlayer4 = 0;

    public GameObject[] players;

    public static int ballChosenPlayer1;
    public static int ballChosenPlayer2;
    public static int ballChosenPlayer3;
    public static int ballChosenPlayer4;
    public Text numberPlayersText;
    public Slider player1Slider;
    public Slider player2Slider;
    public Slider player3Slider;
    public Slider player4Slider;

    private void Start()
    {
        numberPlayers = 1;
    }
    // Update is called once per frame
    void Update()
    {
        numberPlayersText.text = numberPlayers.ToString();
        namePlayer1 = player1.text;
        namePlayer2 = player2.text;
        namePlayer3 = player3.text;
        namePlayer4 = player4.text;
        if (player1Slider != null)
        {
            ballChosenPlayer1 = (int)player1Slider.value;
        }

        if (player2Slider != null)
        {
            ballChosenPlayer2 = (int)player2Slider.value;
        }

        if (player3Slider != null)
        {
            ballChosenPlayer3 = (int)player3Slider.value;
        }

        if (player4Slider != null)
        {
            ballChosenPlayer4 = (int)player4Slider.value;
        }

    }

    public void AddPlayer()
    {
        if(numberPlayers < 4)
        {
            numberPlayers++;
            players[numberPlayers - 1].SetActive(true);
        }
    }

    public void RemovePlayer()
    {
        if(numberPlayers > 1)
        {
            numberPlayers--;
            players[numberPlayers].SetActive(false);
        }
    }

}
