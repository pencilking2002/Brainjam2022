using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolypSpawner : MonoBehaviour
{
    public PolypPickup[] pickups;

    private void Awake()
    {
        pickups = GetComponentsInChildren<PolypPickup>(true);
    }

    private void Start()
    {
        GameManager.Instance.polypSpawner = this;
    }

    public PolypPickup GetPickup()
    {
        foreach (var pickup in pickups)
        {
            if (!pickup.IsActive())
            {
                pickup.Activate(true);
                return pickup;
            }
        }
        return null;
    }

    public void InsertPickup(PolypPickup pickup)
    {
        pickup.Activate(false);
        pickup.transform.localPosition = Vector3.zero;
    }

    private void OnWaypointFilled(Waypoint waypoint)
    {
        for (int i = 0; i < waypoint.maxNumPolypPickups; i++)
        {
            var pickup = GetPickup();
            var randomVector = (new Vector3(0.001f, 0.001f, 0.001f) + Random.insideUnitSphere).normalized;
            randomVector.y *= 0.15f;
            pickup.transform.position = waypoint.transform.position + (Vector3.up) + randomVector;
        }
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
