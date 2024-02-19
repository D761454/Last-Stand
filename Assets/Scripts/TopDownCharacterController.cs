using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

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
    [SerializeField] private float m_dashCooldown = 0.5f;
    private float m_lastDash;
    private float m_hitTime;
    private bool m_dead = false;

    // UI
    public WeaponSystem weaponSystem;


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
        if (!m_dead)
        {
            // read input from WASD keys
            playerDirection.x = Input.GetAxisRaw("Horizontal");
            playerDirection.y = Input.GetAxisRaw("Vertical");

            // check if there is some movement direction, if there is something, then set animator flags and make speed = 1
            if (playerDirection.magnitude != 0)
            {
                animator.SetFloat("Horizontal", playerDirection.x);
                animator.SetFloat("Vertical", playerDirection.y);
                animator.SetFloat("Speed", playerDirection.magnitude);

                //And set the speed to 1, so they move!
                playerSpeed = 1f;

                if (Input.GetButtonDown("Dash"))
                {
                    if ((Time.time - m_lastDash) >= m_dashCooldown)
                    {
                        animator.SetTrigger("Dashing");
                        m_lastDash = Time.time;
                    }
                }

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("dashTree"))
                {
                    playerSpeed = 2f;
                }
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
            if (Input.GetButtonDown("Fire1") && (weaponSystem.GetAmmo() > 0) && !weaponSystem.m_reloading)
            {
                if ((Time.time - weaponSystem.GetLastShot()) >= weaponSystem.GetFireTimer())
                {
                    weaponSystem.Fire();
                    weaponSystem.SetLastShot();
                }
            }

            if ((Input.GetButtonDown("Reload") && (weaponSystem.GetAmmo() < weaponSystem.GetMaxAmmo()) && !weaponSystem.m_reloading) || (!weaponSystem.m_reloading && weaponSystem.GetAmmo() == 0))
            {
                StartCoroutine(weaponSystem.Reload());
            }
        }
        
    }

    /// <summary>
    /// Uses a Trigger Collider to detect enemy collision
    /// </summary>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if ((Time.time - m_hitTime) >= 1.00f)
            {
                m_health--;
                m_hitTime = Time.time;
            }
        }

        if (m_health <= 0)
        {
            m_dead = true;
        }
    }
}
