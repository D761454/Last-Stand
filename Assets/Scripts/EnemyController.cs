using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [Header("STD Attributes")]
    [SerializeField] private int m_health;
    [SerializeField] private int m_maxHealth = 3;
    [SerializeField] private int level = 1;

    private Transform m_target;
    NavMeshAgent m_agent;

    enum EnemyStates
    {
        Idle,
        Walk
    }
    EnemyStates m_enemyStates;

    private ScoreSystem scoreSystem;
    GameObject scoreParent;

    private void Awake()
    {
        //Get the attached components so we can use them later
        animator = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_health = m_maxHealth;

        m_agent = GetComponent<NavMeshAgent>();
        m_target = FindObjectOfType<TopDownCharacterController>().transform;

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

    // Update is called once per frame
    void Update()
    {
        m_agent.SetDestination(m_target.position);

        if (m_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage()
    {
        m_health -= 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            TakeDamage();
        }
    }
}
