using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowRenderer : MonoBehaviour
{
    Scene parallelScene;
    PhysicsScene parallelPhysicsScene;
    LineRenderer lineRenderer;
    public List<Vector3> arrowSimulationPositions;

    public GameObject scenarioToSimulate;


    private void Start()
    {
        CreateSceneParameters createSceneParameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        parallelScene = SceneManager.CreateScene("ParallelScene", createSceneParameters);
        parallelPhysicsScene = parallelScene.GetPhysicsScene();
        lineRenderer = GetComponent<LineRenderer>();
        parallelPhysicsScene.Simulate(Time.fixedDeltaTime);

        scenarioToSimulate.transform.position = this.transform.parent.transform.position;
        arrowSimulationPositions = new List<Vector3>();

    }

    public void SimulatePhysics(GameObject arrowObject, Vector3 initialVelocity)
    {
        GameObject simulatedScenario = Instantiate(scenarioToSimulate);
        GameObject simulatedArrow = Instantiate(arrowObject, simulatedScenario.transform);

        SceneManager.MoveGameObjectToScene(simulatedScenario, parallelScene);
        simulatedArrow.GetComponent<Rigidbody>().useGravity = true;
        simulatedArrow.GetComponent<Rigidbody>().velocity = initialVelocity;
        simulatedArrow.tag = "Untagged"; //Importante para las simulaciones y el escenario
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            parallelPhysicsScene.Simulate(Time.fixedDeltaTime);
            lineRenderer.SetPosition(i, simulatedArrow.transform.localPosition);
            if (i > 0 && lineRenderer.GetPosition(i - 1).x == lineRenderer.GetPosition(i).x && lineRenderer.GetPosition(i - 1).z == lineRenderer.GetPosition(i).z)
                lineRenderer.positionCount = i;

        }
        Destroy(simulatedScenario);
    }

    public void ManageLineRenderer(bool enabled, int positionCount)
    {
        lineRenderer.enabled = enabled;
        lineRenderer.positionCount = positionCount;
        lineRenderer.startWidth = .1f * this.transform.parent.localScale.magnitude;
        lineRenderer.endWidth = .1f * this.transform.parent.localScale.magnitude;
    }
}