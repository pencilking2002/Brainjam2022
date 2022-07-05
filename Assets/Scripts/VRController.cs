using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRController : MonoBehaviour
{
    [SerializeField] private XROrigin origin;
    [SerializeField] private LookRaycaster raycaster;
    [SerializeField] private InputActionAsset playerControls;
    public Waypoint currWaypoint;
    InputAction interactAction;
    InputAction valueTestAction;

    private void Awake()
    {
        InitializeControls();
    }

    private void InitializeControls()
    {
        var map = playerControls.FindActionMap("Player");
        interactAction = map.FindAction("Interact");
        valueTestAction = map.FindAction("ValueTest");

        interactAction.performed += OnInteract;
        interactAction.Enable();

        valueTestAction.performed += OnValueTestPress;
        valueTestAction.Enable();
    }

    private void Update()
    {
        raycaster.Raycast(this);
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        //Debug.Log("grip pressed");
    }

    private void OnValueTestPress(InputAction.CallbackContext context)
    {
        //Debug.Log("val: " + context.ReadValue<float>().ToString("F2"));
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
        {
            currWaypoint.SetInteractable(true);
        }
        currWaypoint = waypoint;
        currWaypoint.SetInteractable(false);
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
