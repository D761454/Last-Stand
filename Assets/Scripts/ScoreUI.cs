using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private ScoreSystem scoreSystem;
    public TMPro.TextMeshProUGUI uiLabel;

    private void Start()
    {
        try
        {
            scoreSystem = GetComponent<ScoreSystem>();
        }
        catch (UnityException ex)
        {
            Debug.LogException(ex, this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        uiLabel.text = scoreSystem.score.ToString();
    }
}
