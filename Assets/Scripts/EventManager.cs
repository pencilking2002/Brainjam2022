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
    }

    public class Game
    {
        public static Action onBeginWaypointSequence;
    }

    public class Input
    {
        public static Action onPressConfirm;
    }
}
