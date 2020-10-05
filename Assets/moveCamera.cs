using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public GameObject cameraPos;
    GameObject palo;
    float positionY;
    public LayerMask layermask;
    public GameObject arCamera;
    // Start is called before the first frame update
    void Start()
    {
        cameraPos = GameObject.FindGameObjectWithTag("MainCamera");
        palo = GameObject.FindGameObjectWithTag("Stick");
        positionY = palo.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {



		hit = new RaycastHit();
		if (Physics.Raycast(cameraPos.transform.position, Camera.allCameras[0].transform.forward, out hit, Mathf.Infinity, layermask))
		{
            

            if (hit.collider.tag == "GolfTerrain")
            {
                palo.transform.position = hit.point;
                palo.transform.position = new Vector3(palo.transform.position.x, positionY, palo.transform.position.z);
                Debug.DrawRay(cameraPos.transform.position, Camera.allCameras[0].transform.forward * hit.distance, Color.yellow);
            }

            else
            {
                palo.transform.position = hit.point;
                palo.transform.position = new Vector3(palo.transform.position.x, positionY, palo.transform.position.z);
                Debug.DrawRay(cameraPos.transform.position, Camera.allCameras[0].transform.forward * hit.distance, Color.red);
            }
        }
        


    }

    public void hitInfo()
    {
        Debug.Log(hit.collider.name);
    }
}
