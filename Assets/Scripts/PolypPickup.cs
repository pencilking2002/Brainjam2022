using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolypPickup : MonoBehaviour
{
    [SerializeField] private bool isActive;
    private ParticleSystem ps;
    private UnityEngine.ParticleSystem.EmissionModule emission;
    private UnityEngine.ParticleSystem.MainModule main;
    public AudioSource audioSource;

    private void Awake()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        main = ps.main;
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
        main.maxParticles = 100;
        GameManager.Instance.polypSpawner.InsertPickup(this);
        EventManager.Player.onPickupPolyp?.Invoke(this);
        // GameManager.Instance.audioManager.PlayPickupComplete(audioSource);
        // Debug.Log("complete");
    }

    public void DecreaseEmissionRate()
    {
        if (main.maxParticles > 0)
            main.maxParticles -= 2;
        else
            Pickup();
    }
}
