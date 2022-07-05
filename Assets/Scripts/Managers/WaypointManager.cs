using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    [SerializeField] private Waypoint[] waypoints;
    [SerializeField] private Waypoint currWaypoint;
    [SerializeField] private bool hasCurrWaypoint;

    private void Awake()
    {
        waypoints = GetComponentsInChildren<Waypoint>(true);
    }

    private void Start()
    {
        GameManager.Instance.waypointManager = this;
    }

    private void Update()
    {
        if (hasCurrWaypoint)
        {
            currWaypoint.OnUpdate();
        }
    }

    public bool HasWaypoint() { return hasCurrWaypoint; }
    public Waypoint GetCurrWaypoint() { return currWaypoint; }

    public void SetCurrWaypoint(Waypoint waypoint)
    {
        currWaypoint = waypoint;
        hasCurrWaypoint = currWaypoint != null;
    }
}
