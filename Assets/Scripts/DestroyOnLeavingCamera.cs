using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLeavingCamera : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Level")
        {
            Destroy(gameObject);
        }
    }
}
