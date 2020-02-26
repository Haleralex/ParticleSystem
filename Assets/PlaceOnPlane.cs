using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VFX;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceOnPlane : MonoBehaviour
{

    private List<ARRaycastHit> hits;

    public GameObject model;

    ARRaycastManager raycastManager;
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        hits = new List<ARRaycastHit>();
        model.SetActive(false);
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);
        
        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon) & !IsPointerOverUIObject(touch.position))
        {
            Pose pose = hits[0].pose;

            if (!model.activeInHierarchy)
            {
                model.SetActive(true);

                model.transform.position = pose.position;
                model.transform.rotation = pose.rotation;
            }
            else
            {
                model.transform.position = pose.position;
                model.transform.rotation = pose.rotation;
            }
        }
    }
    bool IsPointerOverUIObject(Vector2 pos)
    {
        if (EventSystem.current == null)
            return false;
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(pos.x, pos.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
