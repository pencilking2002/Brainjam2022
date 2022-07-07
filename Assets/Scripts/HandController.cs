using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private Collider currPickupCollider;
    private PolypPickup currPickup;
    private Renderer rend;
    private Material mat;
    private Vector3 initScale;

    private void Awake()
    {
        rend = GetComponentInChildren<Renderer>();
        mat = rend.material;
        initScale = transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Util.polypColliderLayer)
        {
            //Debug.Log("Collider with polyp");
            //other.GetComponent<PolypPickup>().Pickup();
        }
        else if (other.gameObject.layer == Util.polypPlacementLayer)
        {
            var placement = other.GetComponent<PolypPlaceSpot>();

            if (placement.IsReady())
            {
                //Debug.Log("place polyps");
                placement.SetInteractable();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other != currPickupCollider)
        {
            currPickupCollider = other;
            currPickup = other.GetComponent<PolypPickup>();
        }
        currPickup.DecreaseEmissionRate();
    }

    private void SetGlowStrength(float glowStrength)
    {
        mat.SetFloat(Util.glowStrength, glowStrength);
    }

    private void OnPickupPolyp(PolypPickup pickup)
    {
        LeanTween.delayedCall(gameObject, 0.1f, () =>
        {
            LeanTween.cancel(gameObject);
            transform.localScale = initScale;
            var currWaypoint = GameManager.Instance.vrController.currWaypoint;
            float amountComplete = (float)currWaypoint.numPolypsPickedUp / (float)currWaypoint.maxNumPolypPickups;

            var seq = LeanTween.sequence();
            seq.append(() =>
            {
                SetGlowStrength(5);
            });

            seq.append(() =>
            {
                LeanTween.value(gameObject, 5, amountComplete, 0.25f)
                .setOnUpdate((float val) =>
                {
                    SetGlowStrength(val);
                });

                transform.localScale = initScale * 1.1f;
                LeanTween.value(gameObject, transform.localScale, initScale, 0.25f)
                .setOnUpdate((Vector3 val) =>
                {
                    transform.localScale = val;
                });
            });
        });

    }

    private void OnEnable()
    {
        EventManager.Player.onPickupPolyp += OnPickupPolyp;
    }

    private void OnDisable()
    {
        EventManager.Player.onPickupPolyp -= OnPickupPolyp;
    }
}
