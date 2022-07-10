using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [ReadOnly] public WaypointManager waypointManager;
    [ReadOnly] public PolypSpawner polypSpawner;
    [ReadOnly] public VRController vrController;
    public AudioManager audioManager;
    public InputManager inputManager;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        InitInstance();
    }

    private void InitInstance()
    {
        if (Instance == null)
            Instance = this;
        else
            Instance = null;
    }
}
