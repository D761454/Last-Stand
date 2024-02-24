using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpResetter : MonoBehaviour
{
    private GameObject pickUpParent;

    // Start is called before the first frame update
    void Start()
    {
        pickUpParent = GameObject.Find("PickUps");
    }

    public void Reset()
    {
        foreach (Transform child in pickUpParent.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
