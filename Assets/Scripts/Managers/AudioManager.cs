using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip level1;
    public AudioClip damage;
    public AudioClip gameover;
    public AudioClip title;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            musicSource.clip = title;
            musicSource.Play();
        } else if (SceneManager.GetActiveScene().name == "Game")
        {
            musicSource.clip = level1;
            musicSource.Play();
        } else if (SceneManager.GetActiveScene().name == "Gameover")
        {
            musicSource.clip = gameover;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
