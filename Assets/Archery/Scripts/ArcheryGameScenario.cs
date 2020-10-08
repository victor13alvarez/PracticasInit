using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcheryGameScenario : MonoBehaviour
{
    public ArcheryGameArrowThrowAndRenderer archeryGameArrowThrowAndRenderer;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Rigidbody>() != null) {
            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            collision.gameObject.GetComponent<Collider>().isTrigger = true;
        }
        if (collision.gameObject.CompareTag("Arrow"))
        {
            collision.gameObject.tag = "Untagged";
            archeryGameArrowThrowAndRenderer.ArrowHasImpacted();
        }
    }
}
