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
                // go through each child getting their transform component then going through each component to add the parent game object to an array
                // done as cannot get component game object
                var buildings = new ArrayList();
                Transform[] tfs;
                tfs = spawnParent.GetComponentsInChildren<Transform>();
                foreach (Transform t in tfs)
                {
                    buildings.Add(t.gameObject);
                }

                foreach (GameObject s in buildings)
                {
                    spawns = s.GetComponentsInChildren<Transform>();
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
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && waveSystem.m_zToSpawn == 0)
        {
            StartCoroutine(waveSystem.NextWave());
        }

        // fix
        Transform spawnLocation = spawns[Random.Range(0, spawns.Length)];

        waveSystem.SpawnEnemy(spawnLocation, m_enemyPrefab);
    }
}