using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcheryGameScenario : MonoBehaviour
{
    public ArrowManager archeryGameArrowThrowAndRenderer;
    public ParticleSystem arrowParticles;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Rigidbody>() != null) {
            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            collision.gameObject.GetComponent<Collider>().isTrigger = true;
        }
        if (collision.gameObject.CompareTag("Arrow"))
        {
            archeryGameArrowThrowAndRenderer.ArrowHasImpacted(collision.gameObject);
            Instantiate(arrowParticles, collision.GetContact(0).point,arrowParticles.transform.rotation);
        }
    }
}
