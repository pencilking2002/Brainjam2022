using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class LookRaycaster : MonoBehaviour
{
    [SerializeField] private XROrigin origin;
    [SerializeField] private float maxLookDistance = 10;
    [SerializeField] private LayerMask layerMask;


    public void Raycast(VRController controller)
    {
        Ray ray = new Ray(origin.Camera.transform.position, origin.Camera.transform.forward);
        RaycastHit hitInfo;
        bool foundWaypoint = Physics.Raycast(ray, out hitInfo, maxLookDistance, layerMask);

        if (foundWaypoint)
        {
            var waypoint = hitInfo.collider.transform.parent.GetComponent<Waypoint>();
            bool hasWaypoint = GameManager.Instance.waypointManager.HasWaypoint();
            var currWaypoint = GameManager.Instance.waypointManager.GetCurrWaypoint();

            if (!hasWaypoint || waypoint != currWaypoint)
            {
                GameManager.Instance.waypointManager.SetCurrWaypoint(waypoint);
                if (waypoint.IsIdle())
                {
                    waypoint.SetFilling();
                    GameManager.Instance.audioManager.PlayBuildup();
                }
            }
        }
        else
        {
            if (GameManager.Instance.waypointManager.HasWaypoint())
            {
                if (controller.currWaypoint != GameManager.Instance.waypointManager.GetCurrWaypoint())
                    GameManager.Instance.waypointManager.GetCurrWaypoint().SetIdle();

                GameManager.Instance.waypointManager.SetCurrWaypoint(null);
            }
            else
            {
                GameManager.Instance.audioManager.StopBuildup();
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * maxLookDistance, Color.red);
    }
}
