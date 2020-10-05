using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public GameObject thisObject;
    public Material materialWhite;
    public bool imShown;
    public GameObject myShape;
    GameObject tablero;
    Animator anim;
    public GameObject explodeEffect;
    
    public void OnMouseDown()
    {
        if (!imShown && tablero.GetComponent<MemoryGameController>().turnAvailable && tablero.GetComponent<MemoryGameController>().cardsShown <= 1)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = materialWhite;

            if (tablero.GetComponent<MemoryGameController>().cardsShown == 0)
            {
                Debug.Log(this.transform.rotation);
                tablero.GetComponent<MemoryGameController>().cardText = this.gameObject.name;
                tablero.GetComponent<MemoryGameController>().cardsShown = 1;
                tablero.GetComponent<MemoryGameController>().totalModelsShown += 1;
                Debug.Log("El valor de cardsShown = " + tablero.GetComponent<MemoryGameController>().cardsShown);
                imShown = true;
                Debug.Log(tablero.GetComponent<MemoryGameController>().cardText);
            }

            else if (tablero.GetComponent<MemoryGameController>().cardsShown == 1)
            {
                tablero.GetComponent<MemoryGameController>().cardsShown = 0;
                tablero.GetComponent<MemoryGameController>().totalModelsShown += 1;
                imShown = true;
                Debug.Log("El nombre del primer objeto es: " + tablero.GetComponent<MemoryGameController>().cardText);
                Debug.Log("El nombre del segundo objeto es: " + this.gameObject.name);
                if (this.gameObject.name != tablero.GetComponent<MemoryGameController>().cardText)
                {
                    tablero.GetComponent<MemoryGameController>().failedAttempt = true;
                    StartCoroutine(pausita());
                }
                else if (this.gameObject.name == tablero.GetComponent<MemoryGameController>().cardText)
                {
                    tablero.GetComponent<MemoryGameController>().wonAttempt = true;
                    tablero.GetComponent<MemoryGameController>().bestPlayer += 2;
                    Instantiate(explodeEffect,  new Vector3(tablero.transform.position.x, tablero.transform.position.y + explodeEffect.transform.position.y, tablero.transform.position.z), explodeEffect.transform.rotation);
                    
                }
                Debug.Log(tablero.GetComponent<MemoryGameController>().cardText);

            }
            this.GetComponent<AudioSource>().Play();
            myShape.SetActive(true);
            anim.SetBool("isActive", true);
            StartCoroutine(pausa());
            
        }
        
        
    }

    IEnumerator pausita()
    {
        yield return new WaitForSeconds(0.8f);
        if(tablero.GetComponent<MemoryGameController>().player1Turn == playersInfo.numberPlayers)
        {
            tablero.GetComponent<MemoryGameController>().player1Turn = 1;
        }

        else
        {
            tablero.GetComponent<MemoryGameController>().player1Turn++;
        }
        //tablero.GetComponent<MemoryGameController>().player1Turn = !tablero.GetComponent<MemoryGameController>().player1Turn;

    }

    IEnumerator pausa()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("isActive", false);

    }

    // Start is called before the first frame update
    void Start()
    {
        imShown = false;
        myShape = Instantiate(thisObject, this.transform.position + new Vector3(0, thisObject.transform.position.y, 0), thisObject.transform.rotation);
        anim = myShape.GetComponent<Animator>();
        myShape.SetActive(false);
        tablero = GameObject.FindGameObjectWithTag("Tablero");

    }

    // Update is called once per frame
    void Update()
    {
        //if (!imShown && myShape != null)
        //{
        //    myShape.SetActive(false);
        //}
    }
}
