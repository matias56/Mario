using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D rb;

    private Vector2 vel;
    private float inputAxis;

    public float speed = 8f;
    public float maxJumpH = 5f;
    public float maxJumpTime = 1f;

    public float jumpForce => (2f * maxJumpH) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpH) / Mathf.Pow(maxJumpTime / 2f, 2f);

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }

    public bool isCrouching { get; private set; }

    public bool isShooting { get; private set; }

    public bool sliding => (inputAxis > 0f && vel.x < 0f) || (inputAxis < 0f && vel.x > 0f);
    public bool running => Mathf.Abs(vel.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;

    public Player p;

    private AudioSource s;
    public AudioClip jump;

    public bool canMove = false;

    public Transform fireSpawn;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        p = GetComponentInParent<Player>();
        s = GetComponent<AudioSource>();

        Invoke("StartLevel", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            HorizontalMovement();

            grounded = rb.Raycast(Vector2.down);

            if (grounded)
            {
                GroundedMovement();
            }


            ApplyGravity();


            if (Input.GetKey(KeyCode.DownArrow) && p.big || Input.GetKey(KeyCode.DownArrow) && p.fire)
            {
                isCrouching = true;
                maxJumpH = 0;
                speed = 0f;
                vel.x = 0;
            }
            else
            {
                isCrouching = false;
                speed = 8f;
                maxJumpH = 5f;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 12;
            }
            else
            {
                speed = 8;
            }
        }
        
        

    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        vel.x = Mathf.MoveTowards(vel.x, inputAxis * speed, speed * Time.deltaTime);

        if(rb.Raycast(Vector2.right * vel.x))
        {
            vel.x = 0f;
        }

        if(vel.x>0f)
        {
            transform.eulerAngles = Vector3.zero;
        } else if (vel.x < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        
    }

    private void GroundedMovement()
    {

        vel.y = Mathf.Max(vel.y, 0f);
        jumping = vel.y > 0;
        if(Input.GetButtonDown("Jump") && isCrouching == false)
        {
            s.clip = jump;
            s.Play();
            vel.y = jumpForce;
            jumping = true;

        }
    }

    private void ApplyGravity()
    {
        bool falling = vel.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;
        vel.y += gravity * multiplier * Time.deltaTime;
        vel.y = Mathf.Max(vel.y, gravity / 2f);
    }

    private void FixedUpdate()
    {
        Vector2 pos = rb.position;
        pos += vel * Time.fixedDeltaTime;

        Vector2 leftEdge = cam.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        pos.x = Mathf.Clamp(pos.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        rb.MovePosition(pos);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(transform.DotTest(other.transform, Vector2.down))
            {
                vel.y = jumpForce / 2f;
                jumping = true;
            }
        }
        else if (other.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            vel.y = 0f;
            if (transform.DotTest(other.transform, Vector2.up))
            {
                vel.y = 0f;
            }
        }

        
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
    }

    private void StartLevel()
    {
        // Enable player movement
       EnableMovement();

   
        
    }


}
