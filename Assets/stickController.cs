using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class stickController : MonoBehaviour
{
    
    float positionY;
    GameObject arCamera;
    RaycastHit hit;
    public LayerMask layermask;
    GameObject ball1;
    GameObject ball2;
    GameObject ball3;
    GameObject ball4;
    public Material materialBase1;
    public Material materialBody1;
    public Material materialUp1;
    public Material materialBase2;
    public Material materialBody2;
    public Material materialUp2;
    public Material materialBase3;
    public Material materialBody3;
    public Material materialUp3;
    public Material materialBase4;
    public Material materialBody4;
    public Material materialUp4;
    public Material materialTransparent;
    Material[] stickComponents;
    string player1;
    string player2;
    string player3;
    string player4;
    public int playerTurn = 1;
    public Text canvasTurn;
    public static bool ballHitted;
    Material[] myMaterialsTransparent;
    Material[] myMaterialsOpaque1;
    Material[] myMaterialsOpaque2;
    Material[] myMaterialsOpaque3;
    Material[] myMaterialsOpaque4;
    LineRenderer ball1line;
    LineRenderer ball2line;
    LineRenderer ball3line;
    LineRenderer ball4line;
    public static bool lineRenderer;
    public Image image;
    float hitForce;
    public GameObject canvasGetClose;
    bool canShoot;
    public Button hitButton;
    public Text scorePlayer;
    public bool player1end;
    public bool player2end;
    public bool player3end;
    public bool player4end;
    GameObject parent;
    public GameObject canvasWin;
    public Text winText;
    public GameObject stickObject;
    Animator stickAnim;
    bool makingAnim;
    bool acercarElPalo;
    Vector3 End_Pos;
    Vector3 Start_Pos;
    float fraction_of_the_way_there = 0;
    public GameObject yellowStars;
    public GameObject redStars;
    public GameObject greenStars;
    public GameObject blueStars;
    public Text finalScore1;
    public Text finalScore2;
    public Text finalScore3;
    public Text finalScore4;




    // Start is called before the first frame update
    void Start()
    {
        arCamera = GameObject.FindGameObjectWithTag("MainCamera");
        ball1 = GameObject.FindGameObjectWithTag("Ball1");
        ball2 = GameObject.FindGameObjectWithTag("Ball2");
        ball3 = GameObject.FindGameObjectWithTag("Ball3");
        ball4 = GameObject.FindGameObjectWithTag("Ball4");
        positionY = this.transform.position.y;
        player1 = playersInfo.namePlayer1;
        player2 = playersInfo.namePlayer2;
        player3 = playersInfo.namePlayer3;
        player4 = playersInfo.namePlayer4;
        player1end = player2end = player3end = player4end = false;
        ball1line = ball1.GetComponent<LineRenderer>();
        ball2line = ball2.GetComponent<LineRenderer>();
        ball3line = ball3.GetComponent<LineRenderer>();
        ball4line = ball4.GetComponent<LineRenderer>();
        ball1line.SetPosition(0, ball1.transform.position);
        ball2line.SetPosition(0, ball2.transform.position);
        ball3line.SetPosition(0, ball3.transform.position);
        ball4line.SetPosition(0, ball4.transform.position);
        ball1line.enabled = false;
        ball2line.enabled = false;
        ball3line.enabled = false;
        ball4line.enabled = false;
        ball1.SetActive(true);
        ball2.SetActive(false);
        ball3.SetActive(false);
        ball4.SetActive(false);
        stickAnim = this.GetComponent<Animator>();
        playerTurn = 1;
        lineRenderer = true;
        myMaterialsTransparent = stickObject.gameObject.GetComponent<Renderer>().materials;
        myMaterialsTransparent[0] = materialTransparent;
        myMaterialsTransparent[1] = materialTransparent;
        myMaterialsTransparent[2] = materialTransparent;
        myMaterialsOpaque1 = stickObject.gameObject.GetComponent<Renderer>().materials;
        myMaterialsOpaque1[0] = materialBase1;
        myMaterialsOpaque1[1] = materialBody1;
        myMaterialsOpaque1[2] = materialUp1;
        myMaterialsOpaque2 = stickObject.gameObject.GetComponent<Renderer>().materials;
        myMaterialsOpaque2[0] = materialBase2;
        myMaterialsOpaque2[1] = materialBody2;
        myMaterialsOpaque2[2] = materialUp2;
        myMaterialsOpaque3 = stickObject.gameObject.GetComponent<Renderer>().materials;
        myMaterialsOpaque3[0] = materialBase3;
        myMaterialsOpaque3[1] = materialBody3;
        myMaterialsOpaque3[2] = materialUp3;
        myMaterialsOpaque4 = stickObject.gameObject.GetComponent<Renderer>().materials;
        myMaterialsOpaque4[0] = materialBase4;
        myMaterialsOpaque4[1] = materialBody4;
        myMaterialsOpaque4[2] = materialUp4;
        playersInfo.scorePlayer1 = 0;
        playersInfo.scorePlayer2 = 0;
        playersInfo.scorePlayer3 = 0;
        playersInfo.scorePlayer4 = 0;
        scorePlayer.text = "Strokes" + "\n" + playersInfo.scorePlayer1.ToString();
        parent = this.transform.parent.gameObject;
        stickObject.GetComponent<Collider>().enabled = false;



    }

    // Update is called once per frame
    void Update()
    {








        if(allEnd())
        {
            if(parent.tag == "Easy")
            {
                playersInfo.hole1Player1 = playersInfo.finalScorePlayer1;
                playersInfo.hole1Player2 = playersInfo.finalScorePlayer2;
                playersInfo.hole1Player3 = playersInfo.finalScorePlayer3;
                playersInfo.hole1Player4 = playersInfo.finalScorePlayer4;
                GameObject.FindObjectOfType<levelsController>().easyCompleted();
            }

            else if (parent.tag == "Medium")
            {
                playersInfo.hole2Player1 = playersInfo.finalScorePlayer1 - playersInfo.hole1Player1;
                playersInfo.hole2Player2 = playersInfo.finalScorePlayer2 - playersInfo.hole1Player2;
                playersInfo.hole2Player3 = playersInfo.finalScorePlayer3 - playersInfo.hole1Player3;
                playersInfo.hole2Player4 = playersInfo.finalScorePlayer4 - playersInfo.hole1Player4;
                GameObject.FindObjectOfType<levelsController>().mediumCompleted();
            }

            else if (parent.tag == "Hard")
            {
                playersInfo.hole3Player1 = playersInfo.finalScorePlayer1 - (playersInfo.hole2Player1 + playersInfo.hole1Player1);
                playersInfo.hole3Player2 = playersInfo.finalScorePlayer2 - (playersInfo.hole2Player2 + playersInfo.hole1Player2);
                playersInfo.hole3Player3 = playersInfo.finalScorePlayer3 - (playersInfo.hole2Player3 + playersInfo.hole1Player3);
                playersInfo.hole3Player4 = playersInfo.finalScorePlayer4 - (playersInfo.hole2Player4 + playersInfo.hole1Player4);
                canvasWin.SetActive(true);
                if (playersInfo.numberPlayers == 1)
                {
                    canvasWin.SetActive(true);
                    finalScore1.text = playersInfo.namePlayer1 + "'s score:\n" +
                        "Hole 1 ---> " + playersInfo.hole1Player1 + "\n" +
                        "Hole 2 ---> " + playersInfo.hole2Player1 + "\n" +
                        "Hole 3 ---> " + playersInfo.hole3Player1 + "\n" +
                        "Total strokes ---> " + playersInfo.finalScorePlayer1;
                    winText.text = playersInfo.namePlayer1 + " " + "is the winner";
                    finalScore2.text = finalScore3.text = finalScore4.text = "";
                }

                else if (playersInfo.numberPlayers == 2)
                {
                    string[] name2players = new string[] { playersInfo.namePlayer1, playersInfo.namePlayer2 };
                    GameObject[] stars2players = new GameObject[] { yellowStars, redStars };
                    int[] score2players = new int[] { playersInfo.finalScorePlayer1, playersInfo.finalScorePlayer2 };
                    int bestScore = score2players.Min();
                    int winner = score2players.ToList().IndexOf(bestScore);

                    finalScore1.text = playersInfo.namePlayer1 + "'s score:\n" +
                    "Hole 1 ---> " + playersInfo.hole1Player1 + "\n" +
                    "Hole 2 ---> " + playersInfo.hole2Player1 + "\n" +
                    "Hole 3 ---> " + playersInfo.hole3Player1 + "\n" +
                    "Total strokes ---> " + playersInfo.finalScorePlayer1;

                    finalScore2.text = playersInfo.namePlayer2 + "'s score:\n" +
                        "Hole 1 ---> " + playersInfo.hole1Player2 + "\n" +
                        "Hole 2 ---> " + playersInfo.hole2Player2 + "\n" +
                        "Hole 3 ---> " + playersInfo.hole3Player2 + "\n" +
                        "Total strokes ---> " + playersInfo.finalScorePlayer2;

                    if (playersInfo.finalScorePlayer1 == playersInfo.finalScorePlayer2)
                    {
                        winText.text = "TIE";
                    }

                    else
                    {
                        winText.text = name2players[winner] + " " + "is the winner";
                        stars2players[bestScore].SetActive(true);
                    }

                    finalScore3.text = finalScore4.text = "";
                }

                else if (playersInfo.numberPlayers == 3)
                {
                    string[] name3players = new string[] { playersInfo.namePlayer1, playersInfo.namePlayer2, playersInfo.namePlayer3 };
                    GameObject[] stars3players = new GameObject[] { yellowStars, redStars, greenStars };
                    int[] score3players = new int[] { playersInfo.finalScorePlayer1, playersInfo.finalScorePlayer2, playersInfo.finalScorePlayer3 };
                    int bestScore = score3players.Min();
                    int winner = score3players.ToList().IndexOf(bestScore);

                    finalScore1.text = playersInfo.namePlayer1 + "'s score:\n" +
                    "Hole 1 ---> " + playersInfo.hole1Player1 + "\n" +
                    "Hole 2 ---> " + playersInfo.hole2Player1 + "\n" +
                    "Hole 3 ---> " + playersInfo.hole3Player1 + "\n" +
                    "Total strokes ---> " + playersInfo.finalScorePlayer1;

                    finalScore2.text = playersInfo.namePlayer2 + "'s score:\n" +
                        "Hole 1 ---> " + playersInfo.hole1Player2 + "\n" +
                        "Hole 2 ---> " + playersInfo.hole2Player2 + "\n" +
                        "Hole 3 ---> " + playersInfo.hole3Player2 + "\n" +
                        "Total strokes ---> " + playersInfo.finalScorePlayer2;

                    finalScore3.text = playersInfo.namePlayer3 + "'s score:\n" +
                        "Hole 1 ---> " + playersInfo.hole1Player3 + "\n" +
                        "Hole 2 ---> " + playersInfo.hole2Player3 + "\n" +
                        "Hole 3 ---> " + playersInfo.hole3Player3 + "\n" +
                        "Total strokes ---> " + playersInfo.finalScorePlayer3;

                    if (playersInfo.finalScorePlayer1 == playersInfo.finalScorePlayer2  && playersInfo.finalScorePlayer2 == playersInfo.finalScorePlayer3)
                    {
                        winText.text = "TIE";
                    }

                    else
                    {
                        winText.text = name3players[winner] + " " + "is the winner";
                        stars3players[bestScore].SetActive(true);
                    }

                    finalScore4.text = "";
                }

                else
                {
                    string[] name4players = new string[] { playersInfo.namePlayer1, playersInfo.namePlayer2, playersInfo.namePlayer3, playersInfo.namePlayer4 };
                    GameObject[] stars4players = new GameObject[] { yellowStars, redStars, greenStars, blueStars };
                    int[] score4players = new int[] { playersInfo.finalScorePlayer1, playersInfo.finalScorePlayer2, playersInfo.finalScorePlayer3, playersInfo.finalScorePlayer4 };
                    int bestScore = score4players.Min();
                    int winner = score4players.ToList().IndexOf(bestScore);

                    finalScore1.text = playersInfo.namePlayer1 + "'s score:\n" +
                    "Hole 1 ---> " + playersInfo.hole1Player1 + "\n" +
                    "Hole 2 ---> " + playersInfo.hole2Player1 + "\n" +
                    "Hole 3 ---> " + playersInfo.hole3Player1 + "\n" +
                    "Total strokes ---> " + playersInfo.finalScorePlayer1;

                    finalScore2.text = playersInfo.namePlayer2 + "'s score:\n" +
                        "Hole 1 ---> " + playersInfo.hole1Player2 + "\n" +
                        "Hole 2 ---> " + playersInfo.hole2Player2 + "\n" +
                        "Hole 3 ---> " + playersInfo.hole3Player2 + "\n" +
                        "Total strokes ---> " + playersInfo.finalScorePlayer2;

                    finalScore3.text = playersInfo.namePlayer3 + "'s score:\n" +
                        "Hole 1 ---> " + playersInfo.hole1Player3 + "\n" +
                        "Hole 2 ---> " + playersInfo.hole2Player3 + "\n" +
                        "Hole 3 ---> " + playersInfo.hole3Player3 + "\n" +
                        "Total strokes ---> " + playersInfo.finalScorePlayer3;

                    finalScore4.text = playersInfo.namePlayer4 + "'s score:\n" +
                        "Hole 1 ---> " + playersInfo.hole1Player4 + "\n" +
                        "Hole 2 ---> " + playersInfo.hole2Player4 + "\n" +
                        "Hole 3 ---> " + playersInfo.hole3Player4 + "\n" +
                        "Total strokes ---> " + playersInfo.finalScorePlayer4;

                    if (playersInfo.finalScorePlayer1 == playersInfo.finalScorePlayer2 && playersInfo.finalScorePlayer2 == playersInfo.finalScorePlayer3 && playersInfo.finalScorePlayer3 == playersInfo.finalScorePlayer4)
                    {
                        winText.text = "TIE";
                    }

                    else
                    {
                        winText.text = name4players[winner] + " " + "is the winner";
                        stars4players[winner].SetActive(true);
                    }

                }

                GameObject.FindObjectOfType<levelsController>().hardCompleted();
            }

        }













        if (acercarElPalo)
        {
            if(playerTurn == 1)
            {
                if (Vector3.Distance(ball1.transform.position, this.transform.position) > 0.05f)
                {
                    fraction_of_the_way_there += 0.05f; //Adjust this for how fast you want it to be.
                    transform.position = Vector3.Lerp(Start_Pos, End_Pos, fraction_of_the_way_there);
                }


                else
                {
                    stickAnim.SetBool("isHitted", true);
                    acercarElPalo = false;
                }
            }

            if (playerTurn == 2)
            {
                if (Vector3.Distance(ball2.transform.position, this.transform.position) > 0.05f)
                {
                    fraction_of_the_way_there += 0.05f; //Adjust this for how fast you want it to be.
                    transform.position = Vector3.Lerp(Start_Pos, End_Pos, fraction_of_the_way_there);
                }


                else
                {
                    stickAnim.SetBool("isHitted", true);
                    acercarElPalo = false;
                }
            }

            if (playerTurn == 3)
            {
                if (Vector3.Distance(ball3.transform.position, this.transform.position) > 0.05f)
                {
                    fraction_of_the_way_there += 0.05f; //Adjust this for how fast you want it to be.
                    transform.position = Vector3.Lerp(Start_Pos, End_Pos, fraction_of_the_way_there);
                }


                else
                {
                    stickAnim.SetBool("isHitted", true);
                    acercarElPalo = false;
                }
            }

            if (playerTurn == 4)
            {
                if (Vector3.Distance(ball4.transform.position, this.transform.position) > 0.05f)
                {
                    fraction_of_the_way_there += 0.05f; //Adjust this for how fast you want it to be.
                    transform.position = Vector3.Lerp(Start_Pos, End_Pos, fraction_of_the_way_there);
                }


                else
                {
                    stickAnim.SetBool("isHitted", true);
                    acercarElPalo = false;
                }
            }
        }














        if (playerTurn == 1)
        {
            if (!player1end)
            {
                canvasTurn.text = "Turn of " + player1;
                scorePlayer.text = "Strokes" + "\n" + playersInfo.scorePlayer1.ToString();
                ball1.SetActive(true);
                ball1.GetComponent<MeshRenderer>().enabled = true;
                if (ball1.GetComponent<Rigidbody>().velocity.magnitude == 0 && !makingAnim)
                {

                    if (ballHitted)
                    {
                        canShoot = false;
                        lineRenderer = true;
                        ballHitted = false;
                        ball1.SetActive(false);
                        playerTurn = (playerTurn < playersInfo.numberPlayers) ? 2 : 1;
                    }

                    else
                    {
                        canShoot = false;
                        RaycastHit hit;

                        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit, Mathf.Infinity, layermask))
                        {
                            canvasGetClose.SetActive(false);
                            if (hit.collider.tag == "GolfTerrain" || hit.collider.tag == "Arena")
                            {
                                this.transform.position = hit.point;
                                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.053067f, this.transform.position.z);
                                if (Vector3.Distance(ball1.transform.position, this.transform.position) <= 0.2 && Vector3.Distance(ball1.transform.position, this.transform.position) >= 0.04)
                                {
                                    float distance = Vector3.Distance(ball1.transform.position, this.transform.position);
                                    hitForce = distance * 50f;
                                    stickAnim.SetFloat("distance", distance);
                                    image.fillAmount = distance * 5f;
                                    ball1line.SetPosition(0, ball1.transform.position);
                                    ball1line.SetPosition(1, this.transform.position);

                                    if (lineRenderer)
                                    {
                                        ball1line.enabled = true;
                                        canShoot = true;
                                    }


                                    stickMaterial(false);
                                }

                                else
                                {
                                    canShoot = false;
                                    ball1line.enabled = false;
                                    hitForce = 0f;
                                    image.fillAmount = 0f;
                                    stickMaterial(true);
                                }
                            }

                            else if (hit.collider.tag == "Obstacle")
                            {
                                canShoot = false;
                                ball1line.enabled = false;
                                this.transform.position = hit.point;
                                this.transform.position = new Vector3(this.transform.position.x, positionY, this.transform.position.z);
                                stickMaterial(true);
                            }

                            Vector3 direction = (ball1.transform.position - this.transform.position).normalized;
                            Quaternion lookRotation = Quaternion.LookRotation(direction);
                            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, 1);
                            this.transform.rotation = new Quaternion(0, this.transform.rotation.y, 0, this.transform.rotation.w);
                        }

                        else
                        {
                            stickAnim.SetFloat("distance", 0f);
                            canShoot = false;
                            if (!PauseMenu.isPaused)
                            {
                                canvasGetClose.SetActive(true);
                            }
                            
                            ball1line.enabled = false;
                        }


                    }
                }
            }

            else
            {
                playerTurn = (playerTurn < playersInfo.numberPlayers) ? 2 : 1;
            }
            
        }

        else if (playerTurn == 2)
        {
            if (!player2end)
            {
                canvasTurn.text = "Turn of " + player2;
                scorePlayer.text = "Strokes" + "\n" + playersInfo.scorePlayer2.ToString();
                ball2.SetActive(true);
                ball2.GetComponent<MeshRenderer>().enabled = true;
                if (ball2.GetComponent<Rigidbody>().velocity.magnitude == 0 && !makingAnim)
                {
                    if (ballHitted)
                    {
                        canShoot = false;
                        lineRenderer = true;
                        ballHitted = false;
                        ball2.SetActive(false);
                        playerTurn = (playerTurn < playersInfo.numberPlayers) ? 3 : 1;
                    }

                    else
                    {
                        canShoot = false;
                        RaycastHit hit;

                        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit, Mathf.Infinity, layermask))
                        {
                            canvasGetClose.SetActive(false);
                            if (hit.collider.tag == "GolfTerrain" || hit.collider.tag == "Arena")
                            {
                                this.transform.position = hit.point;
                                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.053067f, this.transform.position.z);
                                if (Vector3.Distance(ball2.transform.position, this.transform.position) <= 0.2 && Vector3.Distance(ball2.transform.position, this.transform.position) >= 0.04)
                                {
                                    float distance = Vector3.Distance(ball2.transform.position, this.transform.position);
                                    hitForce = distance * 50f;
                                    stickAnim.SetFloat("distance", distance);
                                    image.fillAmount = distance * 5f;
                                    ball2line.SetPosition(0, ball2.transform.position);
                                    ball2line.SetPosition(1, this.transform.position);

                                    if (lineRenderer)
                                    {
                                        ball2line.enabled = true;
                                        canShoot = true;
                                    }


                                    stickMaterial(false);
                                }

                                else
                                {
                                    stickAnim.SetFloat("distance", 0f);
                                    canShoot = false;
                                    ball2line.enabled = false;
                                    hitForce = 0f;
                                    image.fillAmount = 0f;
                                    stickMaterial(true);
                                }
                            }

                            else if (hit.collider.tag == "Obstacle")
                            {
                                canShoot = false;
                                ball2line.enabled = false;
                                this.transform.position = hit.point;
                                this.transform.position = new Vector3(this.transform.position.x, positionY, this.transform.position.z);
                                stickMaterial(true);
                            }

                            Vector3 direction = (ball2.transform.position - this.transform.position).normalized;
                            Quaternion lookRotation = Quaternion.LookRotation(direction);
                            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, 1);
                            this.transform.rotation = new Quaternion(0, this.transform.rotation.y, 0, this.transform.rotation.w);
                        }

                        else
                        {
                            canShoot = false;
                            if (!PauseMenu.isPaused)
                            {
                                canvasGetClose.SetActive(true);
                            }
                            ball2line.enabled = false;
                        }


                    }
                }
            }

            else
            {
                playerTurn = (playerTurn < playersInfo.numberPlayers) ? 3 : 1;
            }
        }

        else if (playerTurn == 3)
        {
            if (!player3end)
            {
                canvasTurn.text = "Turn of " + player3;
                scorePlayer.text = "Strokes" + "\n" + playersInfo.scorePlayer3.ToString();
                ball3.SetActive(true);
                ball3.GetComponent<MeshRenderer>().enabled = true;
                if (ball3.GetComponent<Rigidbody>().velocity.magnitude == 0 && !makingAnim)
                {

                    if (ballHitted)
                    {
                        canShoot = false;
                        lineRenderer = true;
                        ballHitted = false;
                        ball3.SetActive(false);
                        playerTurn = (playerTurn < playersInfo.numberPlayers) ? 4 : 1;
                        Debug.Log("Ahora le toca a " + playerTurn);
                    }

                    else
                    {
                        canShoot = false;
                        RaycastHit hit;

                        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit, Mathf.Infinity, layermask))
                        {
                            canvasGetClose.SetActive(false);
                            if (hit.collider.tag == "GolfTerrain" || hit.collider.tag == "Arena")
                            {
                                this.transform.position = hit.point;
                                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.053067f, this.transform.position.z);
                                if (Vector3.Distance(ball3.transform.position, this.transform.position) <= 0.2 && Vector3.Distance(ball3.transform.position, this.transform.position) >= 0.04)
                                {
                                    float distance = Vector3.Distance(ball3.transform.position, this.transform.position);
                                    hitForce = distance * 50f;
                                    stickAnim.SetFloat("distance", distance);
                                    image.fillAmount = distance * 5f;
                                    ball3line.SetPosition(0, ball3.transform.position);
                                    ball3line.SetPosition(1, this.transform.position);

                                    if (lineRenderer)
                                    {
                                        ball3line.enabled = true;
                                        canShoot = true;
                                    }


                                    stickMaterial(false);
                                }

                                else
                                {
                                    stickAnim.SetFloat("distance", 0f);
                                    canShoot = false;
                                    ball3line.enabled = false;
                                    hitForce = 0f;
                                    image.fillAmount = 0f;
                                    stickMaterial(true);
                                }
                            }

                            else if (hit.collider.tag == "Obstacle")
                            {
                                canShoot = false;
                                ball3line.enabled = false;
                                this.transform.position = hit.point;
                                this.transform.position = new Vector3(this.transform.position.x, positionY, this.transform.position.z);
                                stickMaterial(true);
                            }

                            Vector3 direction = (ball3.transform.position - this.transform.position).normalized;
                            Quaternion lookRotation = Quaternion.LookRotation(direction);
                            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, 1);
                            this.transform.rotation = new Quaternion(0, this.transform.rotation.y, 0, this.transform.rotation.w);
                        }

                        else
                        {
                            canShoot = false;
                            if (!PauseMenu.isPaused)
                            {
                                canvasGetClose.SetActive(true);
                            }
                            ball3line.enabled = false;
                        }


                    }
                }
            }

            else
            {
                playerTurn = (playerTurn < playersInfo.numberPlayers) ? 4 : 1;
            }
        }

        else if (playerTurn == 4)
        {
            if (!player4end)
            {
                canvasTurn.text = "Turn of " + player4;
                scorePlayer.text = "Strokes" + "\n" + playersInfo.scorePlayer4.ToString();
                ball4.SetActive(true);
                ball4.GetComponent<MeshRenderer>().enabled = true;
                if (ball4.GetComponent<Rigidbody>().velocity.magnitude == 0 && !makingAnim)
                {

                    if (ballHitted)
                    {
                        canShoot = false;
                        lineRenderer = true;
                        ballHitted = false;
                        ball4.SetActive(false);
                        playerTurn = 1;
                    }

                    else
                    {
                        canShoot = false;
                        RaycastHit hit;

                        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit, Mathf.Infinity, layermask))
                        {
                            canvasGetClose.SetActive(false);
                            if (hit.collider.tag == "GolfTerrain" || hit.collider.tag == "Arena")
                            {
                                this.transform.position = hit.point;
                                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.053067f, this.transform.position.z);
                                if (Vector3.Distance(ball4.transform.position, this.transform.position) <= 0.2 && Vector3.Distance(ball4.transform.position, this.transform.position) >= 0.04)
                                {
                                    float distance = Vector3.Distance(ball4.transform.position, this.transform.position);
                                    hitForce = distance * 50f;
                                    stickAnim.SetFloat("distance", distance);
                                    image.fillAmount = distance * 5f;
                                    ball4line.SetPosition(0, ball4.transform.position);
                                    ball4line.SetPosition(1, this.transform.position);

                                    if (lineRenderer)
                                    {
                                        ball4line.enabled = true;
                                        canShoot = true;
                                    }


                                    stickMaterial(false);
                                }

                                else
                                {
                                    stickAnim.SetFloat("distance", 0f);
                                    canShoot = false;
                                    ball4line.enabled = false;
                                    hitForce = 0f;
                                    image.fillAmount = 0f;
                                    stickMaterial(true);
                                }
                            }

                            else if (hit.collider.tag == "Obstacle")
                            {
                                canShoot = false;
                                ball4line.enabled = false;
                                this.transform.position = hit.point;
                                this.transform.position = new Vector3(this.transform.position.x, positionY, this.transform.position.z);
                                stickMaterial(true);
                            }

                            Vector3 direction = (ball4.transform.position - this.transform.position).normalized;
                            Quaternion lookRotation = Quaternion.LookRotation(direction);
                            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, 1);
                            this.transform.rotation = new Quaternion(0, this.transform.rotation.y, 0, this.transform.rotation.w);
                        }

                        else
                        {
                            canShoot = false;
                            if (!PauseMenu.isPaused)
                            {
                                canvasGetClose.SetActive(true);
                            }
                            ball4line.enabled = false;
                        }


                    }
                }
            }

            else
            {
                playerTurn = 1;
            }
        }

        if (canShoot)
        {
            hitButton.interactable = true;
        }

        else if (!canShoot)
        {
            hitButton.interactable = false;
        }
        
    }












    void stickMaterial(bool transparent)
    {
        if (transparent)
        {
            stickObject.gameObject.GetComponent<Renderer>().materials = myMaterialsTransparent;
        }

        else
        {
            if(playerTurn == 1)
            {
                stickObject.gameObject.GetComponent<Renderer>().materials = myMaterialsOpaque1;
            }

            else if (playerTurn == 2)
            {
                stickObject.gameObject.GetComponent<Renderer>().materials = myMaterialsOpaque2;
            }

            else if (playerTurn == 3)
            {
                stickObject.gameObject.GetComponent<Renderer>().materials = myMaterialsOpaque3;
            }

            else if (playerTurn == 4)
            {
                stickObject.gameObject.GetComponent<Renderer>().materials = myMaterialsOpaque4;
            }
            
        }
        
    }











    public void hitBall()
    {
        if (canShoot)
        {
            stickObject.GetComponent<Collider>().enabled = true;
            canShoot = false;
            lineRenderer = false;
            if (playerTurn == 1)
            {
                playersInfo.scorePlayer1 += 1;
                playersInfo.finalScorePlayer1 += 1;
            }

            else if (playerTurn == 2)
            {
                playersInfo.scorePlayer2 += 1;
                playersInfo.finalScorePlayer2 += 1;
            }

            else if (playerTurn == 3)
            {
                playersInfo.scorePlayer3 += 1;
                playersInfo.finalScorePlayer3 += 1;
            }

            else if (playerTurn == 4)
            {
                playersInfo.scorePlayer4 += 1;
                playersInfo.finalScorePlayer4 += 1;
            }
            makingAnim = true;
           
            GameObject ball = playerTurn == 1 ? ball1 : playerTurn == 2 ? ball2 : playerTurn == 3 ? ball3 : ball4;
            fraction_of_the_way_there = 0f;
            Start_Pos = gameObject.transform.position;
            End_Pos = gameObject.transform.position + (gameObject.transform.forward * (Vector3.Distance(ball.transform.position, this.transform.position) - 0.04f));
            End_Pos = new Vector3(End_Pos.x, ball.transform.position.y + 0.008f, End_Pos.z);
            acercarElPalo = true;
            ball.GetComponent<LineRenderer>().enabled = false;
        }
        
    }










    public void ballMovement()
    {
        GameObject ball = playerTurn == 1 ? ball1 : playerTurn == 2 ? ball2 : playerTurn == 3 ? ball3 : ball4;
        ball.GetComponent<LineRenderer>().enabled = false;
        Vector3 direction = new Vector3(ball.transform.position.x, this.transform.position.y, ball.transform.position.z) - this.transform.position;
        ball.GetComponent<Rigidbody>().AddForce(direction * image.fillAmount * 75, ForceMode.Impulse);
        stickObject.GetComponent<Collider>().enabled = false;
        StartCoroutine(pausa());
    }

    IEnumerator pausa()
    {
        
        yield return new WaitForSeconds(1f);
        
        ballHitted = true;
        stickAnim.SetBool("isHitted", false);
        makingAnim = false;
        //canShoot = true;
    }

    bool allEnd()
    {
        if (playersInfo.numberPlayers == 1)
        {
            return player1end;
        }

        else if (playersInfo.numberPlayers == 2)
        {
            return player1end && player2end;
        }

        else if (playersInfo.numberPlayers == 3)
        {
            return player1end && player2end && player3end;
        }

        else
        {
            return player1end && player2end && player3end && player4end;
        }

    }
}
