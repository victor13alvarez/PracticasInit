using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject[] baloons;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSpawning());
    }


    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(4f);
        if (ARTapToPlaceObject.spawnDefined)
        {
            Instantiate(baloons[Random.Range(0, 3)], this.transform.position, Quaternion.identity);
        }

        
        
        StartCoroutine(StartSpawning());
    }
}
