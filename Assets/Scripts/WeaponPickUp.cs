using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField] private GameObject m_weaponPrefab;

    private GameObject m_weaponHolder;

    private GameObject m_characterController;

    private void Start()
    {
        m_weaponHolder = GameObject.Find("WeaponsHolder");
        m_characterController = GameObject.Find("character");
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
            m_characterController.SendMessage("PickUpWeapon");
        }
    }
}
