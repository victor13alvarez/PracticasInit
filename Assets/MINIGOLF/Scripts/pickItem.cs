using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickItem : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    public GameObject target;
    private Vector3 targetPoint;
    private Quaternion targetRotation;
    bool located;
    public Camera cam;

    private void Start()
    {
        located = false;
    }

    private void Update()
    {
        if (!located)
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                Debug.Log("Moviendose");
                Debug.DrawRay(ray.direction, hit.transform.position, Color.green);
                this.gameObject.transform.position = new Vector3(hit.transform.position.x, target.transform.position.y , hit.transform.position.z);
            }

            targetPoint = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position;
            targetRotation = Quaternion.LookRotation(-targetPoint, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5.0f);

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                located = true;
            }

        }
        
    }

    public void hitBall()
    {
        target.GetComponent<Rigidbody>().AddForce(targetPoint * 50f);
    }
}
