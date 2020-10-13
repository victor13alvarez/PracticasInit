using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.SceneManagement;

public class ArrowManager : MonoBehaviour
{

    public Vector3 initialVelocity;
    public float forceMultiplicator = 100f;
    GameObject arrowObject;
    public GameObject arrowPrefab;

    Vector3 mousePositionAtStart;

    bool mainPhysics = true;
    public float maximumForce = 500f;
    public float minimumForce = 0f;
    public float minimumXForce = -500f;
    readonly float maximumForceNormalized = 13;
    readonly float minimumForceNormalized = 3;
    readonly float minimumForceNormalizedX = -10f;
    readonly float maximumForceNormalizedX = 10f;


    public Transform startMousePosition;
    public ArrowRenderer arrowRenderer;

    bool arrowIsgettingThrowed;


    // Start is called before the first frame update
    void Start()
    {
        Physics.autoSimulation = false;
        arrowIsgettingThrowed = false;
        arrowObject = GameObject.FindGameObjectWithTag("Arrow");
    }

    
    void Shoot()
    {
        arrowObject.GetComponent<Rigidbody>().useGravity = true;
        arrowObject.GetComponent<Rigidbody>().velocity += initialVelocity;
        arrowRenderer.ManageLineRenderer(false, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!arrowIsgettingThrowed)
        {
            if (Input.GetMouseButtonDown(0) && Vector2.Distance(Input.mousePosition, startMousePosition.position) <= 125f)
            {
                mousePositionAtStart = Input.mousePosition;
                arrowRenderer.ManageLineRenderer(true, 100);
                mainPhysics = false;
                initialVelocity = new Vector3(GetNormalizedValuesY(mousePositionAtStart.x, Input.mousePosition.x), GetNormalizedValuesY(mousePositionAtStart.y, Input.mousePosition.y), 10f);
            }
            else if (!mainPhysics)
            {
                if (Input.GetMouseButton(0))
                {
                    if (Input.mousePosition.y > 0f && Input.mousePosition.y < mousePositionAtStart.y)
                        initialVelocity.y = GetNormalizedValuesY(mousePositionAtStart.y, Input.mousePosition.y);
                    if (Input.mousePosition.x < Screen.width && Input.mousePosition.x > 0f)
                        initialVelocity.x = GetNormalizedValuesX(mousePositionAtStart.x, Input.mousePosition.x);
                    arrowRenderer.SimulatePhysics(arrowObject , initialVelocity);
                    arrowObject.transform.rotation = Quaternion.LookRotation(initialVelocity);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    mainPhysics = true;
                    arrowIsgettingThrowed = true;
                    Shoot();
                }
            }
        }
        else if(arrowObject != null && arrowObject.GetComponent<Rigidbody>().velocity != Vector3.zero) arrowObject.transform.rotation = Quaternion.LookRotation(arrowObject.GetComponent<Rigidbody>().velocity);

    }
    float GetNormalizedValuesY(float startValue , float finalValue)
    {
        return Mathf.Clamp(startValue - finalValue, 0, 500f) / (maximumForce - minimumForce) * (maximumForceNormalized - minimumForceNormalized) + minimumForceNormalized;
    }
    float GetNormalizedValuesX(float startValue, float finalValue)
    {
        return Mathf.Clamp(startValue - finalValue, -500f, 500f) / (maximumForce - minimumXForce) * (maximumForceNormalizedX - minimumForceNormalizedX);
    }

    void FixedUpdate()
    {

        if (mainPhysics)
            SceneManager.GetActiveScene().GetPhysicsScene().Simulate(Time.fixedDeltaTime);
    }

    public void ArrowHasImpacted(GameObject gameObject)
    {
        gameObject.tag = "Untagged";
        Destroy(gameObject);
        SpawnNewArrow();
    }
    private void SpawnNewArrow()
    {
        arrowObject = Instantiate(arrowPrefab);
        arrowObject.tag = "Arrow";
        arrowIsgettingThrowed = false;
    }
}
