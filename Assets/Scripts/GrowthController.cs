using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthController : MonoBehaviour
{
    [SerializeField] Renderer[] rends;
    private Material[] mats;

    private void Awake()
    {
        CacheMaterials();
        //StartGrowthTest();
    }

    private void CacheMaterials()
    {
        rends = GetComponentsInChildren<Renderer>();
        mats = new Material[rends.Length];
        for (int i = 0; i < rends.Length; i++)
        {
            mats[i] = rends[i].material;
            mats[i].SetFloat(Util.grow, 0);
        }
    }

    // private void StartGrowthTest()
    // {
    //     LeanTween.value(gameObject, 0, 1, 2.0f).setOnUpdate((float val) =>
    //     {
    //         for (int i = 0; i < rends.Length; i++)
    //         {
    //             mats[i].SetFloat(Util.grow, val);
    //         }
    //     })
    //     .setRepeat(-1);
    // }

    public void Grow()
    {
        LeanTween.value(gameObject, 0, 1, 2.0f).setOnUpdate((float val) =>
        {
            for (int i = 0; i < rends.Length; i++)
            {
                mats[i].SetFloat(Util.grow, val);
            }
        });
        GameManager.Instance.audioManager.PlaySimulationSound();
    }
}
