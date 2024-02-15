using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private WaveSystem waveSystem;
    GameObject spawnParent;
    int m_timeBetweenSpawn = 3;

    Transform[] spawns;

    [SerializeField] private GameObject m_enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            waveSystem = GetComponent<WaveSystem>();
        }
        catch (UnityException ex)
        {
            Debug.LogException(ex, this);
        }
    }

    public bool SameAsSP(Transform transform)
    {
        return transform.position == spawnParent.transform.position;
    }

    public void UpdateSpawns()
    {
        try
        {
            spawnParent = GameObject.Find("z_spawns");
            if (spawnParent != null)
            {
                // included parent - child - grandchild
                var transforms = new HashSet<Transform>(spawnParent.GetComponentsInChildren<Transform>());

                // remove parent and 1st layer children
                transforms.RemoveWhere(SameAsSP);

                spawns = transforms.ToArray();
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