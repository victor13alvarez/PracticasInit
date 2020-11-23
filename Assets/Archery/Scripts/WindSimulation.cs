using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSimulation : MonoBehaviour
{
    bool windSimulationIsActive;
    Vector3 currentWindForce;
    public GameObject flagNoWindPrefab;
    public GameObject flagWithWindPrefab;
    public GameObject windZone;
    Transform[] childTransforms;
    private readonly GameObject[] flags = new GameObject[2];
    // Start is called before the first frame update
    private void Awake()
    {
        childTransforms = GetComponentsInChildren<Transform>();
        SpawnFlags(flagNoWindPrefab);
        SetUpWindZone(0f, 0f, 0f, 0f);
    }
    void NewWind()
    {
        currentWindForce = Vector3.zero;
        do
        {
            currentWindForce.x = Random.Range(-.8f, .8f);
        } while (currentWindForce.x == 0);
        //currentWindForce.y = Random.value * Random.Range(.4f, .8f);
        windZone.transform.rotation = currentWindForce.x > 0 ? Quaternion.Euler(0f, 90f, 0f) : Quaternion.Euler(0f, -90f, 0f);
        SetUpWindZone(100f, 5f, .3f, .1f);

    }

    public void RoundHasWind()
    {
        NewWind();
        if (windSimulationIsActive)
            return;
        windSimulationIsActive = true;
        SpawnFlags(flagWithWindPrefab);
    }

    public void RoundIsQuiet()
    {
        if (!windSimulationIsActive)
            return;
        windSimulationIsActive = false;
        windZone.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        SetUpWindZone(0f, 0f, 0f, 0f);
        SpawnFlags(flagNoWindPrefab);
    }

    public void AddWindToArrow(GameObject arrowObject)
    {
        if (windSimulationIsActive)
            arrowObject.GetComponent<Rigidbody>().velocity += currentWindForce;
    }

    private void SpawnFlags(GameObject typeOfFlag)
    {
        for (int i = 1; i < childTransforms.Length; i++)
        {
           if (childTransforms[i].childCount != 0)
            {
                Destroy(childTransforms[i].GetChild(0).gameObject);
            }
            flags[i - 1] = Instantiate(typeOfFlag, childTransforms[i]);
            if (currentWindForce.x > 0) flags[i - 1].transform.Rotate(0f, 180f, 0f);
        }
    }
    private void SetUpWindZone(float a, float b, float c, float d)
    {
        windZone.GetComponent<WindZone>().windMain = a;
        windZone.GetComponent<WindZone>().windTurbulence = b;
        windZone.GetComponent<WindZone>().windPulseFrequency = c;
        windZone.GetComponent<WindZone>().windPulseMagnitude = d;
    }
}
