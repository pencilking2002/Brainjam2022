using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HandAudioController : MonoBehaviour
{
    private AudioSource audioSource;
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

    private void OnTriggerStay()
    {
        isColliding = true;
    }

    private void OnTriggerExit()
    {
        isColliding = false;
    }

    public void PlayRandomPickupSound()
    {
        GameManager.Instance.audioManager.PlayRandomPickupClip(audioSource);
    }
}
