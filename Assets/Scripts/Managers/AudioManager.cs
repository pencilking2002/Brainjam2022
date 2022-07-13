using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusicAudioSource;
    [SerializeField] private AudioSource ForegroundMusicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource voiceAudioSource;
    [SerializeField] private AudioData audioData;
    //[SerializeField] private int currVoiceCue;

    private void Awake()
    {
        sfxAudioSource.mute = false;
        sfxAudioSource.volume = 1;
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
        Debug.Log("pickup sound");
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

    public float GetSimulationSoundLength()
    {
        return audioData.simulationVoiceCue.length;
    }

    public void PlaySimulationVoiceCue()
    {
        PlayOneShotSound(voiceAudioSource, audioData.simulationVoiceCue);
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

    public void PlaySimulationSound()
    {
        PlayOneShotSound(sfxAudioSource, audioData.simulation);
        Debug.Log("plays simulation sound");
    }

    private AudioClip GetRandomPickupClip()
    {
        return audioData.pickupClips[UnityEngine.Random.Range(0, audioData.pickupClips.Length)];
    }
}
