using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager instance;
    [SerializeField] private GameObject m_explosionPrefab;

    private void Awake()
    {
        // singleton implementation
        if (instance == null)
        {
            instance = this;
        }
    }

    public static GameObject CreateExplosion(Vector3 position, float deathTime = 0.5f)
    {
        if (instance == null)
        {
            Debug.Log("Instance not set for Explosion.");
            return null;
        }

        GameObject explosion = Instantiate(instance.m_explosionPrefab, position, Quaternion.identity);

        Destroy(explosion, deathTime);

        return explosion;
    }
}
