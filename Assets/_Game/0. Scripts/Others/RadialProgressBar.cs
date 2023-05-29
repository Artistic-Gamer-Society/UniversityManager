using System;
using System.Collections;
using Unity.Plastic.Antlr3.Runtime.Misc;
using UnityEngine;

public class RadialProgressBar : MonoBehaviour
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private string fillAmountPropertyName = "_FillAmount";
    [SerializeField] private float minFillAmount = 0f;
    [SerializeField] private float maxFillAmount = 1f;
    [SerializeField] private float transitionDuration = 1f;

    private Material material;
    private Coroutine fillCoroutine;

    public static event Action<Student> OnProgressComplete;
    internal Student student;

    private void Awake()
    {
        if (renderer != null)
        {
            material = renderer.material;
            renderer.sharedMaterial = material;
            material.SetFloat(fillAmountPropertyName, 0);
        }
    }
    private void OnEnable()
    {
        SetFillAmount(1);
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        material.SetFloat(fillAmountPropertyName, 0);
    }
    public void SetFillAmount(float fillAmount)
    {
        fillAmount = Mathf.Clamp(fillAmount, minFillAmount, maxFillAmount);

        if (fillCoroutine != null)
        {
            StopCoroutine(fillCoroutine);
        }

        fillCoroutine = StartCoroutine(TransitionFillAmount(fillAmount, transitionDuration));
    }
    private IEnumerator TransitionFillAmount(float targetFillAmount, float duration)
    {
        float initialFillAmount = material.GetFloat(fillAmountPropertyName);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float currentFillAmount = Mathf.Lerp(initialFillAmount, targetFillAmount, t);
            material.SetFloat(fillAmountPropertyName, currentFillAmount);
            yield return null;
        }
        material.SetFloat(fillAmountPropertyName, targetFillAmount);
        fillCoroutine = null;
        OnProgressComplete?.Invoke(student);
        gameObject.SetActive(false);
    }
}