using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private WaveSystem waveSystem;
    GameObject waveParent;
    int m_timeBetweenSpawn = 3;

    [SerializeField] private GameObject m_enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {


        try
        {
            waveParent = GameObject.Find("waveSystem");
            if (waveParent != null)
            {
                waveSystem = waveParent.GetComponent<WaveSystem>();
            }
            else
            {
                Debug.Log("waveSystem not Found!");
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
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && waveSystem.m_zToSpawn == 0)
        {
            StartCoroutine(waveSystem.NextWave());
        }
    }
}