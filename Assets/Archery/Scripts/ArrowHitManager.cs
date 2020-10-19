using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHitManager : MonoBehaviour
{
    public ArcheryGameManager archeryGameManager { get; set; }
    public GameObject arrowHitted;
    public GameObject arrowMissed;

    private void OnCollisionEnter(Collision collision)
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        this.GetComponent<Collider>().isTrigger = true;
        if (this.gameObject.CompareTag("Untagged"))
            return;

        switch (collision.gameObject.name)
        {                
            case "Scenario(Clone)":
                archeryGameManager.ArrowThrowed(0);
                Instantiate(arrowMissed, collision.GetContact(0).point, arrowMissed.transform.rotation, this.transform.parent);

                break;
            case "ArcheryTarget":
                Instantiate(arrowHitted, collision.GetContact(0).point, arrowHitted.transform.rotation,this.transform.parent);
                archeryGameManager.ArrowThrowed(100);
                break;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        this.GetComponent<Collider>().isTrigger = true;
        if (this.gameObject.CompareTag("Untagged"))
            return;
        Instantiate(arrowMissed, collision.ClosestPoint(this.transform.position), arrowMissed.transform.rotation, this.transform.parent);
        archeryGameManager.ArrowThrowed(0);
    }
}
