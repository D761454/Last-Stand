using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public int wave = 1;
    public int m_zToSpawn;

    public IEnumerator NextWave()
    {
        yield return new WaitForSeconds(1);
        // increment wave and calculate total enemies for wave
        wave++;
        m_zToSpawn = (wave * 2) + 5;
    }

    public void SpawnEnemy(Transform spawnPt, GameObject enemy)
    {
        Vector3 spawn = spawnPt.position;
        Instantiate(enemy, spawn, Quaternion.identity);
        m_zToSpawn--;
    }
}
