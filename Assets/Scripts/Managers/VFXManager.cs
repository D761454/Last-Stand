using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager instance;
    [SerializeField] private GameObject m_explosionPrefab;
    [SerializeField] private GameObject m_damagePrefab;
    [SerializeField] private GameObject m_bulletDestroyPrefab;

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

    public static GameObject CreateProjHit(Vector3 position, float deathTime = 0.25f)
    {
        if (instance == null)
        {
            Debug.Log("Instance not set for Proj Particle.");
            return null;
        }

        GameObject projDeath = Instantiate(instance.m_bulletDestroyPrefab, position, Quaternion.identity);

        Destroy(projDeath, deathTime);

        return projDeath;
    }

    public static GameObject CreatePlayerHit(Vector3 position, float deathTime = 0.5f)
    {
        if (instance == null)
        {
            Debug.Log("Instance not set for Damage Particle.");
            return null;
        }

        GameObject pDamage = Instantiate(instance.m_damagePrefab, position, Quaternion.identity);

        Destroy(pDamage, deathTime);

        return pDamage;
    }
}
