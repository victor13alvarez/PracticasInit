using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcheryGameManager : MonoBehaviour
{


    //public static ArcheryGameManager archeryGameManager;

    //private void Awake()
    //{
    //    if (archeryGameManager == null)
    //    {
    //        archeryGameManager = this;
    //        DontDestroyOnLoad(this.gameObject);
    //    }
    //    else
    //        Destroy(this);
    //}

    public GameObject arrowPrefab;

    public void ArrowWasThrowed()
    {
        SpawnNewArrow();
    }

    void SpawnNewArrow()
    {
        GameObject currentArrow = Instantiate(arrowPrefab);
        currentArrow.tag = "Arrow";
    }


}
