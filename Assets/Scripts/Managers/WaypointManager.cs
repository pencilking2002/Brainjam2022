using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    [SerializeField] private WaypointTree[] waypointTree;
    [SerializeField] private int currTreeIndex = -1;
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
        RevealNext();
    }

    private void Update()
    {
        if (hasCurrWaypoint)
        {
            currWaypoint.OnUpdate();
        }
    }

    private void RevealNext()
    {
        currTreeIndex++;

        if (currTreeIndex < waypoints.Length - 1)
        {
            var waypoints = waypointTree[currTreeIndex].waypoints;

            foreach (Waypoint waypoint in waypoints)
                waypoint.SetIdle();
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

[Serializable]
public class WaypointTree
{
    public Waypoint[] waypoints;
}
