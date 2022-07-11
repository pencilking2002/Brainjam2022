using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(AudioSource))]
public class HandAudioController : MonoBehaviour
{
    [ReadOnly] private AudioSource audioSource;
    public Collider col;
    private bool isColliding;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        if (isColliding)
        {
            GameManager.Instance.audioManager.PlayRandomPickupClip(audioSource);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == Util.polypColliderLayer)
            isColliding = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Util.polypColliderLayer)
            isColliding = false;
    }

    public void PlayRandomPickupSound()
    {
        GameManager.Instance.audioManager.PlayRandomPickupClip(audioSource);
    }

    private void OnPolypPickup(PolypPickup pickup)
    {
        GameManager.Instance.audioManager.PlayPickupComplete(audioSource);
    }

    private void OnEnable()
    {
        EventManager.Player.onPickupPolyp += OnPolypPickup;
    }

    private void OnDisable()
    {
        EventManager.Player.onPickupPolyp -= OnPolypPickup;
    }
}
