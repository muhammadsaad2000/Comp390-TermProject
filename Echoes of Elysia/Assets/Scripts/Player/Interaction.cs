using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.PixelArtPlatformer_VillageProps;
using Components.HP;

public class Interaction : MonoBehaviour
{
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        HPStats hp = GetComponent<HPStats>();
        if (collision.gameObject.tag == "Spikes"){
            Debug.Log("Ouch!");
            audioManager.PlaySFX(audioManager.hurt);
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
                    audioManager.PlaySFX(audioManager.chest);
                    other.gameObject.GetComponent<Chest>().Open();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var item = collision.GetComponent<Item>();

        if (item)
        {
            GetComponent<HeroKnight>().inventory.AddItem(item.item, 1);
            Destroy(collision.gameObject);
        }
    }
}
