using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class LookRaycaster : MonoBehaviour
{
    [SerializeField] private XROrigin origin;
    [SerializeField] private float maxLookDistance = 10;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private bool hasWaypoint;
    [SerializeField] private Waypoint waypoint;


    public void Raycast(VRController controller)
    {
        Ray ray = new Ray(origin.Camera.transform.position, origin.Camera.transform.forward);
        RaycastHit hitInfo;
        hasWaypoint = Physics.Raycast(ray, out hitInfo, maxLookDistance, layerMask);

        if (hasWaypoint)
        {
            waypoint = hitInfo.collider.transform.parent.GetComponent<Waypoint>();

            if (waypoint != controller.currWaypoint)
            {
                if (waypoint.IsIdle())
                {
                    waypoint.SetFilling();
                }
            }
        }
        else
        {
            if (waypoint != null)
            {
                waypoint.SetIdle();
                waypoint = null;
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * maxLookDistance, Color.red);
    }

    public bool HasWaypoint() { return hasWaypoint; }
    public Waypoint GetCurrWaypoint() { return waypoint; }
}
