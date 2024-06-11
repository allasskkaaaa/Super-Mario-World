using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    private SpriteRenderer spriteRenderer;
    public float detectionRange = 5f; //Setting Detection Range
    private Animator anim;


    void Start()
    {
        // Get the SpriteRenderer component attached to the enemy
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            //Checks the distance between the enemy and the player
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                anim.SetBool("Active",true);

                // Check if the player's position is to the left or right of the enemy
                if (player.position.x > transform.position.x)
                {
                    // Player is to the right, face right
                    spriteRenderer.flipX = false;
                }
                else
                {
                    // Player is to the left, face left
                    spriteRenderer.flipX = true;
                }
            } else
            {
                anim.SetBool("Active", false);
            }
            
        }
    }
}
