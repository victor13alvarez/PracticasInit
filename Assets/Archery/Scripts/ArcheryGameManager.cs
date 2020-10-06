using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcheryGameManager : MonoBehaviour
{
    public static ArcheryGameManager archeryGameManager;

    public int rounds;


    void Awake()
    {
        if (archeryGameManager != null)
            GameObject.Destroy(archeryGameManager);
        else
            archeryGameManager = this;

        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        //ManageTurns();
    }
    private void ManageTurns()
    {

    }
}
