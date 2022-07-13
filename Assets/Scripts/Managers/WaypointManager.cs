using System;
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

    private void RevealNext(float delay = 0)
    {
        LeanTween.delayedCall(gameObject, delay, () =>
        {
            var prevWaypoint = GameManager.Instance.vrController.currWaypoint;
            bool isFirstWaypoint = prevWaypoint == null;

            if (isFirstWaypoint || prevWaypoint.HasNextWaypoint())
            {
                var nextWaypoint = isFirstWaypoint ? waypoints[0] : prevWaypoint.GetNextWaypoint();
                nextWaypoint.SetIdle();

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
        currWaypoint = waypoint;
        hasCurrWaypoint = currWaypoint != null;
    }

    private void OnTriggerNextWaypointSequence()
    {
        var waypoint = GameManager.Instance.vrController.currWaypoint;
        if (waypoint && waypoint.GetWaypointIndex() == 0)
        {
            var length = GameManager.Instance.audioManager.GetSimulationSoundLength();
            GameManager.Instance.audioManager.PlaySimulationVoiceCue();
            LeanTween.delayedCall(gameObject, length, () =>
            {
                RevealNext(1);
            });
        }
        else
        {
            RevealNext(1);
        }
        //Debug.Log("Trigger next waypoint");
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
