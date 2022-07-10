using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTrigger : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private bool particleSystemLoaded;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystemLoaded = true;
    }
}
