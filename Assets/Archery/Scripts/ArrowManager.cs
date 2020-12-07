using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.iOS;
using UnityEngine.SceneManagement;

public class ArrowManager : MonoBehaviour
{

    Vector3 initialVelocity;
    internal GameObject arrowObject;
    internal WindSimulation arrowWind;
    public GameObject arrowPrefab;
    GameObject target;
    GameObject _OptionsButtons;
    List<GameObject> freezedArrows = new List<GameObject>();
    Vector3 inputPosAtStart;

    bool mainPhysics = true;
    const float maximumForce = 200f;
    const float minimumForce = 0f;
    const float minimumXForce = -200f;
    const float maximumForceNormalized = 3.5f;
    const float minimumForceNormalized = .5f;
    const float minimumForceNormalizedX = -3.5f;
    const float maximumForceNormalizedX = 3.5f;

    public ArrowRenderer arrowRenderer;
    private bool arrowIsgettingThrowed;
    private bool arrowIsPrepared;

    float xSpeed;
    public GameObject _playerModel { get; private set;}



    // Start is called before the first frame update
    void Awake()
    {
        Physics.autoSimulation = false;
        arrowObject = GameObject.FindGameObjectWithTag("Arrow");
        arrowIsgettingThrowed = false;
        target = GameObject.Find("ArcheryTarget");
        _OptionsButtons = GameObject.Find("OptionsButtons");
        _playerModel = GameObject.Find("FinalPlayerModel");

    }

    void Update()
    {
        if (!arrowIsgettingThrowed && arrowObject != null)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                inputPosAtStart = Input.mousePosition;
                initialVelocity = (target.transform.position - arrowObject.transform.position) * 3f;
                initialVelocity.y = 0f;
                mainPhysics = false;
                arrowIsPrepared = true;
                arrowRenderer.ManageLineRenderer(true, 30);
                _playerModel.GetComponent<PlayerAnimatorController>().SetTriggerAnim("AimArrow");

            }
            else if (Input.GetMouseButton(0))
            {
                initialVelocity.y = GetNormalizedValuesY(inputPosAtStart.y, Input.mousePosition.y);
                xSpeed = GetNormalizedValuesX(inputPosAtStart.x, Input.mousePosition.x);
                arrowRenderer.SimulatePhysics(arrowObject, initialVelocity + new Vector3(xSpeed, 0f));

            }
            else if (arrowIsPrepared && Input.GetMouseButtonUp(0))
            {
                initialVelocity.x += xSpeed;
                arrowIsgettingThrowed = true;
                arrowIsPrepared = false;
                arrowRenderer.ManageLineRenderer(false, 0);
                GetComponent<Animator>().SetTrigger("ArrowThrow");
                _playerModel.GetComponent<PlayerAnimatorController>().SetTriggerAnim("ShootArrow");
                //StartCoroutine(ArrowAnim());
            }
#else
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject())
            {
                inputPosAtStart = Input.GetTouch(0).position;
                initialVelocity = (target.transform.position - arrowObject.transform.position) * 3f ;
                initialVelocity.y = 0f;
                mainPhysics = false;
                arrowIsPrepared = true;
                arrowRenderer.ManageLineRenderer(true, 30);
                _playerModel.GetComponent<PlayerAnimatorController>().SetTriggerAnim("ShootArrow");

            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                initialVelocity.y = GetNormalizedValuesY(inputPosAtStart.y, Input.GetTouch(0).position.y);
                xSpeed = GetNormalizedValuesX(inputPosAtStart.x, Input.GetTouch(0).position.x);
                arrowRenderer.SimulatePhysics(arrowObject, initialVelocity + new Vector3(xSpeed,0f));

            }
            else if (arrowIsPrepared && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                initialVelocity.x += xSpeed;
                arrowIsgettingThrowed = true;
                arrowIsPrepared = false;
                arrowRenderer.ManageLineRenderer(false, 0);
                GetComponent<Animator>().SetTrigger("ArrowThrow");
                _playerModel.GetComponent<PlayerAnimatorController>().SetTriggerAnim("AimArrow");
                //StartCoroutine(ArrowAnim());
            }
#endif
        }
    }

    public void DestroyArrow()
    {
        if (arrowObject != null)
        {
            arrowObject.tag = "Untagged";
            Destroy(arrowObject);
        }
    }

    public void FreezeCurrentArrow()
    {
        if (arrowObject != null)
        {
            arrowObject.tag = "Untagged";
            arrowObject.GetComponent<Collider>().isTrigger = true;
            freezedArrows.Add(arrowObject);
        }
    }

    float GetNormalizedValuesY(float startValue, float finalValue)
    {
        return Mathf.Clamp(startValue - finalValue, 0, maximumForce) / (maximumForce - minimumForce) * (maximumForceNormalized - minimumForceNormalized) + minimumForceNormalized;
    }
    float GetNormalizedValuesX(float startValue, float finalValue)
    {
        return Mathf.Clamp(startValue - finalValue, minimumXForce, maximumForce) / (maximumForce - minimumXForce) * (maximumForceNormalizedX - minimumForceNormalizedX);
    }

    void FixedUpdate() //This is used in order to be possible to simulate physics in other scenes
    {
        if (mainPhysics)
            SceneManager.GetActiveScene().GetPhysicsScene().Simulate(Time.fixedDeltaTime);
    }

    public void DestroyCurrentArrows()
    {
        freezedArrows.ForEach(delegate (GameObject go)
        {
            Destroy(go);
        });
        freezedArrows.Clear();
    }

    public void SpawnNewArrow(ArcheryGameManager archeryGameManager)
    {
        arrowObject = Instantiate(arrowPrefab, this.transform.parent);
        arrowObject.GetComponent<ArrowHitManager>().archeryGameManager = archeryGameManager;
        arrowObject.tag = "Arrow";
        arrowIsgettingThrowed = false;
    }

    public void MoveArrow()
    {
        StopCoroutine(ArrowAnim());
        arrowObject.GetComponent<Rigidbody>().useGravity = true;
        arrowObject.GetComponent<Rigidbody>().velocity = initialVelocity;
        arrowWind.AddWindToArrow(arrowObject);
        mainPhysics = true;
    }

    IEnumerator ArrowAnim()
    {
        float elapsedFrames = 0f;
        float framesToWait = 100;
        float elapsedTime = 0f;
        float timeToWait = 5f;
        Vector3 posToGo = new Vector3(0f, 2.85f, -30.74f);
        while (elapsedFrames < framesToWait && elapsedTime < timeToWait)
        {
            arrowObject.transform.localPosition = Vector3.Lerp(arrowObject.transform.localPosition, posToGo, elapsedTime / timeToWait);
            elapsedTime += Time.deltaTime;
            elapsedFrames += 1;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    /*private void OnGUI()
    {
        float size = 100;
        GUIStyle style = new GUIStyle
        {
            fontSize = 100,
            alignment = TextAnchor.MiddleCenter
        };
        GUIContent gUIContent = new GUIContent
        {
            text = EventSystem.current.IsPointerOverGameObject().ToString(),
        };
        
        GUI.Label(new Rect(Screen.width / 2 - size, Screen.height / 2 - size, size, size), gUIContent, style);
    }*/
}

