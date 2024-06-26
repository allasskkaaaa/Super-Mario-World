using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour

{
    public bool TestMode;
    //Player Gameplay Variables

    public float dmgCooldown = 1.0f; //Cooldown of when the player takes damage again
    public bool isInvulnerable = false;
    public GameObject respawnPoint; //Respawn point using game object

    [SerializeField] public int maxLives = 3;
    //Movement Var
    [SerializeField] private int speed;
    [SerializeField] private int jumpForce = 3;

    //Groundcheck
    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask isGroundLayer;
    [SerializeField] private float groundCheckRadius;


    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    private Coroutine jumpForceChange = null;
    private Coroutine speedChange = null;

    public void PowerupValueChange(Pickup.PickupType type)
    {
        if (type == Pickup.PickupType.PowerupSpeed)
            FillSpecificCoroutineVar(ref speedChange, ref speed, type);

        if (type == Pickup.PickupType.PowerupJump)
            FillSpecificCoroutineVar(ref jumpForceChange, ref jumpForce, type);
    }

    void FillSpecificCoroutineVar(ref Coroutine inVar, ref int varToChange, Pickup.PickupType type)
    {
        if (inVar != null)
        {
            StopCoroutine(inVar);
            inVar = null;
            varToChange /= 2;
            inVar = StartCoroutine(ValueChangeCoroutine(type));
            return;
        }

        inVar = StartCoroutine(ValueChangeCoroutine(type));
    }
    IEnumerator ValueChangeCoroutine(Pickup.PickupType type)
    {
        if (type == Pickup.PickupType.PowerupSpeed)
            speed *= 2;
        if (type == Pickup.PickupType.PowerupJump)
            jumpForce *= 2;

        yield return new WaitForSeconds(2.0f);

        if (type == Pickup.PickupType.PowerupSpeed)
        {
            speed /= 2;
            speedChange = null;
        }
        if (type == Pickup.PickupType.PowerupJump)
        {
            jumpForce /= 2;
            jumpForceChange = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();


        if (speed <= 0)
        {
            speed = 5;
            if (TestMode) Debug.Log("Speed Set To Default Value");
        }

        if (jumpForce <= 0)
        {
            jumpForce = 3;
            if (TestMode) Debug.Log("Jump Force To Default Value");

        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.02f;
            if (TestMode) Debug.Log("Ground Check Radius Set To Default Value");

        }

        if (groundCheck == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("GroundCheck");
            if ((obj = null))
            {
                groundCheck = obj.transform;
                return;

            }
            GameObject newObj = new GameObject();
            newObj.transform.SetParent(transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.name = "GroundCheck";
            newObj.tag = newObj.name;
            groundCheck = newObj.transform;
            if (TestMode) Debug.Log("Ground Check Transform Created via Code - Did you forget to assign it in the inspector?");
        }
    }
    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        float xInput = Input.GetAxis("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);




        //Sprite Flipping
        if (xInput != 0) sr.flipX = (xInput < 0);

        anim.SetFloat("speed", Mathf.Abs(xInput));
        anim.SetBool("isGrounded", isGrounded);


        //Input Checks
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("attack");
        }

        if (Input.GetButtonDown("Jump") && !isGrounded)
        {
            anim.SetTrigger("jumpAttack");
        }

        //Check animation frame for physics
        if (curPlayingClips.Length > 0)
        {
            if (curPlayingClips[0].clip.name == "Mario_Attack")
                rb.velocity = Vector2.zero;
            else
            {
                Vector2 moveDirection = new Vector2(xInput * speed, rb.velocity.y);
                rb.velocity = moveDirection;

            }
            if (curPlayingClips[0].clip.name == "Mario_JumpAttack")
                anim.gameObject.GetComponent<Rigidbody2D>().gravityScale = 5;
            else
            {
                anim.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            }

        }

    }

    public void IncreaseGravity()
    {
        rb.gravityScale = 10;
    }

    
}