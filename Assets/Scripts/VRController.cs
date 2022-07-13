using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRController : MonoBehaviour
{
    public Camera cam;
    public TransitionSphere transition;
    [SerializeField] private Canvas canvas;
    [SerializeField] private XROrigin origin;
    [SerializeField] private LookRaycaster raycaster;
    public Waypoint currWaypoint;
    public HandAudioController[] handAudioControllers;

    private void Awake()
    {
        var localPos = canvas.transform.localPosition;
        localPos.y = 0;
        canvas.transform.localPosition = localPos;
    }

    private void Start()
    {
        GameManager.Instance.vrController = this;
    }

    private void Update()
    {
        raycaster.Raycast(this);
    }

    public HandAudioController GetHandAudioController(Vector3 particlePos, out bool controllerFound)
    {
        controllerFound = false;
        for (int i = 0; i < handAudioControllers.Length; i++)
        {
            HandAudioController audioController = handAudioControllers[i];
            if (audioController.col.bounds.Contains(particlePos))
            {
                controllerFound = true;
                return audioController;
            }
        }
        return null;
    }

    public void SetPlayerPosition(Transform spawnTransform)
    {
        var spawnPosition = spawnTransform.position;
        spawnPosition.y = origin.Camera.gameObject.transform.position.y;
        origin.MoveCameraToWorldLocation(spawnPosition);
        origin.MatchOriginUpCameraForward(spawnTransform.up, spawnTransform.forward);
    }
}
