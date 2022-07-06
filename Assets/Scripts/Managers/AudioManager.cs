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
}
