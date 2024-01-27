using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    private AmmoSystem ammoSystem;
    public TMPro.TextMeshProUGUI uiLabel;

    private void Start()
    {
        try
        {
            ammoSystem = GetComponent<AmmoSystem>();
        }
        catch (UnityException ex)
        {
            Debug.LogException(ex, this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        uiLabel.text = ammoSystem.GetAmmo().ToString();
    }
}
