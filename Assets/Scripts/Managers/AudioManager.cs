using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicAudioSource_01;
    [SerializeField] private AudioSource musicAudioSource_02;
    [SerializeField] private AudioSource voiceAudioSource;
    [SerializeField] private AudioData audioData;
    //[SerializeField] private int currVoiceCue;

    private void Awake()
    {
        //PlayMusic();
    }

    // private void PlayMusic()
    // {
    //     musicAudioSource_01.PlayOneShot(audioData.track);
    // }

    private void PlayOneShotSound(AudioSource audioSource, AudioClip clip, bool isLoop = false)
    {
        audioSource.loop = isLoop;
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
        LeanTween.value(gameObject, 1, 0, 0.3f).setOnUpdate((float val) =>
        {
            musicAudioSource_02.volume = val;
        });
        var currWaypoint = GameManager.Instance.vrController.currWaypoint;

        AudioClip clip = null;
        if (currWaypoint == null)
        {
            clip = audioData.introVoiceCue;
            PlayOneShotSound(voiceAudioSource, clip);
            LeanTween.delayedCall(clip.length, () =>
            {
                LeanTween.value(gameObject, 0, 1, 0.3f).setOnUpdate((float val) =>
                {
                    musicAudioSource_02.volume = val;
                });
                if (onComplete != null)
                    onComplete();
            });
        }
        else
        {
            var index = currWaypoint.GetWaypointIndex();

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
                if (onComplete != null)
                    onComplete();
            });
        }
    }

    public void PlayBuildup()
    {
        PlayOneShotSound(voiceAudioSource, audioData.teleportBuildup, true);
    }

    public void StopBuildup()
    {
        // voiceAudioSource.loop = false;
        // voiceAudioSource.Stop();
    }

    public void Teleport()
    {
        PlayOneShotSound(voiceAudioSource, audioData.teleport);
    }

    private AudioClip GetRandomPickupClip()
    {
        return audioData.pickupClips[UnityEngine.Random.Range(0, audioData.pickupClips.Length)];
    }
}
