using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;

public class placeMinigolf : MonoBehaviour
{
	public GameObject objectToPlace;
	public GameObject placementIndicator;
	private ARSessionOrigin arOrigin;
	private Pose placementPose;
	private bool placementPoseIsValid = false;
	public static bool spawnDefined;
	public GameObject canvasLocate;


	void Start()
	{
		arOrigin = FindObjectOfType<ARSessionOrigin>();
		spawnDefined = false;
		placementIndicator.SetActive(false);
		canvasLocate.SetActive(true);
	}

	void Update()
	{
		if (!spawnDefined)
		{
			UpdatePlacementPose();
			UpdatePlacementIndicator();

			if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			{
				GameObject levels = Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
				levels.transform.localScale.Set(0.5f, 0.5f, 0.5f);
				levels.transform.position = new Vector3(levels.transform.position.x, levels.transform.position.y - 0.025f, levels.transform.position.z);
				spawnDefined = true;
				canvasLocate.SetActive(false);
				placementIndicator.SetActive(false);
				golfStick.empiezaElJuego = true;
			}
		
		
		}
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
		arOrigin.GetComponent<ARRaycastManager>().Raycast(screenCenter, hits, TrackableType.Planes);

		Debug.Log("Counter of hits: " + hits.Count);

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
