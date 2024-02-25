using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffect : MonoBehaviour
{
    public Image FlashScreen;

    private Color white => Color.white;
    private Color none = new Color (0, 0, 0, 0);

    private void Start()
    {
        FlashScreen.gameObject.SetActive(false);
    }

    public void Begin()
    {
        FlashScreen.gameObject.SetActive(true);
        FlashScreen.color = none;
        StartCoroutine(Lerp(none, white, 5/3, 3));
    }

    public void End()
    {
        FlashScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    //public void Update()
    //{
    //    FlashScreen.color = Lerp(none, white, 3.5f);
    //}

    //public Color Lerp(Color initial, Color end, float lifespan)
    //{
    //    return Color.Lerp(initial, end, Mathf.Sin(Time.time * lifespan));
    //}

    IEnumerator Lerp(Color initial, Color end, float lifespan, int times)
    {
        float age = 0.0f;
        float frac = 0.0f;

        do
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
                times--;

                yield return null;
            } while (frac > 0.0f);
        } while (times > 0);
        
    }
}
