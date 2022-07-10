using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource voiceAudioSource;
    [SerializeField] private AudioData audioData;
    //[SerializeField] private int currVoiceCue;

    private void Awake()
    {
        PlayMusic();
    }

    private void PlayMusic()
    {
        musicAudioSource.PlayOneShot(audioData.track);
    }

    private void PlayOneShotSound(AudioSource audioSource, AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlayRandomPickupClip(AudioSource source)
    {
        var clip = GetRandomPickupClip();
        PlayOneShotSound(source, clip);
    }

    public void PlayPickupComplete(AudioSource audioSource)
    {
        PlayOneShotSound(audioSource, audioData.polypPickedUpSound);
    }

    public void PlayVoiceCue(Action onComplete = null)
    {
        var currWaypoint = GameManager.Instance.vrController.currWaypoint;

        AudioClip clip = null;
        if (currWaypoint == null)
        {
            clip = audioData.introVoiceCue;
            PlayOneShotSound(voiceAudioSource, clip);
            LeanTween.delayedCall(clip.length, () =>
            {
                if (onComplete != null)
                    onComplete();
            });
        }
        else
        {
            var index = currWaypoint.GetWaypintIndex();

            if (index == 0)
                clip = audioData.waypointVoiceCues_01[currWaypoint.currVoiceCue];
            else if (index == 1)
                clip = audioData.waypointVoiceCues_02[currWaypoint.currVoiceCue];
            else if (index == 2)
                clip = audioData.waypointVoiceCues_03[currWaypoint.currVoiceCue];
            else if (index == 3)
                clip = audioData.waypointVoiceCues_04[currWaypoint.currVoiceCue];
            else if (index == 4)
                clip = audioData.waypointVoiceCues_05[currWaypoint.currVoiceCue];

            PlayOneShotSound(voiceAudioSource, clip);
            LeanTween.delayedCall(clip.length, () =>
            {
                currWaypoint.currVoiceCue++;
                if (onComplete != null)
                    onComplete();
            });
        }

    }

    private AudioClip GetRandomPickupClip()
    {
        return audioData.pickupClips[UnityEngine.Random.Range(0, audioData.pickupClips.Length)];
    }
}
