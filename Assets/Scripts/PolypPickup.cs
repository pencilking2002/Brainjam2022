using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolypPickup : MonoBehaviour
{
    [SerializeField] private bool isActive;
    private ParticleSystem ps;
    private UnityEngine.ParticleSystem.EmissionModule emission;

    private void Awake()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        emission = ps.emission;
        emission.enabled = false;
    }

    public void Activate(bool activate)
    {
        isActive = activate;
        emission.enabled = activate;
    }

    public bool IsActive() { return isActive; }

    public void Pickup()
    {
        GameManager.Instance.polypSpawner.InsertPickup(this);
        EventManager.Player.onPickupPolyp?.Invoke(this);
    }
}
