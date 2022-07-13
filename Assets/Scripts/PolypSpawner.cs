using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolypSpawner : MonoBehaviour
{
    //public GameObject pickupToInstantiate;
    public int numPickups = 7;
    public PolypPickup[] pickups;

    private void Awake()
    {
        pickups = GetComponentsInChildren<PolypPickup>(true);
        pickups = new PolypPickup[numPickups];
        for (int i = 0; i < numPickups; i++)
        {
            var pickup = Instantiate(transform.GetChild(0).gameObject);
            pickup.transform.SetParent(transform);
            pickup.transform.localPosition = Vector3.zero;
            pickups[i] = pickup.GetComponent<PolypPickup>();
        }
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

    public void PositionPickup(PolypPickup pickup, Waypoint waypoint)
    {
        var player = GameManager.Instance.vrController;
        var spot = GameManager.Instance.GetPlaceSpot(waypoint);
        var playerDirection = (player.cam.transform.position - spot.transform.position).normalized;
        pickup.transform.position = spot.transform.position + (Vector3.up * 0.25f) + (playerDirection * 0.5f);
    }
}
