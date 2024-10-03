using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components.HP;

namespace Enemy.AI
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 2f;        // Speed of enemy movement
        [SerializeField] float followSpeed = 3f;      // Speed of enemy when following player
        [SerializeField] float patrolPointA;          // Patrol point A (left)
        [SerializeField] float patrolPointB;          // Patrol point B (right)
        [SerializeField] float detectionRadius = 5f;  // Radius within which enemy detects player
        [SerializeField] float attackRadius = 1f;     // Distance from player where enemy attacks
        [SerializeField] Transform m_attackPoint;

        private bool movingRight = true;    // Direction of enemy
        private Transform player;           // Reference to the player
        private bool playerInRange = false; // Whether player is within detection radius
        public bool isAttacking = false;   // Whether enemy is attacking

        public Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.FindGameObjectWithTag("Player").transform;

            GetComponent<HPStats>().OnDeath += OnDeath;
        }

        void Update()
        {
            if (GetComponent<HPStats>().isAlive)
            {
                Patrol();
                DetectPlayer();
                if (playerInRange && !isAttacking)
                {
                    FollowPlayer();
                }

                if (movingRight)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }

                if (!GetComponent<SpriteRenderer>().flipX)
                {
                    Vector3 attackPointFront = new Vector3(0.87f, 0.12f, 0);
                    m_attackPoint.localPosition = attackPointFront;
                }
                else
                {
                    Vector3 attackPointBack = new Vector3(-0.87f, 0.12f, 0);
                    m_attackPoint.localPosition = attackPointBack;
                }
            }            
        }

        // Patrol between two points
        void Patrol()
        {
            if (!playerInRange)
            {
                isAttacking = false;
                if (movingRight)
                {
                    rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                    if (transform.position.x >= patrolPointB)
                        movingRight = false;
                }
                else
                {
                    rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                    if (transform.position.x <= patrolPointA)
                        movingRight = true;
                }
            }
        }

        // Detect player within radius
        void DetectPlayer()
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= detectionRadius)
            {
                playerInRange = true;
            }
            else
            {
                playerInRange = false;
            }
        }

        // Follow the player when detected
        void FollowPlayer()
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > attackRadius)
            {
                isAttacking = false;
                if (player.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(followSpeed, rb.velocity.y);
                    movingRight = true;
                }
                else
                {
                    rb.velocity = new Vector2(-followSpeed, rb.velocity.y);
                    movingRight = false;
                }
            }
            else
            {
                isAttacking = true;                
            }
        }

        void OnDeath()
        {
            Destroy(gameObject);
        }

        void OnDestroy()
        {
            GetComponent<HPStats>().OnDeath -= OnDeath;
        }

        // Optional Gizmos to visualize patrol points and detection radius in the editor
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(new Vector3(patrolPointA, transform.position.y, transform.position.z),
                            new Vector3(patrolPointB, transform.position.y, transform.position.z));
        }
    }
}