using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void PlayGame()
    {
        audioManager.PlaySFX(audioManager.buttons);
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        audioManager.PlaySFX(audioManager.buttons);
        Application.Quit();
    }
}