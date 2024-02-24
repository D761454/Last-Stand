using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    private TopDownCharacterController m_characterController;

    // Start is called before the first frame update
    void Start()
    {
        m_characterController = GameObject.Find("character").GetComponent<TopDownCharacterController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_characterController.Heal();
            gameObject.SetActive(false);
        }
    }
}
