using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionSphere : MonoBehaviour
{
    public float transitionDuration = 2;
    private Renderer rend;
    private Material mat;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
        mat = rend.material;
    }

    public void FadeToBlack(float delay)
    {
        LeanTween.delayedCall(gameObject, delay, () =>
        {
            rend.enabled = true;
            LeanTween.value(gameObject, 0, 1, 2).setOnUpdate((float val) =>
            {
                mat.SetFloat(Util.alpha, val);
            })
            .setOnComplete(() =>
            {
                Camera.main.backgroundColor = Color.black;
                SceneManager.LoadScene(1, LoadSceneMode.Single);
                LeanTween.delayedCall(1, () =>
                {
                    LeanTween.value(gameObject, 1, 0, 0.5f).setOnUpdate((float val) =>
                    {
                        mat.SetFloat(Util.alpha, val);
                    })
                    .setOnComplete(() =>
                    {
                        rend.enabled = false;
                    });
                });
            });
        });
    }
}
