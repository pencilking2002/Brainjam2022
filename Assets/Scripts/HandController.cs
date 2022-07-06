using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Util.polypColliderLayer)
        {
            //Debug.Log("Collider with polyp");
            other.GetComponent<PolypPickup>().Pickup();
        }
        else if (other.gameObject.layer == Util.polypPlacementLayer)
        {
            var placement = other.GetComponent<PolypPlaceSpot>();

            if (placement.IsReady())
            {
                //Debug.Log("place polyps");
                placement.SetInteractable();
            }
        }
    }
}
