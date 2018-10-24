using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb2d;
    private bool facingRight = true;
    private Animator anim;

    public float speed;
    public float jumpforce;

    //ground check
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;


    //audio stuff
    public AudioClip jumpSound;
    public AudioClip coinSound;
    private AudioSource source1;
    private AudioSource source2;
   
    

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
	}

    void Awake()
    {

       source1 = GetComponent<AudioSource>();
       source2 = GetComponent<AudioSource>(); 

    }

    private void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    
    // Update is called once per frame
    void FixedUpdate () {

        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);

        rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        Debug.Log(isOnGround);

        //for running animation
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }



        //stuff I added to flip my character
        if(facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if(facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }

	}

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickups"))
        {
            other.gameObject.SetActive(false);
            source2.PlayOneShot(coinSound, 1f);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
   

            if (Input.GetKey(KeyCode.UpArrow))
            {
               rb2d.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
               rb2d.velocity = Vector2.up * jumpforce;


                // Audio stuff

                source1.PlayOneShot(jumpSound, 1f);
                
            }
        }
    }
}
