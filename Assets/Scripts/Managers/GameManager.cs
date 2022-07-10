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
    public PolypPlaceSpot[] placeSpots;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        InitInstance();
        placeSpots = GameObject.FindObjectsOfType<PolypPlaceSpot>();
    }

    private void Start()
    {
        LeanTween.delayedCall(1, () =>
        {
            audioManager.PlayVoiceCue(() =>
            {
                EventManager.Game.onTriggerNextWaypointSequence?.Invoke();
            });
        });
    }

    public PolypPlaceSpot GetPlaceSpot(Waypoint waypoint)
    {
        for (int i = 0; i < placeSpots.Length; i++)
        {
            var spot = placeSpots[i];
            if (spot.waypoint == waypoint)
                return spot;
        }
        return null;
    }

    private void InitInstance()
    {
        if (Instance == null)
            Instance = this;
        else
            Instance = null;
    }
}
