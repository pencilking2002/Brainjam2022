using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    //[SerializeField] private WaypointTree[] waypointTree;
    //[SerializeField] private int currTreeIndex = -1;
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
        //RevealNext();
    }

    private void Update()
    {
        if (hasCurrWaypoint)
        {
            currWaypoint.OnUpdate();
        }
    }

    private void RevealNext(float delay = 0)
    {
        LeanTween.delayedCall(gameObject, delay, () =>
        {
            //currTreeIndex++;
            var prevWaypoint = GameManager.Instance.vrController.currWaypoint;
            bool isFirstWaypoint = prevWaypoint == null;

            //if (currTreeIndex < waypoints.Length)
            if (isFirstWaypoint || prevWaypoint.HasNextWaypoint())
            {
                var nextWaypoint = isFirstWaypoint ? waypoints[0] : prevWaypoint.GetNextWaypoint();
                nextWaypoint.SetIdle();
                // var waypoints = waypointTree[currTreeIndex].waypoints;

                // foreach (Waypoint waypoint in waypoints)
                //     waypoint.SetIdle();

                EventManager.Player.onWaypointsRevealed?.Invoke(waypoints);
                Debug.Log("Reveal waypoint");
            }
            else
            {
                Debug.Log("Will not reveal.Last waypoint reached ");
            }
        });
    }

    public bool HasWaypoint() { return hasCurrWaypoint; }
    public Waypoint GetCurrWaypoint() { return currWaypoint; }

    public void SetCurrWaypoint(Waypoint waypoint)
    {
        //Debug.Log(waypoint);
        //if (waypoint == null)
        //GameManager.Instance.audioManager.PlayVoiceCue();

        currWaypoint = waypoint;
        hasCurrWaypoint = currWaypoint != null;
    }


    private void OnTriggerNextWaypointSequence()
    {
        RevealNext(1);
    }

    private void OnEnable()
    {
        EventManager.Game.onTriggerNextWaypointSequence += OnTriggerNextWaypointSequence;
    }

    private void OnDisable()
    {
        EventManager.Game.onTriggerNextWaypointSequence -= OnTriggerNextWaypointSequence;
    }
}

[Serializable]
public class WaypointTree
{
    public Waypoint[] waypoints;
}
