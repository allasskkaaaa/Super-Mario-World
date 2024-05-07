using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    SpriteRenderer sr;

    public float projectileSpeed;
    public Transform spawnPointLeft;
    public Transform spawnPointRight;
    public Projectiles projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (projectileSpeed <= 0) projectileSpeed = 7.0f;

        if (!spawnPointRight || !spawnPointLeft || !projectilePrefab)
            Debug.Log("Please set default values on the shoot script" + gameObject.name);
    }

    public void Fire()
    {
        if (!sr.flipX)
        {
            Projectiles curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            curProjectile.speed = projectileSpeed;
        }
        else
        {
            Projectiles curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            curProjectile.speed = -projectileSpeed;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
