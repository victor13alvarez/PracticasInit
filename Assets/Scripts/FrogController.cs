using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    public GameObject canvasLose;
    public GameObject canvasWin;
    public GameObject canvasMove;

    public float speed;
    public Vector3 destination, startPos;
    public bool jumping;
    public float startTime, time;
    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canvasLose.SetActive(false);
        canvasWin.SetActive(false);
        canvasMove.SetActive(true);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetFloat("speed", speed/4);
        if (jumping)
        {
            time = (Time.time - startTime) * speed;
            transform.position = Vector3.Lerp(startPos, destination, time);
            if(transform.position == destination)
            {
                jumping = false;
                anim.SetBool("isJumping", false);
            }
        }
        
    }

    public void goLeft()
    {
        if (!jumping && destination.x > -7)
        {
            destination = transform.position + new Vector3(-1, 0, 0);
            transform.rotation = Quaternion.Euler(0, -90, 0);
            anim.Rebind();
            anim.SetBool("isJumping", true);
            startTime = Time.time;
            startPos = transform.position;
            jumping = true;
        }
    }

    public void goRight()
    {
        if (!jumping && destination.x < 7)
        {
            destination = transform.position + new Vector3(1, 0, 0);
            transform.rotation = Quaternion.Euler(0, 90, 0);
            anim.Rebind();
            anim.SetBool("isJumping", true);
            startTime = Time.time;
            startPos = transform.position;
            jumping = true;
        }
    }

    public void goForward()
    {
        if (!jumping && destination.z < 6)
        {
            destination = transform.position + new Vector3(0, 0, 1);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            anim.Rebind();
            anim.SetBool("isJumping", true);
            startTime = Time.time;
            startPos = transform.position;
            jumping = true;
        }
    }

    public void goBackward()
    {
        if (!jumping && destination.z > -7)
        {
            destination = transform.position + new Vector3(0, 0, -1);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            anim.Rebind();
            anim.SetBool("isJumping", true);
            startTime = Time.time;
            startPos = transform.position;
            jumping = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fly")
        {
            canvasMove.SetActive(false);
            canvasWin.SetActive(true);
        }
        if (other.tag == "Obstacle")
        {
            canvasMove.SetActive(false);
            canvasLose.SetActive(true);
        }
    }
}
