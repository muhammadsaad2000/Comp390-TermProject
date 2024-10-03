using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components.HP;
using Enemy.AI;
using Cainos.PixelArtPlatformer_VillageProps;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] int attackDmg = 10;

    [SerializeField] float attackCooldown = 1f;  // Cooldown period between attacks

    private bool canAttack = true; // Tracks if the enemy can attack

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<HPStats>().isAlive)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log($"Attack {other.gameObject.name}");
                    other.gameObject.GetComponent<HPStats>().TakeDamage(attackDmg);
                }
            }
        }

        if (other.tag == "Player" && canAttack)
        {
            StartCoroutine(AttackPlayer(other.transform));
        }
    }

    // Attack the player
    IEnumerator AttackPlayer(Transform player)
    {
        // Set attack to false so it can't happen during cooldown
        canAttack = false;

        GetComponentInParent<EnemyAI>().isAttacking = true;
        GetComponentInParent<EnemyAI>().rb.velocity = Vector2.zero; // Stop moving when attacking
        Debug.Log("Enemy Attacking!");

        // Apply damage to the player
        if (player.GetComponent<HeroKnight>().isShielded)
        {
            player.GetComponent<HPStats>().TakeDamage(0);
        }
        else
        {
            player.GetComponent<HPStats>().TakeDamage(attackDmg);
        }      

        // Wait for the cooldown duration
        yield return new WaitForSeconds(attackCooldown);

        // Reset attack and allow attacking again
        canAttack = true;
        GetComponentInParent<EnemyAI>().isAttacking = false;
    }
}
