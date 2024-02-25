using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLeavingCamera : MonoBehaviour
{
    [SerializeField] private float m_lifespan;
    private float m_fireTime;
    private void Start()
    {
        m_fireTime = Time.time;  
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if ((Time.time - m_fireTime) > m_lifespan)
        {
            Destroy(gameObject);
        }
    }
}
