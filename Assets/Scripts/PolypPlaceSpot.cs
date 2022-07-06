using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlaceState
{
    NONE,
    READY,
    INTERACTABLE,
    COMPLETE
}

public class PolypPlaceSpot : MonoBehaviour
{
    public Waypoint waypoint;
    [SerializeField] private GrowthController growthController;
    [SerializeField] private PlaceState placeState;

    private void OnCompletePolypPickupForWaypoint(Waypoint waypoint)
    {
        if (this.waypoint == waypoint)
        {
            if (IsNone())
                SetReady();
        }
    }

    public void SetNone() { placeState = PlaceState.NONE; }
    public void SetReady() { placeState = PlaceState.READY; }
    public void SetInteractable()
    {
        growthController.Grow();
        placeState = PlaceState.INTERACTABLE;

        LeanTween.delayedCall(gameObject, 2, () =>
        {
            SetComplete();
        });
    }

    public void SetComplete() { placeState = PlaceState.COMPLETE; }

    public void Activate()
    {
        growthController.Grow();
    }

    public bool IsNone() { return placeState == PlaceState.NONE; }
    public bool IsReady() { return placeState == PlaceState.READY; }
    public bool IsInteractable() { return placeState == PlaceState.INTERACTABLE; }
    public bool IsComplete() { return placeState == PlaceState.COMPLETE; }

    private void OnDrawGizmos()
    {
        if (waypoint)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, waypoint.transform.position);
        }
    }

    private void OnEnable()
    {
        EventManager.Player.onCompletePolypPickupForWaypoint += OnCompletePolypPickupForWaypoint;
    }

    private void OnDisable()
    {
        EventManager.Player.onCompletePolypPickupForWaypoint -= OnCompletePolypPickupForWaypoint;
    }
}
