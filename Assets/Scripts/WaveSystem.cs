using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public int wave = 0;
    public int m_zToSpawn = 0;
    public bool m_NWCR = false;
    public bool m_SECR = false;

    public IEnumerator NextWave()
    {
        m_NWCR = true;
        yield return new WaitForSeconds(5);
        // increment wave and calculate total enemies for wave
        wave++;
        m_zToSpawn = (wave * 2) + 5;
        m_NWCR = false;
    }

    public void SpawnEnemy(Transform spawnPt, GameObject enemy)
    {
        Vector3 spawn = spawnPt.position;
        Instantiate(enemy, spawn, Quaternion.identity, GameObject.Find("EnemyHolder").transform);
        m_zToSpawn--;
    }
}
