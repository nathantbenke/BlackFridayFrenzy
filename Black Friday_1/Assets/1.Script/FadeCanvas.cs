using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FadeCanvas : MonoBehaviour
{
    public Image bgImg;

  

    public void FadeOut(Action endCallback)
    {
        gameObject.SetActive(true);
        StartCoroutine(CoFadeOut(endCallback));
    }

 
    IEnumerator CoFadeOut(Action endCallback)
    {
        float alpha = 0;
        while (true)
        {
            bgImg.color = new Color(0, 0, 0, alpha);

            if (alpha >= 1)
            {
                break;
            }

            yield return null;
            alpha += Time.deltaTime;
        }

        FadeIn();
        endCallback.Invoke();
    }

    public void FadeIn()
    {
        StartCoroutine(CoFadeIn());
    }

 
    IEnumerator CoFadeIn()
    {
        float alpha = 1;
        while (true)
        {
            bgImg.color = new Color(0, 0, 0, alpha);

            if (alpha <= 0)
            {
                break;
            }

            yield return null;
            alpha -= Time.deltaTime;
        }

        gameObject.SetActive(false);
    }
}
