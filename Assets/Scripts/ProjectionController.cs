using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionController : MonoBehaviour
{
    [SerializeField] private Texture2D[] causticsTextures;
    [SerializeField] private float fps = 0.1f;
    private Projector projector;
    [SerializeField] private Material mat;
    private int numTextures;
    private float lastFrameTime;
    private int currFrame;
    private Material instanceMat;

    private void Awake()
    {
        projector = GetComponent<Projector>();
        numTextures = causticsTextures.Length;
        projector.material = Instantiate<Material>(mat);
        instanceMat = projector.material;
    }

    private void Update()
    {
        if (Time.time > fps + lastFrameTime)
        {
            instanceMat.SetTexture("_CausticsTex", causticsTextures[currFrame]);
            currFrame++;
            if (currFrame > numTextures - 1)
                currFrame = 0;

            lastFrameTime = Time.time;

        }
    }
}
