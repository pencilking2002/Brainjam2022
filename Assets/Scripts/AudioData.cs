using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "AudioData", menuName = "AudioData")]
public class AudioData : ScriptableObject
{
    public AudioClip track;
    public AudioClip[] pickupClips;
    public AudioClip polypPickedUpSound;
    public AudioClip[] voiceCues;
}
