using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform destination; // Assign the other portal's Transform in the Inspector
    GameObject player;
    Rigidbody2D playerRb;

    private void Awake()
    {
        // Ensure you get the first player if there are multiple tagged objects
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            player = players[0]; // Assign the first player in the array
        }
        else
        {
            Debug.LogWarning("No GameObject with the tag 'Player' found.");
        }

        playerRb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Check if the player is not already at the destination to avoid immediate re-teleportation
            if (Vector2.Distance(player.transform.position, destination.position) > 0.3f)
            {
                player.transform.position = destination.position; // Teleport to the destination portal
                playerRb.velocity = Vector2.zero;
            }
        }
    }

    IEnumerator MoveInPortal ()
    {
        float timer = 0;
        while (timer < 0.5f)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, 3 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }

}
