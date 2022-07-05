using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class IntroTextPanel : MonoBehaviour
{
    //[TextArea(5, 10)][SerializeField] private string introString;
    [SerializeField] private RectTransform container;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private bool isDisplayed;
    private CanvasGroup cg;
    private Vector3 initContainerScale;
    private void Awake()
    {
        cg = container.GetComponent<CanvasGroup>();
        initContainerScale = container.localScale;
        container.localScale = new Vector3(0.001f, initContainerScale.y, initContainerScale.z);
        Display();
    }

    private void Display()
    {
        container.gameObject.SetActive(true);
        int textLength = text.text.Length;
        text.maxVisibleCharacters = 0;
        var seq = LeanTween.sequence();
        seq.append(2);
        seq.append(container.LeanScaleX(initContainerScale.x, 0.25f));
        seq.append(0.25f);
        seq.append(LeanTween.value(gameObject, 0, textLength, 0.5f).setOnUpdate((float val) =>
        {
            text.maxVisibleCharacters = (int)val;
            isDisplayed = true;
        }));

    }
    private void Hide()
    {
        LeanTween.alphaCanvas(cg, 0, 0.5f).setOnComplete(() =>
        {
            container.gameObject.SetActive(false);
            isDisplayed = false;
        });
    }

    private void OnPressConfirm()
    {
        if (isDisplayed)
        {
            Hide();
        }
    }

    private void OnEnable()
    {
        EventManager.Input.onPressConfirm += OnPressConfirm;
    }

    private void OnDisable()
    {
        EventManager.Input.onPressConfirm -= OnPressConfirm;
    }
}
