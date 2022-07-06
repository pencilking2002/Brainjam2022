using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [HideInInspector] public WaypointManager waypointManager;
    [HideInInspector] public PolypSpawner polypSpawner;

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
