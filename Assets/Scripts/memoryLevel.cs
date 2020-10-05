using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class memoryLevel : MonoBehaviour
{
    public static int level;
    public Dropdown dropdown;

    // Update is called once per frame
    void Update()
    {
        level = dropdown.value;
    }
}
