using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GateController : MonoBehaviour
{
    private ScoreSystem scoreSystem;
    GameObject scoreParent;

    [SerializeField] GameObject spawnPoints;

    GameObject waveParent;
    WaveController waveController;

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

        try
        {
            waveParent = GameObject.Find("waveSystem");
            if (waveParent != null)
            {
                waveController = waveParent.GetComponent<WaveController>();
            }
            else
            {
                Debug.Log("wave system not Found!");
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
        if (Input.GetButtonDown("Buy") && uiLabel.text != "-")
        {
            m_purchase = true;
        }

        // reset purchase as it does not work in in buy door or on trigger stay 
        if (uiLabel.text == "-")
        {
            m_purchase = false;
        }
    }

    /// <summary>
    /// take score from player and remove door
    /// </summary>
    void BuyDoor()
    {
        scoreSystem.RemoveScore(m_cost);
        uiLabel.text = "-";
        gameObject.GetComponent<NavMeshObstacle>().carving = false;
        gateParent.SetActive(false);
        waveController.UpdateSpawns();
    }

    /// <summary>
    /// display UI when near door, allow player to buy door
    /// </summary>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            uiLabel.text = m_cost.ToString();
            if (m_purchase && (scoreSystem.score >= m_cost))
            {
                if(spawnPoints != null)
                {
                    spawnPoints.SetActive(true);
                }
                BuyDoor();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        uiLabel.text = "-";
    }
}
