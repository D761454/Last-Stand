using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    #region Framework Stuff
    //Reference to attached animator
    private Animator animator;

    //Reference to attached rigidbody 2D
    private Rigidbody2D rb;

    //The direction the player is moving in
    private Vector2 playerDirection;

    //The speed at which they're moving
    private float playerSpeed = 1f;

    [Header("Movement parameters")]
    //The maximum speed the player can move
    [SerializeField] private float playerMaxSpeed = 100f;
    #endregion

    [Header("STD Attributes")]
    [SerializeField] private int m_health;
    [SerializeField] private int m_maxHealth = 9;
    private ScoreSystem scoreSystem;
    GameObject scoreParent;

    [Header("Shooting")]
    [SerializeField] private float m_projectileSpeed = 7;
    [SerializeField] private GameObject m_bulletPrefab;
    [SerializeField] private Transform m_firePoint;
    [SerializeField] private int m_ammo;
    [SerializeField] private int m_maxAmmo;

    [SerializeField] private float m_fireTimer = 0.5f;
    [SerializeField] private float m_reloadTimer = 1.0f;
    private bool m_reloading = false;
    private float m_lastShot;

    /// <summary>
    /// When the script first initialises this gets called, use this for grabbing componenets
    /// </summary>
    private void Awake()
    {
        //Get the attached components so we can use them later
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Called after Awake(), and is used to initialize variables e.g. set values on the player
    /// </summary>
    private void Start()
    {
        m_ammo = m_maxAmmo;
        try
        {
            GameObject scoreParent = GameObject.Find("scoreSystem");
            if (scoreParent != null)
            {
                scoreSystem = scoreParent.GetComponent<ScoreSystem>();
            }
            else
            {
                Debug.Log("scoreSystem not Found!");
            }
        }
        catch (UnityException ex)
        {
            Debug.LogException(ex, this);
        }
    }

    /// <summary>
    /// When a fixed update loop is called, it runs at a constant rate, regardless of pc perfornamce so physics can be calculated properly
    /// </summary>
    private void FixedUpdate()
    {
        //Set the velocity to the direction they're moving in, multiplied
        //by the speed they're moving
        rb.velocity = playerDirection.normalized * (playerSpeed * playerMaxSpeed) * Time.fixedDeltaTime;
    }

    /// <summary>
    /// When the update loop is called, it runs every frame, ca run more or less frequently depending on performance. Used to catch changes in variables or input.
    /// </summary>
    private void Update()
    {
        // read input from WASD keys
        playerDirection.x = Input.GetAxis("Horizontal");
        playerDirection.y = Input.GetAxis("Vertical");

        // check if there is some movement direction, if there is something, then set animator flags and make speed = 1
        if (playerDirection.magnitude != 0)
        {
            animator.SetFloat("Horizontal", playerDirection.x);
            animator.SetFloat("Vertical", playerDirection.y);
            animator.SetFloat("Speed", playerDirection.magnitude);

            //And set the speed to 1, so they move!
            playerSpeed = 1f;
        }
        else
        {
            //Was the input just cancelled (released)? If so, set
            //speed to 0
            playerSpeed = 0f;

            //Update the animator too, and return
            animator.SetFloat("Speed", 0);
        }

        // Was the fire button pressed (mapped to Left mouse button or gamepad trigger)
        if (Input.GetButtonDown("Fire1") && (m_ammo > 0) && !m_reloading)
        {
            if (Time.time - m_lastShot >= m_fireTimer)
            {
                Fire();
                m_lastShot = Time.time;
            }
        }
        
        if ((Input.GetButtonDown("Reload") && (m_ammo < m_maxAmmo) && !m_reloading) || (!m_reloading && m_ammo == 0))
        {
            StartCoroutine(Reload());
        }
    }

    void Fire()
    {
        Vector3 mousePointOnScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 fireOrigin = m_firePoint.position;

        Vector2 fireDir = mousePointOnScreen - fireOrigin;

        GameObject bulletToSpawn = Instantiate(m_bulletPrefab, transform.position, Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan(fireDir.y / fireDir.x) - 90, Vector3.forward));
        // uses rad to degrees conversion for ease of use, atan used to give angle, V3.forward = z axis = desired rotation axis

        if (bulletToSpawn.GetComponent<Rigidbody2D>() != null)
        {
            bulletToSpawn.GetComponent<Rigidbody2D>().AddForce(fireDir.normalized * m_projectileSpeed, ForceMode2D.Impulse);
            m_ammo--;
            scoreSystem.AddScore(10);
        }
    }

    IEnumerator Reload()
    {
        m_reloading = true;
        yield return new WaitForSeconds(m_reloadTimer);
        m_ammo = m_maxAmmo;
        m_reloading = false;
    }
}
