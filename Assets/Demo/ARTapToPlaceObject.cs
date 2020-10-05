using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceObject : MonoBehaviour
{
	public GameObject objectToPlace;
	public GameObject placementIndicator;
	private ARRaycastManager arRaycastManager;
	private ARSessionOrigin arOrigin;
	private Pose placementPose;
	private bool placementPoseIsValid = false;
	public static bool spawnDefined;
	public GameObject canvasShoot;
	public GameObject canvasLocate;
	int contadorDeSpawners;

	void Start()
	{
		arOrigin = FindObjectOfType<ARSessionOrigin>();
		spawnDefined = false;
		contadorDeSpawners = 0;
		canvasLocate.SetActive(true);

		#if UNITY_ANDROID
		arRaycastManager = FindObjectOfType<ARRaycastManager>();
		#endif
	}

    void Update()
	{
		if (contadorDeSpawners < 3)
		{
			UpdatePlacementPose();
			UpdatePlacementIndicator();
        
			if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			{
				PlaceObject();
			}
		}

        else
        {
			spawnDefined = true;
			canvasShoot.SetActive(true);
			canvasLocate.SetActive(false);
			Destroy(this.gameObject);
		}
        
	}

    private void PlaceObject()
	{
		contadorDeSpawners++;
		Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
		
	}


	private void UpdatePlacementIndicator()
	{
        if (placementPoseIsValid)
		{
			placementIndicator.SetActive(true);
			placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
		}

		else
		{
			placementIndicator.SetActive(false);
		}
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
