using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickUpResetter : MonoBehaviour
{
    public Transform[] PU;

    private GameObject pickUpParent;

    private bool IsOrig(Transform transform)
    {
        return transform.position == gameObject.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            pickUpParent = GameObject.Find("PickUps");
            if (pickUpParent != null)
            {
                // included parent - child - grandchild
                var transforms = new HashSet<Transform>(pickUpParent.GetComponentsInChildren<Transform>());

                // remove parent and 1st layer children
                transforms.RemoveWhere(IsOrig);

                PU = transforms.ToArray();
            }
            else
            {
                Debug.Log("pickUps not Found!");
            }
        }
        catch (UnityException ex)
        {
            Debug.LogException(ex, this);
        }
    }
}
