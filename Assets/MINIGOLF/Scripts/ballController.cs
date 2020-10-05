using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballController : MonoBehaviour
{
    public GameObject canvasWin;
    public GameObject canvasGame;
    Rigidbody ballRigidbody;
    GameObject stick;
    bool newHit;
    Vector3 initPos;
    public Vector3 ballPosSave;
    public Material[] ballDesign1;
    public Material[] ballDesign2;
    public Material[] ballDesign3;
    public Material[] ballDesign4;
    public Mesh ballModel1;
    public Mesh ballModel2;
    public Mesh ballModel3;
    public Mesh ballModel4;
    public AudioClip hoyo;
    public AudioClip hitted;

    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.tag == "Ball1")
        {
            if (playersInfo.ballChosenPlayer1 == 1)
            {
                this.GetComponent<MeshFilter>().mesh = ballModel1;
                this.GetComponent<MeshRenderer>().materials = ballDesign1;
            }
            else if (playersInfo.ballChosenPlayer1 == 2)
            {
                this.GetComponent<MeshFilter>().mesh = ballModel2;
                this.GetComponent<MeshRenderer>().materials = ballDesign2;
            }

            else if (playersInfo.ballChosenPlayer1 == 3)
            {
                this.GetComponent<MeshFilter>().mesh = ballModel3;
                this.GetComponent<MeshRenderer>().materials = ballDesign3;
            }

            else if (playersInfo.ballChosenPlayer1 == 4)
            {
                this.GetComponent<MeshFilter>().mesh = ballModel4;
                this.GetComponent<MeshRenderer>().materials = ballDesign4;
            }
        }

        else if (this.gameObject.tag == "Ball2")
            
        {
            if (playersInfo.ballChosenPlayer2 == 1)
            {
                this.GetComponent<MeshFilter>().mesh = ballModel1;
                this.GetComponent<MeshRenderer>().materials = ballDesign1;
            }
            else if (playersInfo.ballChosenPlayer2 == 2)
            {
                this.GetComponent<MeshFilter>().mesh = ballModel2;
                this.GetComponent<MeshRenderer>().materials = ballDesign2;
            }

            else if (playersInfo.ballChosenPlayer2 == 3)
            {
                this.GetComponent<MeshFilter>().mesh = ballModel3;
                this.GetComponent<MeshRenderer>().materials = ballDesign3;
            }

            else if (playersInfo.ballChosenPlayer2 == 4)
            {
                this.GetComponent<MeshFilter>().mesh = ballModel4;
                this.GetComponent<MeshRenderer>().materials = ballDesign4;
            }
        }

        else if (this.gameObject.tag == "Ball3")

        {
            if (playersInfo.ballChosenPlayer3 == 1)
            {
                this.GetComponent<MeshFilter>().mesh = ballModel1;
                this.GetComponent<MeshRenderer>().materials = ballDesign1;
            }
            else if (playersInfo.ballChosenPlayer3 == 2)
            {
                this.GetComponent<MeshFilter>().mesh = ballModel2;
                this.GetComponent<MeshRenderer>().materials = ballDesign2;
            }

            else if (playersInfo.ballChosenPlayer3 == 3)
            {
                this.GetComponent<MeshFilter>().mesh = ballModel3;
                this.GetComponent<MeshRenderer>().materials = ballDesign3;
            }

            else if (playersInfo.ballChosenPlayer3 == 4)
            {
                this.GetComponent<MeshFilter>().mesh = ballModel4;
                this.GetComponent<MeshRenderer>().materials = ballDesign4;
            }
        }

        else if (this.gameObject.tag == "Ball4")

        {
            if (playersInfo.ballChosenPlayer4 == 1)
            {
                this.GetComponent<MeshFilter>().mesh = ballModel1;
                this.GetComponent<MeshRenderer>().materials = ballDesign1;
            }
            else if (playersInfo.ballChosenPlayer4 == 2)
            {
                this.GetComponent<MeshFilter>().mesh = ballModel2;
                this.GetComponent<MeshRenderer>().materials = ballDesign2;
            }

            else if (playersInfo.ballChosenPlayer4 == 3)
            {
                this.GetComponent<MeshFilter>().mesh = ballModel3;
                this.GetComponent<MeshRenderer>().materials = ballDesign3;
            }

            else if (playersInfo.ballChosenPlayer4 == 4)
            {
                this.GetComponent<MeshFilter>().mesh = ballModel4;
                this.GetComponent<MeshRenderer>().materials = ballDesign4;
            }
        }

        ballRigidbody = this.GetComponent<Rigidbody>();
        initPos = this.transform.position;
        ballRigidbody.velocity = new Vector3(0, 0, 0);
        ballRigidbody.angularVelocity = new Vector3(0, 0, 0);
        stick = GameObject.FindGameObjectWithTag("Stick");
        newHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(ballRigidbody.velocity.magnitude == 0)
        {
            ballPosSave = this.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "HoleEasy")
        {
            this.gameObject.GetComponent<AudioSource>().volume = 0.75f;
            this.gameObject.GetComponent<AudioSource>().clip = hoyo;
            this.gameObject.GetComponent<AudioSource>().Play();
            if (this.gameObject.tag == "Ball1")
            {
                stick.GetComponent<stickController>().player1end = true;
                this.GetComponent<MeshRenderer>().enabled = false;
                this.GetComponent<Collider>().enabled = false;
                stickController.ballHitted = false;
                stickController.lineRenderer = true;

            }

            else if(this.gameObject.tag == "Ball2")
            {
                stick.GetComponent<stickController>().player2end = true;
                stickController.ballHitted = false;
                stickController.lineRenderer = true;
                this.GetComponent<MeshRenderer>().enabled = false;
                this.GetComponent<Collider>().enabled = false;
            }

            else if (this.gameObject.tag == "Ball3")
            {
                stick.GetComponent<stickController>().player3end = true;
                stickController.ballHitted = false;
                stickController.lineRenderer = true;
                this.GetComponent<MeshRenderer>().enabled = false;
                this.GetComponent<Collider>().enabled = false;
            }

            else if (this.gameObject.tag == "Ball4")
            {
                stick.GetComponent<stickController>().player4end = true;
                stickController.ballHitted = false;
                stickController.lineRenderer = true;
                this.GetComponent<MeshRenderer>().enabled = false;
                this.GetComponent<Collider>().enabled = false;
            }


        }

        else if (other.tag == "HoleMedium")
        {
            this.gameObject.GetComponent<AudioSource>().volume = 0.75f;
            this.gameObject.GetComponent<AudioSource>().clip = hoyo;
            this.gameObject.GetComponent<AudioSource>().Play();
            if (this.gameObject.tag == "Ball1")
            {
                stick.GetComponent<stickController>().player1end = true;
                this.GetComponent<MeshRenderer>().enabled = false;
                this.GetComponent<Collider>().enabled = false;
            }

            else if (this.gameObject.tag == "Ball2")
            {
                stick.GetComponent<stickController>().player2end = true;
                this.GetComponent<MeshRenderer>().enabled = false;
                this.GetComponent<Collider>().enabled = false;
            }

            else if (this.gameObject.tag == "Ball3")
            {
                stick.GetComponent<stickController>().player3end = true;
                this.GetComponent<MeshRenderer>().enabled = false;
                this.GetComponent<Collider>().enabled = false;
            }

            else if (this.gameObject.tag == "Ball4")
            {
                stick.GetComponent<stickController>().player4end = true;
                this.GetComponent<MeshRenderer>().enabled = false;
                this.GetComponent<Collider>().enabled = false;
            }
        }

        else if (other.tag == "HoleHard")
        {
            this.gameObject.GetComponent<AudioSource>().volume = 0.75f;
            this.gameObject.GetComponent<AudioSource>().clip = hoyo;
            this.gameObject.GetComponent<AudioSource>().Play();
            if (this.gameObject.tag == "Ball1")
            {
                stick.GetComponent<stickController>().player1end = true;
                this.GetComponent<MeshRenderer>().enabled = false;
                this.GetComponent<Collider>().enabled = false;
            }

            else if (this.gameObject.tag == "Ball2")
            {
                stick.GetComponent<stickController>().player2end = true;
                this.GetComponent<MeshRenderer>().enabled = false;
                this.GetComponent<Collider>().enabled = false;
            }

            else if (this.gameObject.tag == "Ball3")
            {
                stick.GetComponent<stickController>().player3end = true;
                this.GetComponent<MeshRenderer>().enabled = false;
                this.GetComponent<Collider>().enabled = false;
            }

            else if (this.gameObject.tag == "Ball4")
            {
                stick.GetComponent<stickController>().player4end = true;
                this.GetComponent<MeshRenderer>().enabled = false;
                this.GetComponent<Collider>().enabled = false;
            }
        }


        if (other.tag == "Limits")
        {
            if (stick.GetComponent<stickController>().playerTurn == 1)
            {
                playersInfo.scorePlayer1 += 1;
                playersInfo.finalScorePlayer1 += 1;
            }

            else if (stick.GetComponent<stickController>().playerTurn == 2)
            {
                playersInfo.scorePlayer2 += 1;
                playersInfo.finalScorePlayer2 += 1;
            }

            else if (stick.GetComponent<stickController>().playerTurn == 3)
            {
                playersInfo.scorePlayer3 += 1;
                playersInfo.finalScorePlayer3 += 1;
            }

            else if (stick.GetComponent<stickController>().playerTurn == 4)
            {
                playersInfo.scorePlayer4 += 1;
                playersInfo.finalScorePlayer4 += 1;
            }
            ballRigidbody.velocity = new Vector3(0, 0, 0);
            ballRigidbody.angularVelocity = new Vector3(0, 0, 0);
            this.GetComponent<MeshRenderer>().enabled = false;
            this.transform.position = ballPosSave;
        }

        if (other.gameObject.tag == "StickColl")
        {
            this.gameObject.GetComponent<AudioSource>().volume = stick.GetComponent<stickController>().image.fillAmount;
            this.gameObject.GetComponent<AudioSource>().clip = hitted;
            this.gameObject.GetComponent<AudioSource>().Play();
            stick.GetComponent<stickController>().ballMovement();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "StickColl")
        {
            
            stick.GetComponent<stickController>().ballMovement();
        }

        if (collision.gameObject.tag == "Arena")
        {
            this.GetComponent<Rigidbody>().drag = 2f;
            this.GetComponent<Rigidbody>().angularDrag = 1.5f;
        }

        if (collision.gameObject.tag == "Agua")
        {
            if (stick.GetComponent<stickController>().playerTurn == 1)
            {
                playersInfo.scorePlayer1 += 1;
                playersInfo.finalScorePlayer1 += 1;
            }

            else if (stick.GetComponent<stickController>().playerTurn == 2)
            {
                playersInfo.scorePlayer2 += 1;
                playersInfo.finalScorePlayer2 += 1;
            }

            else if (stick.GetComponent<stickController>().playerTurn == 3)
            {
                playersInfo.scorePlayer3 += 1;
                playersInfo.finalScorePlayer3 += 1;
            }

            else if (stick.GetComponent<stickController>().playerTurn == 4)
            {
                playersInfo.scorePlayer4 += 1;
                playersInfo.finalScorePlayer4 += 1;
            }
            ballRigidbody.velocity = new Vector3(0, 0, 0);
            ballRigidbody.angularVelocity = new Vector3(0, 0, 0);
            this.GetComponent<MeshRenderer>().enabled = false;
            this.transform.position = ballPosSave;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Arena")
        {
            this.GetComponent<Rigidbody>().drag = 1f;
            this.GetComponent<Rigidbody>().angularDrag = 0f;
        }
    }
}
