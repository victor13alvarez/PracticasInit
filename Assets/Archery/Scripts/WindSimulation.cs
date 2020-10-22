using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSimulation : MonoBehaviour
{
    bool windSimulationIsActive;
    private Vector3 currentWindForce;

    // Start is called before the first frame update
    private void Awake()
    {
        currentWindForce = new Vector3(0f,0f,0f);
    }

    // Update is called once per frame

    void NewWind()
    {
        currentWindForce.x = Random.value * Random.Range(.4f,1.2f);
        currentWindForce.y = Random.value * Random.Range(.4f, .8f);
    }

    internal void RoundHasWind()
    {
        NewWind();
        windSimulationIsActive = true;
    }

    internal void RoundIsQuiet()
    {
        windSimulationIsActive = false;
    }

    internal void AddWindToArrow(GameObject arrowObject)
    {
        if (windSimulationIsActive)
            arrowObject.GetComponent<Rigidbody>().velocity += currentWindForce;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        GameObject toDraw = GameObject.Find("ArcheryTarget");
        if(toDraw != null)
            Gizmos.DrawSphere(GameObject.Find("ArcheryTarget").GetComponent<MeshCollider>().bounds.center, .05f);
    }
}
