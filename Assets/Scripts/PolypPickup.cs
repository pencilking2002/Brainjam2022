using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolypPickup : MonoBehaviour
{
    [SerializeField] private bool isActive;

    public void Activate(bool activate)
    {
        isActive = activate;
    }

    public bool IsActive() { return isActive; }

    public void Pickup()
    {
        GameManager.Instance.polypSpawner.InsertPickup(this);
        EventManager.Player.onPickupPolyp?.Invoke(this);
    }
}
