using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffect : MonoBehaviour
{
    public Image FlashScreen;

    private Color white = Color.white;
    private Color none = new Color (0, 0, 0, 0);

    public void Begin()
    {
        StartCoroutine(Lerp(none, white, 5/3, 3));
    }

    IEnumerator Lerp(Color initial, Color end, float lifespan, int times)
    {
        float age = 0.0f;
        float frac = 0.0f;

        while (times > 0) 
        {
            do
            {
                frac = age / lifespan;
                age += Time.deltaTime;

                FlashScreen.color = Color.Lerp(initial, end, frac);

                yield return null;
            } while (frac < 1.0f);

            do
            {
                frac = age / lifespan;
                age -= Time.deltaTime;

                FlashScreen.color = Color.Lerp(initial, end, frac);

                yield return null;
            } while (frac > 0.0f);
            times--;
            yield return null;
        }
    }
}
