using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 enemyDirection;
    private float enemySpeed = 1f;

    [Header("Movement parameters")]
    [SerializeField] private float enemyMaxSpeed = 90f;

    [Header("STD Attributes")]
    [SerializeField] private int m_health;
    [SerializeField] private int m_maxHealth = 3;
    [SerializeField] private int level = 1;

    public Transform m_player;
    public float m_stoppingDistance;

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
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_health = m_maxHealth;
        m_player = FindObjectOfType<TopDownCharacterController>().transform;

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
        if (Vector2.Distance(transform.position, m_player.position) >= m_stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_player.position, enemySpeed * Time.deltaTime);
        }
    }
}
