using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI uiLabel;
    private WaveSystem waveSystem;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            waveSystem = GetComponent<WaveSystem>();
        }
        catch (UnityException ex)
        {
            Debug.LogException(ex, this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //uiLabel.text = waveSystem.wave.ToString();
    }
}
