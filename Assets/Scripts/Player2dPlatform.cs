using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2dPlatform : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed, jump, gravityNormal;
    private bool detect, jumping, climbing, touchStair, crouching;
    public Detector deter, crouchDetect;
    private int direction;
    public SpriteRenderer look;
    public Sprite standSprite, crouchSprite;
    public Animator anim;
    public AudioSource _audioSourceHumano;
    public AudioSource _audioSourceZombie;
    private CapsuleCollider2D capsule;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Croucher();
    }

    void Move() {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h > 0)
        {
            direction = 1;
            //anim.SetBool("Mover", true);
        }
        else if (h < 0)
        {
            direction = -1;
            //anim.SetBool("Mover", true);
        }
        else
        {
            //anim.SetBool("Mover", false);
        }
        if (direction == 1)
        {
            look.flipX = false;

        }
        else
        {
            look.flipX = true;
        }
        Vector3 vel = rb2d.velocity;
        vel.x = speed * h;

        if (climbing)
        {
            vel.y = speed * v;
            rb2d.gravityScale = 0;
        }
        else
        {
            rb2d.gravityScale = gravityNormal;
        }
        rb2d.velocity = vel;
        if (IsGrounded() == false)
        {
            detect = true;
        }
        if (detect == true && vel.y < 0)
        {
            //vel.y = vel.y * -0.2f;
            //rb2d.velocity = vel;
            //anim.SetInteger("SaltoNúmero", 1);
            detect = false;
        }
        if (jumping && IsGrounded())
        {
            //anim.SetInteger("SaltoNúmero", 2);
        }
        if (touchStair && !crouching && v > 0)
        {
            climbing = true;
        }

        if (climbing && !touchStair)
        {
            climbing = false;
        }
        if (v < 0 && !climbing)
        {
            crouching = true;
        }
        else if (!crouchDetect.deply)
        {
            crouching = false;
        }
    }

    void Croucher()
    {
        if (crouching)
        {
            capsule.offset = new Vector2(0, -0.55f);
            capsule.size = new Vector2(2, 0.9f);
            capsule.direction = CapsuleDirection2D.Horizontal;
            look.sprite = crouchSprite;
        }
        else
        {
            capsule.offset = new Vector2(0, 0);
            capsule.size = new Vector2(0.9f, 2);
            capsule.direction = CapsuleDirection2D.Vertical;
            look.sprite = standSprite;
        }
    }
    void FixedUpdate (){
        if ((IsGrounded() || climbing) && Input.GetAxis("Jump") > 0.3 && !crouching)
        {
            jumping = true;
            rb2d.AddForce(transform.up * jump * 200);
            climbing = false;
            //anim.SetInteger("SaltoNúmero", 0);
        }

    }

    private bool IsGrounded()
    {
        return GroundCheck.isGrounded;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Stair")
        {
            touchStair = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Stair")
        {
            touchStair = false;
        }
    }
}
