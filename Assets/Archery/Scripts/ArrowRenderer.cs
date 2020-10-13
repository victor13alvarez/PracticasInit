using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowRenderer : MonoBehaviour
{
    Scene parallelScene;
    PhysicsScene parallelPhysicsScene;
    GameObject targetObject;
    LineRenderer lineRenderer;


    private void Start()
    {
        CreateSceneParameters createSceneParameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        parallelScene = SceneManager.CreateScene("ParallelScene", createSceneParameters);
        parallelPhysicsScene = parallelScene.GetPhysicsScene();
        targetObject = GameObject.Find("ArcheryTarget");
        lineRenderer = GetComponent<LineRenderer>();

    }

    public void SimulatePhysics(GameObject arrowObject, Vector3 initialVelocity)
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
    public void ManageLineRenderer(bool enabled, int positionCount)
    {
        lineRenderer.enabled = enabled;
        lineRenderer.positionCount = positionCount;
    }
}
