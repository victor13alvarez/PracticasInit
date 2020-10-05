using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golfStick : MonoBehaviour
{
    public GameObject palo;
    public GameObject mobileCamera;
    GameObject stick;
    public static bool empiezaElJuego = false;
    // Start is called before the first frame update
    void Start()
    {
        stick = Instantiate(palo, mobileCamera.transform);
        stick.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (empiezaElJuego)
        {
            stick.transform.position = mobileCamera.transform.position;
            stick.transform.rotation = mobileCamera.transform.rotation;
            stick.SetActive(true);
        }
    }
}
