using System.Collections;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform destination; // Assign the destination portal in the Inspector
    private GameObject player;
    private Rigidbody2D playerRb;
    private bool canTeleport = true; // Prevent immediate re-teleportation
    private PortalController destinationPortal; // Reference to the destination portal's script

    private void Awake()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            player = players[0];
            playerRb = player.GetComponent<Rigidbody2D>();
            if (playerRb == null)
            {
                Debug.LogError("No Rigidbody2D found on player!");
            }
        }
        else
        {
            Debug.LogWarning("No player found with the tag 'Player'.");
        }

        if (destination != null)
        {
            Debug.Log("Destination portal for " + gameObject.name + " is: " + destination.name);
            destinationPortal = destination.GetComponent<PortalController>();
        }
        else
        {
            Debug.LogError("Destination not assigned for portal: " + gameObject.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && canTeleport)
        {
            Debug.Log("Player entered portal: " + gameObject.name);

            if (destination != null)
            {
                StartCoroutine(TeleportPlayer());
            }
            else
            {
                Debug.LogError("Destination is not assigned for portal: " + gameObject.name);
            }
        }
    }

    private IEnumerator TeleportPlayer()
    {
        canTeleport = false;

        // Disable the destination portal temporarily to prevent immediate re-triggering
        if (destinationPortal != null)
        {
            destinationPortal.DisableTeleport();
        }

        // Reset velocity to prevent unintended motion
        if (playerRb != null)
        {
            playerRb.velocity = Vector2.zero;
        }

        // Teleport the player to the destination
        player.transform.position = destination.position + new Vector3(0, 0.1f, 0); // Slightly above ground
        Debug.Log("Player teleported to: " + destination.name);

        // Wait for a cooldown before this portal can be used again
        yield return new WaitForSeconds(3f);
        canTeleport = true;
    }

    public void DisableTeleport()
    {
        StartCoroutine(DisableTeleportCoroutine());
    }

    private IEnumerator DisableTeleportCoroutine()
    {
        canTeleport = false;
        yield return new WaitForSeconds(3f); // Match the cooldown time
        canTeleport = true;
    }
}
