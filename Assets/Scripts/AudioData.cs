using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "AudioData", menuName = "AudioData")]
public class AudioData : ScriptableObject
{
    public AudioClip track;
    public AudioClip teleportBuildup;
    public AudioClip teleport;
    public AudioClip[] pickupClips;
    public AudioClip polypPickedUpSound;
    public AudioClip[] voiceCues;

    public AudioClip introVoiceCue;
    public AudioClip[] waypointVoiceCues_01;
    public AudioClip[] waypointVoiceCues_02;
    public AudioClip[] waypointVoiceCues_03;
    public AudioClip[] waypointVoiceCues_04;
    public AudioClip[] waypointVoiceCues_05;
}
