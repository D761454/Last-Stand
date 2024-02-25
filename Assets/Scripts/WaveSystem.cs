using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public int wave = 0;
    public int m_zToSpawn = 0;
    public bool m_NWCR = false;

    private PickUpResetter m_reset;
    private FlashEffect m_flash;

    private void Start()
    {
        m_reset = gameObject.GetComponent<PickUpResetter>();
        m_flash = gameObject.GetComponent<FlashEffect>();
    }

    public IEnumerator NextWave()
    {
        m_NWCR = true;
        yield return new WaitForSeconds(5);
        // increment wave and calculate total enemies for wave
        wave++;
        m_zToSpawn = (wave * 2) + 5;
        m_flash.Flash();
        m_NWCR = false;

        if (wave % 5 == 0)
        {
            m_reset.Reset();
        }
    }

    public void SpawnEnemy(Transform spawnPt, GameObject enemy)
    {
        Vector3 spawn = spawnPt.position;
        Instantiate(enemy, spawn, Quaternion.identity, GameObject.Find("EnemyHolder").transform);
        m_zToSpawn--;
    }
}
