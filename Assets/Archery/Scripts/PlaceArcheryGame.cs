using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;

public class PlaceArcheryGame : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject placementIndicator;
    private ARSessionOrigin arOrigin;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    public static bool spawnDefined;
    public GameObject mainCanvas;
    private GameObject scenario;

    bool timeToLoad;

    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        spawnDefined = false;
        timeToLoad = false;
        StartCoroutine(LoadComplete());

#if UNITY_ANDROID
		arRaycastManager = FindObjectOfType<ARRaycastManager>();
#endif
    }

    IEnumerator LoadComplete()
    {
        yield return new WaitForSeconds(1);
        timeToLoad = true;
    }

    void Update()
    {
        if (!spawnDefined && timeToLoad)
        {


#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.A))
            {
                PlaceObject();
                spawnDefined = true;
                placementIndicator.SetActive(false);
                FindObjectOfType<ArcheryGameManager>().GameHasStarted();
                Destroy(this.gameObject);
            }
#else
            UpdatePlacementPose();
            UpdatePlacementIndicator();
            if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PlaceObject();
                spawnDefined = true;
                placementIndicator.SetActive(false);
                FindObjectOfType<ArcheryGameManager>().GameHasStarted();
                Destroy(this.gameObject);
            }
#endif
        }
    }

    private void PlaceObject()
    {
        scenario = Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
        scenario.transform.position = new Vector3(scenario.transform.position.x, scenario.transform.position.y, scenario.transform.position.z);
    }


    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }

        else
            placementIndicator.SetActive(false);
    }



    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();

#if UNITY_IOS
        arOrigin.GetComponent<ARRaycastManager>().Raycast(screenCenter, hits, TrackableType.Planes);
#endif

#if UNITY_ANDROID
		arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
#endif

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
}
