using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    

    public int damage = 1; // Amount of damage to deal

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.instance.PlayerInstance != null)
            {
                audioManager.PlaySFX(audioManager.damage);
                GameManager.instance.lives -= damage;

            }
        }
    }
}
