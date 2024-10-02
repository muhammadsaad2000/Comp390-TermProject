using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.PixelArtPlatformer_VillageProps;
using Components.HP;

public class Interaction : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        HPStats hp = GetComponent<HPStats>();
        if (collision.gameObject.tag == "Spikes"){
            Debug.Log("Ouch!");
            GetComponent<HeroKnight>().TakeDamageAnim();
            hp.TakeDamage(10);
        }
    }

    void OnTriggerStay2D(Collider2D other)
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
