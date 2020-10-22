using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.SceneManagement;

public class ArrowManager : MonoBehaviour
{

    public Vector3 initialVelocity;
    public float forceMultiplicator = 100f;
    internal GameObject arrowObject;
    internal WindSimulation arrowWind;
    public GameObject arrowPrefab;

    Vector3 mousePositionAtStart;

    bool mainPhysics = true;
    public float maximumForce = 100f;
    public float minimumForce = 0f;
    public float minimumXForce = -100f;
    const float maximumForceNormalized = 3f;
    const float minimumForceNormalized = 1f;
    const float minimumForceNormalizedX = -3;
    const float maximumForceNormalizedX = 3;

    public ArrowRenderer arrowRenderer;
    private bool arrowIsgettingThrowed;


    // Start is called before the first frame update
    void Awake()
    {
        Physics.autoSimulation = false;
        arrowObject = GameObject.FindGameObjectWithTag("Arrow");
        Time.timeScale = .5f;

        arrowIsgettingThrowed = false;
    }

    void Update()
    {
        if (!arrowIsgettingThrowed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePositionAtStart = Input.mousePosition;
                initialVelocity = new Vector3(0f, 0f, 5f);
                mainPhysics = false;

                arrowRenderer.ManageLineRenderer(true, 200);
            }
            else if (Input.GetMouseButton(0))
            {
                initialVelocity.y = GetNormalizedValuesY(mousePositionAtStart.y, Input.mousePosition.y);
                initialVelocity.x = GetNormalizedValuesX(mousePositionAtStart.x, Input.mousePosition.x);

                arrowRenderer.SimulatePhysics(arrowObject, initialVelocity);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                arrowObject.GetComponent<Rigidbody>().useGravity = true;
                arrowObject.GetComponent<Rigidbody>().velocity = initialVelocity;
                arrowWind.AddWindToArrow(arrowObject);

                arrowRenderer.ManageLineRenderer(false, 0);
                mainPhysics = true;
                arrowIsgettingThrowed = true;
            }
        }

        if (arrowObject != null && arrowObject.GetComponent<Rigidbody>().velocity != Vector3.zero)
            arrowObject.transform.rotation = Quaternion.LookRotation(arrowObject.GetComponent<Rigidbody>().velocity) * Quaternion.Euler(0f,-90f,0f);
    }

    float GetNormalizedValuesY(float startValue , float finalValue)
    {
        return Mathf.Clamp(startValue - finalValue, 0, maximumForce) / (maximumForce - minimumForce) * (maximumForceNormalized - minimumForceNormalized) + minimumForceNormalized;
    }
    float GetNormalizedValuesX(float startValue, float finalValue)
    {
        return Mathf.Clamp(startValue - finalValue , minimumXForce, maximumForce) / (maximumForce - minimumXForce) * (maximumForceNormalizedX - minimumForceNormalizedX);
    }

    void FixedUpdate()
    {
        if (mainPhysics)
            SceneManager.GetActiveScene().GetPhysicsScene().Simulate(Time.fixedDeltaTime);
    }

    public void DestroyCurrentArrow()
    {
        if(arrowObject != null)
        {
            arrowObject.tag = "Untagged";
            Destroy(arrowObject);
        }
    }

    public void SpawnNewArrow(ArcheryGameManager archeryGameManager)
    {
        arrowObject = Instantiate(arrowPrefab,this.transform.parent);
        arrowObject.GetComponent<ArrowHitManager>().archeryGameManager = archeryGameManager;
        arrowObject.tag = "Arrow";
        arrowIsgettingThrowed = false;
    }
}
