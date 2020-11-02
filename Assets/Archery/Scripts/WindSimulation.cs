using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSimulation : MonoBehaviour
{
    bool windSimulationIsActive;
    [SerializeField]Vector3 currentWindForce;
    public GameObject flagNoWindPrefab;
    public GameObject flagWithWindPrefab;
    Transform[] childTransforms;
    private readonly GameObject[] flags = new GameObject[2];
    // Start is called before the first frame update
    private void Awake()
    {
        childTransforms = GetComponentsInChildren<Transform>();
        SpawnFlags(flagNoWindPrefab);
    }
    void NewWind()
    {
        currentWindForce = Vector3.zero;
        do
        {
            currentWindForce.x = Random.Range(-1.2f, 1.2f);
        } while (currentWindForce.x == 0);
        //currentWindForce.y = Random.value * Random.Range(.4f, .8f);
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
        SpawnFlags(flagNoWindPrefab);
    }

    public void AddWindToArrow(GameObject arrowObject)
    {
        print(windSimulationIsActive);
        print(currentWindForce);
        if (windSimulationIsActive)
            arrowObject.GetComponent<Rigidbody>().velocity += currentWindForce;
    }

    private void SpawnFlags(GameObject typeOfFlag)
    {
        for (int i = 1; i < childTransforms.Length; i++)
        {
           if (childTransforms[i].childCount != 0)
            {
                print("TIENE HIJOS");
                Destroy(childTransforms[i].GetChild(0).gameObject);
            }
            flags[i - 1] = Instantiate(typeOfFlag, childTransforms[i]);
            if (currentWindForce.x > 0) flags[i - 1].transform.Rotate(0f, 180f, 0f);
        }
    }
}
