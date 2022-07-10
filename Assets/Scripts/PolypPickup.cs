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
        if (!isActive)
            return;

        //main.maxParticles = 100;
        pickupTimer = pickupDuration;
        GameManager.Instance.polypSpawner.InsertPickup(this);

        var currWaypoint = GameManager.Instance.vrController.currWaypoint;
        if (currWaypoint.numPolypsPickedUp < currWaypoint.maxNumPolypPickups - 1)
        {
            GameManager.Instance.audioManager.PlayVoiceCue(() =>
            {
                EventManager.Game.onPolypVoiceCueComplete?.Invoke(currWaypoint);
            });
        }
        EventManager.Player.onPickupPolyp?.Invoke(this);
        Debug.Log("pickup");
    }

    public void DecreaseEmissionRate()
    {
        // if (main.maxParticles > 0)
        //     main.maxParticles -= 2;
        // else
        //     Pickup();
        if (!isActive)
            return;

        pickupTimer -= timerDecreaseRate * Time.deltaTime;
        if (pickupTimer <= 0)
            Pickup();
    }
}
