using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcheryGameArrowRenderer : MonoBehaviour
{
    Scene parallelScene;
    PhysicsScene parallelPhysicsScene;

    public Vector3 initalVelocity;
    GameObject arrowObject;
    GameObject targetObject;
    LineRenderer lineRenderer;

    bool mainPhysics = true;

    // Start is called before the first frame update
    void Start()
    {
        Physics.autoSimulation = false;
        targetObject = GameObject.Find("Scenario");
        lineRenderer = GetComponent<LineRenderer>();

        CreateSceneParameters createSceneParameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        parallelScene = SceneManager.CreateScene("ParallelScene", createSceneParameters);
        parallelPhysicsScene = parallelScene.GetPhysicsScene();
    }

    void SimulatePhysics()
    {
        arrowObject = GameObject.FindGameObjectWithTag("Arrow");
        GameObject simulationObject = Instantiate(arrowObject);
        GameObject simulationPlane = Instantiate(targetObject);

        SceneManager.MoveGameObjectToScene(simulationObject, parallelScene);
        SceneManager.MoveGameObjectToScene(simulationPlane, parallelScene);
        simulationObject.GetComponent<Rigidbody>().useGravity = true;
        simulationObject.GetComponent<Rigidbody>().velocity = arrowObject.GetComponent<Rigidbody>().velocity + initalVelocity;
        simulationObject.GetComponent<Rigidbody>().angularVelocity = arrowObject.GetComponent<Rigidbody>().angularVelocity;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            parallelPhysicsScene.Simulate(Time.fixedDeltaTime);
            lineRenderer.SetPosition(i, simulationObject.transform.position);
            if (i > 0 && lineRenderer.GetPosition(i - 1).x == lineRenderer.GetPosition(i).x && lineRenderer.GetPosition(i - 1).z == lineRenderer.GetPosition(i).z)
                lineRenderer.positionCount = i+5;
        }
        Destroy(simulationObject);
        Destroy(simulationPlane);
    }
    void Shoot()
    {
        arrowObject.GetComponent<Rigidbody>().useGravity = true;
        arrowObject.GetComponent<Rigidbody>().velocity += initalVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                lineRenderer.positionCount = 100;
                mainPhysics = false;
            }
            initalVelocity += new Vector3(0f, .2f, .2f) * Time.deltaTime;
            SimulatePhysics();
        }

        if (Input.GetMouseButtonUp(0))
        {
            mainPhysics = true;
            Shoot();
        }
    }

    void FixedUpdate()
    {

        if (mainPhysics)
            SceneManager.GetActiveScene().GetPhysicsScene().Simulate(Time.fixedDeltaTime);
    }
}
