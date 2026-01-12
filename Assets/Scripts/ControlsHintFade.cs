using System.Collections;
using UnityEngine;

public class ControlsHintFade : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float visibleTime = 5f; // How long the text stays fully visible
    [SerializeField] private float fadeDuration = 1.5f; // How long it takes to fade

    private void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 1f;
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        yield return new WaitForSeconds(visibleTime);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        gameObject.SetActive(false); // hide after fade
    }
}
