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

    public IEnumerator SpawnEnemy(Transform spawnPt, GameObject enemy)
    {
        m_SECR = true;
        Vector3 spawn = spawnPt.position;
        Instantiate(enemy, spawn, Quaternion.identity);
        m_zToSpawn--;
        yield return new WaitForSeconds(3);
        m_SECR = false;
    }
}
