using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FillState
{
    NONE,
    IDLE,
    FILLING,
    FILLED,
}

public class Waypoint : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField][ColorUsage(false, true)] private Color defaultColor;
    [SerializeField][ColorUsage(false, true)] private Color highlightColor;


    [Header("References")]
    [SerializeField] private Renderer cylinderRend;
    [SerializeField] private Image outerCircle;
    [SerializeField] private Image loadingCircle;
    [SerializeField] private Collider col;
    [SerializeField] private Waypoint nextWaypoint;


    [Header("Settings")]
    private int index;
    public int maxNumPolypPickups = 3;
    public int numPolypsPickedUp;
    [SerializeField] private FillState fillState;
    [SerializeField] private float fillSpeed = 0.05f;
    [SerializeField] private Vector2 minMaxCylinderScaleY;
    private Material cylinderMat;
    public int currVoiceCue;

    private void Awake()
    {
        cylinderMat = cylinderRend.material;
        if (IsNone())
        {
            Debug.Log("set none");
            SetNone();
        }
    }

    public void OnUpdate()
    {
        if (IsFilling())
        {
            IncreaseFill();
            ScaleCylinder(loadingCircle.fillAmount);

            if (loadingCircle.fillAmount >= 1.0f)
            {
                SetNone();
                EventManager.Player.onWaypointFilled?.Invoke(this);
            }
        }
    }

    public bool HasNextWaypoint()
    {
        return nextWaypoint != null;
    }

    public Waypoint GetNextWaypoint()
    {
        return nextWaypoint;
    }

    public int GetWaypintIndex()
    {
        return index;
    }

    private void IncreaseFill()
    {
        loadingCircle.fillAmount += fillSpeed * Time.deltaTime;
    }

    private void ScaleCylinder(float t)
    {
        var scale = cylinderRend.transform.localScale;
        scale.y = Mathf.Lerp(minMaxCylinderScaleY.x, minMaxCylinderScaleY.y, t);
        cylinderRend.transform.localScale = scale;
    }

    private void Activate(bool activate)
    {
        ScaleCylinder(0);
        col.enabled = activate;
        cylinderRend.enabled = activate;
        loadingCircle.enabled = activate;
        outerCircle.enabled = activate;
    }

    public bool IsNone() { return fillState == FillState.NONE; }
    public bool IsIdle() { return fillState == FillState.IDLE; }
    public bool IsFilling() { return fillState == FillState.FILLING; }
    public bool IsFilled() { return fillState == FillState.FILLED; }

    public void SetIdle()
    {
        Activate(true);
        col.enabled = true;
        LeanTween.cancel(gameObject);
        LeanTween.value(gameObject, loadingCircle.fillAmount, 0, 0.25f).setOnUpdate((float val) =>
        {
            ScaleCylinder(val);
        });
        loadingCircle.fillAmount = 0;
        cylinderMat.SetColor("_Color", defaultColor);
        fillState = FillState.IDLE;
    }

    public void SetFilling()
    {
        LeanTween.cancel(gameObject);
        cylinderMat.SetColor("_Color", highlightColor);
        fillState = FillState.FILLING;
        EventManager.Player.onWaypointFillStart?.Invoke(this);
    }

    public void SetFilled() { fillState = FillState.FILLED; }
    public void SetNone()
    {
        fillState = FillState.NONE;
        Activate(false);
    }

    private void OnDrawGizmos()
    {
        if (nextWaypoint)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, nextWaypoint.transform.position);
        }
    }
}
