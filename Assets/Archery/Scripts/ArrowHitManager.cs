using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHitManager : MonoBehaviour
{
    public ArcheryGameManager archeryGameManager { get; set; }
    public GameObject arrowHitted;
    public GameObject arrowMissed;
    private const int maximumScore = 200;
    private const int minimumScore = 0;
    private const float minimumDist = 0f;
    private float maximimumDist = 2f;

    private void Start()
    {
        maximimumDist = GameObject.Find("ArcheryTarget").GetComponent<MeshCollider>().bounds.size.y;
    }
    private void Update()
    {
        if (GetComponent<Rigidbody>().velocity != Vector3.zero)
        {
            this.transform.rotation = Quaternion.LookRotation(this.GetComponent<Rigidbody>().velocity) * Quaternion.Euler(0f, -90f, 0f);
            Time.timeScale = .5f;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        this.GetComponent<Collider>().isTrigger = true;
        if (this.gameObject.CompareTag("Untagged"))
            return;

        switch (collision.gameObject.name)
        {                
            case "Plane":
                archeryGameManager.ArrowThrowed(0);
                Instantiate(arrowMissed, collision.GetContact(0).point, arrowMissed.transform.rotation, this.transform.parent);

                break;
            case "ArcheryTarget":
                float dist = Vector3.Distance(collision.GetContact(0).point, collision.gameObject.GetComponent<MeshCollider>().bounds.center);
                int score = CalculateScore(dist);
                archeryGameManager.ArrowThrowed(score);
                Instantiate(arrowHitted, collision.GetContact(0).point, arrowHitted.transform.rotation,this.transform.parent);
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

    int CalculateScore(float distance)
    {
        return Mathf.FloorToInt((maximimumDist - Mathf.Clamp(distance, minimumDist, maximimumDist)) / (maximimumDist - minimumDist) * (maximumScore - minimumScore) + minimumScore);
    }
}
