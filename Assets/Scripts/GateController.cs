using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GateController : MonoBehaviour
{
    private ScoreSystem scoreSystem;
    GameObject scoreParent;
    BoxCollider2D payZone;
    [SerializeField] private int m_cost;

    // Start is called before the first frame update
    private void Start()
    {
        try
        {
            GameObject scoreParent = GameObject.Find("scoreSystem");
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

        try
        {
            payZone = GetComponentInChildren<BoxCollider2D>();
        }
        catch (UnityException ex)
        {
            Debug.LogException(ex, this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BuyDoor()
    {
        if (scoreSystem.score >= m_cost)
        {
            Destroy(gameObject);
        }
    }
}
