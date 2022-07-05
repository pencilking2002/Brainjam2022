using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "AudioData")]
public class AudioData : ScriptableObject
{
    public AudioClip[] clips;
}
