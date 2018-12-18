using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashController : MonoBehaviour
{
    public Image splash, scroll;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        Coroutine splashFade = StartCoroutine(FadeImage(splash));

        yield return splashFade;
        
        Coroutine scrollFade = StartCoroutine(FadeImage(scroll));

        yield return scrollFade;

        SceneManager.LoadScene("Menu");
    }

    IEnumerator FadeImage(Image img)
    {
        float alpha = 0;
        while (alpha <= 1f)
        {
            alpha += 0.05f;
            Color c = img.color;
            c.a = alpha;
            img.color = c;

            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(2f);

        while (alpha > 0)
        {
            alpha -= 0.05f;
            Color c = img.color;
            c.a = alpha;
            img.color = c;

            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);

        img.enabled = false;
    }

}
