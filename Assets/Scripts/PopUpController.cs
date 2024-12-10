using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class PopUpController : MonoBehaviour
{
    public float fadeDuration = 1f; // Duration of fade in seconds
    public float scaleDuration = 1f;
    public float duration = 1f;
    public CanvasGroup canvasGroup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartPopUpFadeOut()
    {
        StartCoroutine(PopUpFadeOut());
    }

    IEnumerator PopUpFadeOut()
    {
        float startAlpha = canvasGroup.alpha;
        StartCoroutine(CycleOfDuration(startAlpha, 1));
        yield return new WaitForSeconds(2f);
        StartCoroutine(CycleOfDuration(1, 0));

    }

    public void StartScaleDown()
    {
        StartCoroutine(ScaleDown());
    }
    public void StartDisappear()
    {
        StartCoroutine(FadeAndScale());
    }

    IEnumerator CycleOfDuration(float startAlpha, float finishAlfa)
    {
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, finishAlfa, normalizedTime);
            yield return null;
        }
        // Ensure the alpha is fully set to 1 or 0 at the end
        canvasGroup.alpha = finishAlfa;
    }

    private IEnumerator ScaleDown()
    {
        Vector3 originalScale = transform.localScale;

        for (float t = 0; t < scaleDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / scaleDuration;
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, normalizedTime);
            yield return null;
        }

        // Ensure the scale is fully set to zero at the end
        transform.localScale = Vector3.zero;

        // Optionally disable or destroy the object
        //gameObject.SetActive(false);
    }
    private IEnumerator FadeAndScale()
    {
        Vector3 originalScale = transform.localScale;
        float startAlpha = canvasGroup.alpha;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;

            // Fade out
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, normalizedTime);

            // Scale down
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, normalizedTime);

            yield return null;
        }

        // Ensure the object is fully invisible and scaled down
        canvasGroup.alpha = 0;
        transform.localScale = Vector3.zero;

        // Optionally disable or destroy the object
        //gameObject.SetActive(false);
    }
}
