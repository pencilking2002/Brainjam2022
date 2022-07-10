using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PolypPickup : MonoBehaviour
{
    [SerializeField] private bool isActive;
    private ParticleSystem ps;
    private UnityEngine.ParticleSystem.EmissionModule emission;
    private UnityEngine.ParticleSystem.MainModule main;
    public AudioSource audioSource;
    public float pickupDuration = 2.0f;
    [ReadOnly] public float pickupTimer;
    public float timerDecreaseRate = 0.01f;

    private void Awake()
    {
        pickupTimer = pickupDuration;
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
        //main.maxParticles = 100;
        pickupTimer = pickupDuration;
        GameManager.Instance.polypSpawner.InsertPickup(this);
        EventManager.Player.onPickupPolyp?.Invoke(this);
        GameManager.Instance.audioManager.PlayVoiceCue();
        // Debug.Log("complete");
    }

    public void DecreaseEmissionRate()
    {
        // if (main.maxParticles > 0)
        //     main.maxParticles -= 2;
        // else
        //     Pickup();
        pickupTimer -= timerDecreaseRate * Time.deltaTime;
        if (pickupTimer <= 0)
            Pickup();
    }
}
