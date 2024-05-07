using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]

public class Projectiles : MonoBehaviour
{
    public float lifetime;

    //Speed value is set by shoot script when the player fires
    [HideInInspector]
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        if (lifetime <= 0) lifetime = 2.0f;

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        Destroy(gameObject, lifetime);

    }
}
