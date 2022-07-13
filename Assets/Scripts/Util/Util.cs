using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour
{
    public static int waypointLayer = 7;
    public static int polypPlacementLayer = 8;
    public static int polypColliderLayer = 9;

    public static string glowStrength = "_GlowStrength";
    public static string grow = "_Grow";
    public static string alpha = "_Alpha";
    public static string foregroundVolume = "ForegroundVolume";
}

public static class ExtensionMethods
{
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
