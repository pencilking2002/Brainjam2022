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

        pickupTimer = pickupDuration;
        GameManager.Instance.polypSpawner.InsertPickup(this);

        var currWaypoint = GameManager.Instance.vrController.currWaypoint;
        if (currWaypoint.numPolypsPickedUp < currWaypoint.maxNumPolypPickups)
        {
            Debug.Log("pickup");
            currWaypoint.numPolypsPickedUp++;
            currWaypoint.PlayNextVoiceCue();

            // if (currWaypoint.numPolypsPickedUp == currWaypoint.maxNumPolypPickups)
            // {
            //     //Debug.Log("Picked up all polyps for waypoint");
            //     EventManager.Player.onCompletePolypPickupForWaypoint?.Invoke(currWaypoint);
            // }
            EventManager.Player.onPickupPolyp?.Invoke(this);

            // GameManager.Instance.audioManager.PlayVoiceCue(() =>
            // {
            //     EventManager.Game.onPolypVoiceCueComplete?.Invoke(currWaypoint);
            //     Debug.Log($"voice cur complete: curr num:{currWaypoint.numPolypsPickedUp} max:{currWaypoint.maxNumPolypPickups}");
            // });
        }
    }

    public void DecreaseEmissionRate()
    {
        if (!isActive)
            return;

        pickupTimer -= timerDecreaseRate * Time.deltaTime;
        if (pickupTimer <= 0)
            Pickup();
    }
}
