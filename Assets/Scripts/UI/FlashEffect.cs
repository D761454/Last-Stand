using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffect : MonoBehaviour
{
    public Image FlashScreen;

    private Color white => Color.white;
    private Color none = new Color (0, 0, 0, 0); 

    // Update is called once per frame
    public void Flash()
    {
        FlashScreen.color = Lerp(none, white, 1);
    }

    public Color Lerp(Color initial, Color end, float time)
    {
        return Color.Lerp(initial, end, Mathf.Sin(Time.time * time));
    }
}
