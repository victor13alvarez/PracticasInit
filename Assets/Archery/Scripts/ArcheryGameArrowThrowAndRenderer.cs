using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.iOS;


public class ArcheryGameArrowThrowAndRenderer : MonoBehaviour
{
    Scene parallelScene;
    PhysicsScene parallelPhysicsScene;

    public Vector3 initialVelocity;
    public float forceMultiplicator = 100f;
    GameObject arrowObject;
    GameObject targetObject;
    LineRenderer lineRenderer;
    ArcheryGameManager archeryGameManager;

    Vector3 mousePositionAtStart;

    bool mainPhysics = true;
    public float maximumForce = 500f;
    public float minimumForce = 0f;
    public float minimumXForce = -500f;
    float maximumForceNormalized = 13;
    float minimumForceNormalized = 3;
    float minimumForceNormalizedX = -10f;
    float maximumForceNormalizedX = 10f;


    public Transform startMousePosition;

    bool arrowIsgettingThrowed;


    // Start is called before the first frame update
    void Start()
    {
        Physics.autoSimulation = false;
        arrowIsgettingThrowed = false;
        targetObject = GameObject.Find("Scenario");
        lineRenderer = GetComponent<LineRenderer>();

        CreateSceneParameters createSceneParameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        parallelScene = SceneManager.CreateScene("ParallelScene", createSceneParameters);
        parallelPhysicsScene = parallelScene.GetPhysicsScene();
        archeryGameManager = FindObjectOfType<ArcheryGameManager>();
        arrowObject = GameObject.FindGameObjectWithTag("Arrow");


    }

    void SimulatePhysics()
    {
        GameObject simulationObject = Instantiate(arrowObject);
        GameObject simulationPlane = Instantiate(targetObject);

        SceneManager.MoveGameObjectToScene(simulationObject, parallelScene);
        SceneManager.MoveGameObjectToScene(simulationPlane, parallelScene);
        simulationObject.GetComponent<Rigidbody>().useGravity = true;
        simulationObject.GetComponent<Rigidbody>().velocity = arrowObject.GetComponent<Rigidbody>().velocity + initialVelocity;
        simulationObject.GetComponent<Rigidbody>().angularVelocity = arrowObject.GetComponent<Rigidbody>().angularVelocity;
        simulationObject.tag = "Untagged"; //Importante para las simulaciones y el escenario

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            parallelPhysicsScene.Simulate(Time.fixedDeltaTime);
            lineRenderer.SetPosition(i, simulationObject.transform.position);
            if (i > 0 && lineRenderer.GetPosition(i - 1).x == lineRenderer.GetPosition(i).x && lineRenderer.GetPosition(i - 1).z == lineRenderer.GetPosition(i).z)
                lineRenderer.positionCount = i;
        }
        Destroy(simulationObject);
        Destroy(simulationPlane);
    }
    void Shoot()
    {
        lineRenderer.positionCount = 0;
        arrowObject.GetComponent<Rigidbody>().useGravity = true;
        arrowObject.GetComponent<Rigidbody>().velocity += initialVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (!arrowIsgettingThrowed)
        {
            if (Input.GetMouseButtonDown(0) && Vector2.Distance(Input.mousePosition, startMousePosition.position) <= 125f)
            {
                mousePositionAtStart = Input.mousePosition;
                lineRenderer.positionCount = 100;
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
                    SimulatePhysics();
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

    public void ArrowHasImpacted()
    {
        archeryGameManager.ArrowWasThrowed();
        arrowObject = GameObject.FindGameObjectWithTag("Arrow");
        arrowIsgettingThrowed = false;
    }
}
