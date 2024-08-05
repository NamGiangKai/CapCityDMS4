using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float speed; // Speed at which the background scrolls
    public GameObject[] backgrounds; // Array of background images
    public float fadeDuration = 1f; // Duration of the fade animation

    private int currentBackgroundIndex = 0;
    private Renderer bgRenderer;
    private bool isTransitioning = false;

    void Start()
    {
        foreach (GameObject background in backgrounds)
        {
            background.SetActive(false); // Deactivate all backgrounds initially
        }
        if (backgrounds.Length > 0)
        {
            backgrounds[currentBackgroundIndex].SetActive(true); // Activate the first background
            bgRenderer = backgrounds[currentBackgroundIndex].GetComponent<Renderer>();
        }
    }

    void Update()
    {
        if (bgRenderer != null)
        {
            bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
        }
    }

    public void TransitionToNextBackground()
    {
        if (backgrounds.Length > 0 && !isTransitioning)
        {
            StartCoroutine(FadeTransition());
        }
    }

    private IEnumerator FadeTransition()
    {
        isTransitioning = true;

        GameObject currentBackground = backgrounds[currentBackgroundIndex];
        currentBackgroundIndex = (currentBackgroundIndex + 1) % backgrounds.Length;
        GameObject nextBackground = backgrounds[currentBackgroundIndex];

        nextBackground.SetActive(true);
        Renderer nextRenderer = nextBackground.GetComponent<Renderer>();
        Color nextColor = nextRenderer.material.color;
        Color currentColor = bgRenderer.material.color;

        // Fade out the current background
        yield return StartCoroutine(FadeOut(currentBackground, bgRenderer, fadeDuration));

        // Set the background to fully invisible
        bgRenderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0f);

        // Fade in the next background
        yield return StartCoroutine(FadeIn(nextBackground, nextRenderer, fadeDuration));

        currentBackground.SetActive(false);
        bgRenderer = nextRenderer;

        isTransitioning = false;
    }

    private IEnumerator FadeOut(GameObject background, Renderer renderer, float duration)
    {
        float timer = 0f;
        Color color = renderer.material.color;

        while (timer < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / duration);
            renderer.material.color = new Color(color.r, color.g, color.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        renderer.material.color = new Color(color.r, color.g, color.b, 0f);
    }

    private IEnumerator FadeIn(GameObject background, Renderer renderer, float duration)
    {
        float timer = 0f;
        Color color = renderer.material.color;

        while (timer < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / duration);
            renderer.material.color = new Color(color.r, color.g, color.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        renderer.material.color = new Color(color.r, color.g, color.b, 1f);
    }
}
