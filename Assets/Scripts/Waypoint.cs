using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FillState
{
    IDLE,
    FILLING,
    FILLED
}

public class Waypoint : MonoBehaviour
{
    [SerializeField][ColorUsage(false, true)] private Color defaultColor;
    [SerializeField][ColorUsage(false, true)] private Color highlightColor;

    [SerializeField] private Renderer cylinderRend;
    [SerializeField] private FillState fillState;
    [SerializeField] private Image loadingCircle;
    [SerializeField] private float fillSpeed = 0.05f;
    private Material cylinderMat;

    private void Awake()
    {
        cylinderMat = cylinderRend.material;
    }

    private void Update()
    {
        if (IsFilling())
        {
            IncreaseFill();

            if (loadingCircle.fillAmount >= 1.0f)
            {
                SetFilled();
                EventManager.Player.onWaypointFilled?.Invoke(this);
            }
        }
        else if (IsIdle())
        {
            loadingCircle.fillAmount = 0;
        }
    }

    private void IncreaseFill()
    {
        loadingCircle.fillAmount += fillSpeed * Time.deltaTime;
    }

    public bool IsIdle() { return fillState == FillState.IDLE; }
    public bool IsFilling() { return fillState == FillState.FILLING; }
    public bool IsFilled() { return fillState == FillState.FILLED; }

    public void SetIdle()
    {
        cylinderMat.SetColor("_Color", defaultColor);
        fillState = FillState.IDLE;
    }

    public void SetFilling()
    {
        cylinderMat.SetColor("_Color", highlightColor);
        fillState = FillState.FILLING;
    }
    public void SetFilled() { fillState = FillState.FILLED; }

}
