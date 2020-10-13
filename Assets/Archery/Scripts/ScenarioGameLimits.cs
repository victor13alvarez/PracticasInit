using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioGameLimits : MonoBehaviour
{
    public ArrowManager archeryGameArrowThrowAndRenderer;

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.CompareTag("Arrow"))
            archeryGameArrowThrowAndRenderer.ArrowHasImpacted(other.gameObject);        
    }
}
