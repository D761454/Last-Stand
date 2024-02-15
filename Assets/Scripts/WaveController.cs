using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private WaveSystem waveSystem;
    GameObject waveParent;
    GameObject spawnParent;
    int m_timeBetweenSpawn = 3;

    Transform[] spawns;

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

        try
        {
            spawnParent = GameObject.Find("z_spawns");
            if (spawnParent != null)
            {
                Transform[] spawns;

                // included parent - child - grandchild
                spawns = spawnParent.GetComponentsInChildren<Transform>(true);

                //db
                foreach (Transform t in spawns)
                {
                    Debug.Log(t.position);
                }
            }
            else
            {
                Debug.Log("z_spawns not Found!");
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
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && waveSystem.m_zToSpawn == 0 && !waveSystem.m_NWCR)
        {
            StartCoroutine(waveSystem.NextWave());
        }

        if (waveSystem.m_zToSpawn > 0 && !waveSystem.m_SECR)
        {
            Transform spawnLocation = spawns[Random.Range(0, spawns.Length)];
            Debug.Log(spawnLocation.position);
            StartCoroutine(waveSystem.SpawnEnemy(spawnLocation, m_enemyPrefab));
        }
    }
}