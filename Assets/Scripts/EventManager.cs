using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public class Player
    {
        public static Action<Waypoint> onWaypointFilled;

    }
}
