using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    private WeaponSystem weaponSystem;
    public TMPro.TextMeshProUGUI uiLabel;

    private void Start()
    {
        try
        {
            weaponSystem = GetComponent<WeaponSystem>();
        }
        catch (UnityException ex)
        {
            Debug.LogException(ex, this);
        }

        try
        {
            uiLabel = GameObject.Find("Ammo").GetComponent<TMPro.TextMeshProUGUI>();
        }
        catch (UnityException ex)
        {
            Debug.LogException(ex, this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        uiLabel.text = weaponSystem.GetAmmo().ToString();
    }
}
