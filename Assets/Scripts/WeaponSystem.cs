using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private float m_projectileSpeed = 7;
    [SerializeField] private GameObject m_bulletPrefab;
    [SerializeField] private Transform m_firePoint;

    [SerializeField] private float m_fireTimer = 0.5f;
    [SerializeField] private float m_reloadTimer = 1.0f;
    public bool m_reloading = false;
    private float m_lastShot;

    [SerializeField] private int m_ammo;
    [SerializeField] private int m_maxAmmo;
    [SerializeField] private bool m_spread = false;

    private void Start()
    {
        m_ammo = m_maxAmmo;
        m_firePoint = GameObject.Find("character").GetComponent<Transform>();
    }

    public int GetAmmo()
    {
        return m_ammo;
    }

    public int GetMaxAmmo()
    {
        return m_maxAmmo;
    }

    public float GetFireTimer()
    {
        return m_fireTimer;
    }

    public float GetReloadTimer()
    {
        return m_reloadTimer;
    }

    public float GetLastShot()
    {
        return m_lastShot;
    }

    public void SetLastShot()
    {
        m_lastShot = Time.time;
    }

    /// <summary>
    /// Handles Bullet Instantiation, propulsion and ammo removal
    /// </summary>
    public void Fire()
    {
        if (!m_spread)
        {
            Vector3 mousePointOnScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 fireOrigin = m_firePoint.position;

            Vector2 fireDir = mousePointOnScreen - fireOrigin;

            GameObject bulletToSpawn = Instantiate(m_bulletPrefab, fireOrigin, Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan(fireDir.y / fireDir.x) - 90, Vector3.forward), GameObject.Find("BulletHolder").transform);
            // uses rad to degrees conversion for ease of use, atan used to give angle, V3.forward = z axis = desired rotation axis

            if (bulletToSpawn.GetComponent<Rigidbody2D>() != null)
            {
                bulletToSpawn.GetComponent<Rigidbody2D>().AddForce(fireDir.normalized * m_projectileSpeed, ForceMode2D.Impulse);
                m_ammo--;
            }
        }
        else
        {
            Vector3 mousePointOnScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 fireOrigin = m_firePoint.position;

            for (int i = 0; i < 5; i++)
            {
                int randAngle = Random.Range(-10, 10);
                Vector3 forceOffset = new Vector2(randAngle, randAngle);

                Vector2 fireDir = mousePointOnScreen - fireOrigin + forceOffset;

                GameObject bulletToSpawn = Instantiate(m_bulletPrefab, fireOrigin, Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan(fireDir.y / fireDir.x) - (90 + randAngle), Vector3.forward), GameObject.Find("BulletHolder").transform);
                // uses rad to degrees conversion for ease of use, atan used to give angle, V3.forward = z axis = desired rotation axis

                if (bulletToSpawn.GetComponent<Rigidbody2D>() != null)
                {
                    bulletToSpawn.GetComponent<Rigidbody2D>().AddForce(fireDir.normalized * m_projectileSpeed, ForceMode2D.Impulse);
                }
            }
            m_ammo--;
        }
    }

    /// <summary>
    /// Handles Reloading
    /// </summary>
    public IEnumerator Reload()
    {
        m_reloading = true;
        yield return new WaitForSeconds(m_reloadTimer);
        m_ammo = m_maxAmmo;
        m_reloading = false;
    }
}