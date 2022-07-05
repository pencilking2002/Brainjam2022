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
    [Header("Colors")]
    [SerializeField][ColorUsage(false, true)] private Color defaultColor;
    [SerializeField][ColorUsage(false, true)] private Color highlightColor;


    [Header("References")]
    [SerializeField] private Renderer cylinderRend;
    [SerializeField] private Image loadingCircle;


    [Header("Settings")]
    [SerializeField] private FillState fillState;
    [SerializeField] private float fillSpeed = 0.05f;
    [SerializeField] private Vector2 minMaxCylinderScaleY;
    private Material cylinderMat;

    private void Awake()
    {
        cylinderMat = cylinderRend.material;
    }

    public void OnUpdate()
    {
        if (IsFilling())
        {
            IncreaseFill();
            ScaleCylinder(loadingCircle.fillAmount);

            if (loadingCircle.fillAmount >= 1.0f)
            {
                SetFilled();
                EventManager.Player.onWaypointFilled?.Invoke(this);
            }
        }
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

    public bool IsIdle() { return fillState == FillState.IDLE; }
    public bool IsFilling() { return fillState == FillState.FILLING; }
    public bool IsFilled() { return fillState == FillState.FILLED; }

    public void SetIdle()
    {
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
    }
    public void SetFilled() { fillState = FillState.FILLED; }

}
