using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private WaveSystem waveSystem;
    GameObject spawnParent;
    private float m_timeBetweenSpawn = 3.00f;
    private float m_spawnTime;

    Transform[] spawns;

    [SerializeField] private GameObject m_enemyPrefab;

    /// <summary>
    /// grab wave system to use its Coroutines
    /// update spawns as player unable to buy door and update spawns at the start
    /// </summary>
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

        UpdateSpawns();
    }

    /// <summary>
    /// checks whether 2 positions are the same, all non spawn points have pos (0.00, 0.00, 0.00)
    /// </summary>
    public bool SameAsSP(Transform transform)
    {
        return transform.position == spawnParent.transform.position;
    }

    /// <summary>
    /// updates an array of transforms for spawning enemies, is called on buying a door/gate
    /// </summary>
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

    /// <summary>
    /// checks if no enemies are present, all have been spawned and currently not loading next round - to then load next round.
    /// also checks if we have available enemies to spawn and an enemy spawn cooldown is not goung on - to then spawn a new enemy.
    /// </summary>
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && waveSystem.m_zToSpawn == 0 && !waveSystem.m_NWCR)
        {
            StartCoroutine(waveSystem.NextWave());
        }

        if (waveSystem.m_zToSpawn > 0 && (Time.time - m_spawnTime) > m_timeBetweenSpawn)
        {
            Transform spawnLocation = spawns[Random.Range(0, spawns.Length-1)];
            waveSystem.SpawnEnemy(spawnLocation, m_enemyPrefab);
            m_spawnTime = Time.time;
        }
    }
}