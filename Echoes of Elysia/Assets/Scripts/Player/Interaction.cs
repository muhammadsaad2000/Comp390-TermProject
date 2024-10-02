using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.PixelArtPlatformer_VillageProps;

public class Interaction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.tag == "Chest")
        {
            Debug.Log("Chest!");
            if (Input.GetKeyDown("e"))
            {
                Debug.Log("Open!");
                collision.gameObject.GetComponent<Chest>().Open();
            }
        }*/
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Chest")
        {
            if (!other.gameObject.GetComponent<Chest>().IsOpened)
            {
                if (Input.GetKey("e"))
                {               
                    Debug.Log("Open!");
                    other.gameObject.GetComponent<Chest>().Open();
                }
            }
        }
    }
}
