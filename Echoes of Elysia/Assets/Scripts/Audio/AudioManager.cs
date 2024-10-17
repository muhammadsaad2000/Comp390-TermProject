using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------------Audio Source--------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-------------Audio Clip--------------")]
    public AudioClip menu_background;
    public AudioClip game_background;
    public AudioClip buttons;
    public AudioClip death;
    public AudioClip attack;
    public AudioClip walk;
    public AudioClip jump;
    public AudioClip hurt;
    public AudioClip chest;
    public AudioClip block;
    public AudioClip roll;


    public void Start()
    {
        musicSource.clip = game_background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
