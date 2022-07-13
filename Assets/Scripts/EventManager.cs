using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public class Player
    {
        public static Action<Waypoint> onWaypointFillStart;
        public static Action<Waypoint> onWaypointFilled;
        public static Action<Waypoint[]> onWaypointsRevealed;
        public static Action<PolypPickup> onPickupPolyp;
        public static Action<Waypoint> onCompletePolypPickupForWaypoint;
    }

    public class Game
    {
        //public static Action onBeginWaypointSequence;
        public static Action onTriggerNextWaypointSequence;
        public static Action<Waypoint> onPolypVoiceCueComplete;
    }

    public class Input
    {
        public static Action onPressConfirm;
    }
}
