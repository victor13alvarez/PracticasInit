using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGameController : MonoBehaviour
{
    public GameObject[] decklist;
    public Transform[] spanPositions;
    GameObject tempGO;
    public  int cardsShown;
    public  string cardText;
    public  bool failedAttempt;
    public  bool wonAttempt;
    public  bool turnAvailable;
    public  int totalModelsShown;
    public GameObject canvasWin;
    public GameObject turnCanvas;
    public Text canvasTurn;
    public Text textWin;
    public int player1Turn;
    public Material materialBlack;
    string player1;
    string player2;
    string player3;
    string player4;
    public GameObject explosion;
    public Text textBest;
    public float bestPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player1 = playersInfo.namePlayer1;
        player2 = playersInfo.namePlayer2;
        player3 = playersInfo.namePlayer3;
        player4 = playersInfo.namePlayer4;
        SpawnCards();
        cardsShown = 0;
        failedAttempt = false;
        wonAttempt = false;
        turnAvailable = true;
        canvasTurn.text = "Turn of " + player1;
        player1Turn = 1;
        turnCanvas.SetActive(true);
        bestPlayer = 0;
        textBest.text = "Max streak: 0";
    }

    // Update is called once per frame
    void Update()
    {
        if (failedAttempt)
        {
            turnAvailable = false;
            StartCoroutine(timePause(0.8f));
            
        }

        if(totalModelsShown == decklist.Length)
        {
            StartCoroutine(win());
        }

        else
        {
            if (player1Turn == 1)
            {
                canvasTurn.text = "Turn of " + player1;
            }

            else if (player1Turn == 2)
            {
                canvasTurn.text = "Turn of " + player2;
            }

            else if (player1Turn == 3)
            {
                canvasTurn.text = "Turn of " + player3;
            }

            else if (player1Turn == 4)
            {
                canvasTurn.text = "Turn of " + player4;
            }
        }

        
    }

    IEnumerator win()
    {
        yield return new WaitForSeconds(0.25f);
        turnCanvas.SetActive(false);
        if (player1Turn == 1)
        {
            textWin.text = "The winner is " + player1;
        }
        else if (player1Turn == 2)
        {
            textWin.text = "The winner is " + player2;
        }

        else if (player1Turn == 3)
        {
            textWin.text = "The winner is " + player3;
        }

        else if (player1Turn == 4)
        {
            textWin.text = "The winner is " + player4;
        }

        canvasWin.SetActive(true);
    }

    IEnumerator timePause(float n)
    {
        yield return new WaitForSeconds(n);
        TurnOut();
        
    }

    IEnumerator otherPause()
    {
        yield return new WaitForSeconds(0.25f);
        
        turnAvailable = true;

    }

    void SpawnCards()
    {
        Shuffle();
        for (int i = 0; i < decklist.Length; i++)
        {
            GameObject card = Instantiate(decklist[i], spanPositions[i]);
        }
    }

    public void Shuffle()
    {
        for (int i = 0; i < decklist.Length; i++)
        {
            
            int rnd = Random.Range(0, decklist.Length);
            tempGO = decklist[rnd];
            decklist[rnd] = decklist[i];
            decklist[i] = tempGO;
        }
    }

    public void TurnOut()
    {
        if (bestPlayer < totalModelsShown)
        {
            textBest.text = "Max streak: " + (totalModelsShown - 2);
        }
        totalModelsShown = 0;
        GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].gameObject.GetComponent<CardController>().imShown)
            {
                Instantiate(explosion, cards[i].gameObject.transform.position + new Vector3(0, explosion.transform.position.y, 0), explosion.transform.rotation);
            }
            cards[i].gameObject.GetComponent<CardController>().imShown = false;
            cards[i].gameObject.GetComponent<CardController>().myShape.SetActive(false);
            cards[i].gameObject.GetComponent<MeshRenderer>().material = materialBlack;
        }
        cardText = "";
        failedAttempt = false;
        StartCoroutine(otherPause());



    }
}
