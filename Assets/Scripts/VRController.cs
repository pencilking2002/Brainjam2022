using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private XROrigin origin;
    [SerializeField] private LookRaycaster raycaster;
    public Waypoint currWaypoint;

    private void Awake()
    {
        var localPos = canvas.transform.localPosition;
        localPos.y = 0;
        canvas.transform.localPosition = localPos;
    }

    private void Update()
    {
        raycaster.Raycast(this);
    }

    public void SetPlayerPosition(Transform spawnTransform)
    {
        var spawnPosition = spawnTransform.position;
        spawnPosition.y = origin.Camera.gameObject.transform.position.y;
        origin.MoveCameraToWorldLocation(spawnPosition);
        origin.MatchOriginUpCameraForward(spawnTransform.up, spawnTransform.forward);
    }

    private void OnWaypointFilled(Waypoint waypoint)
    {
        SetPlayerPosition(waypoint.transform);

        if (currWaypoint && currWaypoint != waypoint)
            currWaypoint.SetIdle();

        currWaypoint = waypoint;
    }

    private void OnEnable()
    {
        EventManager.Player.onWaypointFilled += OnWaypointFilled;
    }

    private void OnDisable()
    {
        EventManager.Player.onWaypointFilled -= OnWaypointFilled;
    }
}
