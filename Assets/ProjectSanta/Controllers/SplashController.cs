using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashController : MonoBehaviour
{
    public Image splash;
    float alpha = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        while(alpha <= 1f)
        {
            alpha += 0.05f;
            Color c = splash.color;
            c.a = alpha;
            splash.color = c;

            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(2f);

        while(alpha > 0)
        {
            alpha -= 0.05f;
            Color c = splash.color;
            c.a = alpha;
            splash.color = c;

            yield return new WaitForSeconds(0.05f);
        }

        SceneManager.LoadScene("Menu");
    }

}
