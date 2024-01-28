using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GateController : MonoBehaviour
{
    private ScoreSystem scoreSystem;
    GameObject scoreParent;

    GameObject costParent;
    private TMPro.TextMeshProUGUI uiLabel;
    public bool m_purchase = false;

    [SerializeField] GameObject gateParent;
    [SerializeField] private int m_cost;

    // Start is called before the first frame update
    private void Start()
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

        try
        {
            costParent = GameObject.Find("Cost");
            if (costParent != null)
            {
                uiLabel = costParent.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                Debug.Log("TextMeshProUGUI not found!");
            }
        }
        catch (UnityException ex)
        {
            Debug.LogException(ex, this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Buy"))
        {
            m_purchase = true;
        }
    }

    void BuyDoor()
    {
        scoreSystem.RemoveScore(m_cost);
        uiLabel.text = "-";
        Destroy(gateParent);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            uiLabel.text = m_cost.ToString();
            if (m_purchase && (scoreSystem.score >= m_cost))
            {
                m_purchase = false;
                BuyDoor();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        uiLabel.text = "-";
    }
}
