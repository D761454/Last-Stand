using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField] private GameObject m_weaponPrefab;

    private GameObject m_weaponHolder;

    private TopDownCharacterController m_characterController;

    private void Start()
    {
        m_weaponHolder = GameObject.Find("WeaponsHolder");
        m_characterController = GameObject.Find("character").GetComponent<TopDownCharacterController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            foreach(Transform child in m_weaponHolder.transform)
            {
                Destroy(child.gameObject);
            }
            Instantiate(m_weaponPrefab, m_weaponHolder.transform);
            m_characterController.weaponSystem = m_weaponHolder.GetComponentInChildren<WeaponSystem>();
            gameObject.SetActive(false);
        }
    }
}
