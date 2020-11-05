using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRotationLock : MonoBehaviour
{
    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
