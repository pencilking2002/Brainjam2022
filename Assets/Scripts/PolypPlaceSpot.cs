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
    private Renderer rend;
    private Material mat;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;
        mat.SetFloat(Util.glowStrength, 0);
    }


    public void SetNone() { placeState = PlaceState.NONE; }
    public void SetReady()
    {
        mat.SetFloat(Util.glowStrength, 1);
        placeState = PlaceState.READY;
    }
    public void SetInteractable()
    {
        mat.SetFloat(Util.glowStrength, 0);
        growthController.Grow();
        placeState = PlaceState.INTERACTABLE;

        LeanTween.delayedCall(gameObject, 2, () =>
        {
            SetComplete();
            EventManager.Game.onTriggerNextWaypointSequence?.Invoke();
            Debug.Log("spawn next waypoint");
        });
    }

    public void SetComplete()
    {
        placeState = PlaceState.COMPLETE;
    }

    // public void Activate()
    // {
    //     growthController.Grow();
    // }

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
    // private void OnPolypVoiceCueComplete(Waypoint waypoint)
    // {
    //     if (this.waypoint.GetWaypointIndex() == 0)
    //     {
    //         if (this.waypoint == waypoint && waypoint.numPolypsPickedUp == waypoint.maxNumPolypPickups - 2)
    //         {
    //             Debug.Log("complete voice cue. curr cue num:" + waypoint.numPolypsPickedUp + ". max: " + waypoint.maxNumPolypPickups);

    //             if (IsNone())
    //                 SetReady();
    //         }
    //     }
    //     else
    //     {
    //         if (this.waypoint == waypoint && waypoint.numPolypsPickedUp == waypoint.maxNumPolypPickups)
    //         {
    //             Debug.Log("complete voice cue. curr cue num:" + waypoint.numPolypsPickedUp + ". max: " + waypoint.maxNumPolypPickups);

    //             if (IsNone())
    //                 SetReady();
    //         }
    //     }
    // }

    private void OnEnable()
    {
        //EventManager.Game.onPolypVoiceCueComplete += OnPolypVoiceCueComplete;
    }

    private void OnDisable()
    {
        //EventManager.Game.onPolypVoiceCueComplete -= OnPolypVoiceCueComplete;
    }
}
