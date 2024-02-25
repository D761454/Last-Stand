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
        FlashScreen.color = none;
        FlashScreen.gameObject.SetActive(true);
    }

    public void End()
    {
        FlashScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Update()
    {
        FlashScreen.color = Lerp(none, white, 3.5f);
    }

    public Color Lerp(Color initial, Color end, float time)
    {
        return Color.Lerp(initial, end, Mathf.Sin(Time.time * time));
    }
}
