using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioData audioData;

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

    private AudioClip GetRandomPickupClip()
    {
        return audioData.pickupClips[Random.Range(0, audioData.pickupClips.Length)];
    }
}
