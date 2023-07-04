using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingFadeEffectController : SingletonPersistent<LoadingFadeEffectController>
{
    [SerializeField]
    private Image transitionBackgroundImage;

    [SerializeField]
    [Range(0f, 5f)]
    private float fadeDuration;

    [SerializeField]
    [Range(0f, 0.1f)]
    private float fadeInterval;

    public static bool beginLoad;

    IEnumerator FadeAllEffect()
    {
        yield return StartCoroutine(FadeInEffect());

        yield return new WaitForSeconds(1);

        yield return StartCoroutine(FadeOutEffect());
    }

    private IEnumerator FadeInEffect()
    {

        transitionBackgroundImage.gameObject.SetActive(true);

        var backgroundColor = transitionBackgroundImage.color;

        backgroundColor.a = 0;

        while (backgroundColor.a <= 1)
        {
            yield return new WaitForSeconds(fadeInterval);

            backgroundColor.a += fadeInterval / fadeDuration;

            transitionBackgroundImage.color = backgroundColor;
        }
        beginLoad = true;
    }

    private IEnumerator FadeOutEffect()
    {
        beginLoad = false;

        var backgroundColor = transitionBackgroundImage.color;

        while (backgroundColor.a >= 0)
        {
            yield return new WaitForSeconds(fadeInterval);

            backgroundColor.a -= fadeInterval / fadeDuration;

            transitionBackgroundImage.color = backgroundColor;
        }

        transitionBackgroundImage.gameObject.SetActive(false);

    }

    public void FadeIn()
    {
        StartCoroutine(FadeInEffect());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutEffect());
    }

    public void FadeAll()
    {
        StartCoroutine(FadeAllEffect());
    }

}
