using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    // Score
    private ScoreSystem scoreSystem;
    GameObject scoreParent;

    void Start()
    {
        try
        {
            scoreParent = GameObject.Find("scoreSystem");
            if (scoreParent != null)
            {
                scoreSystem = scoreParent.GetComponent<ScoreSystem>();
            }
            else
            {
                Debug.Log("scoreSystem not Found!");
            }
        }
        catch (UnityException ex)
        {
            Debug.LogException(ex, this);
        }
    }

    /// <summary>
    /// Handles Bullet Death and Score Gain
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Level")
        {
            Destroy(gameObject);
        }
        else if (collision.tag == "Enemy")
        {
            scoreSystem.AddScore(100);
            Destroy(gameObject);
        }
    }
}
