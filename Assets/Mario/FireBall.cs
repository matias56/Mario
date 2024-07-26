using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 10f; 
    public float bounceForce = 5f; 
    private Rigidbody2D rb;
    private Vector2 direction;

    public float timer = 3f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Set the initial direction of the fireball (e.g., to the right)
        
        // Apply the initial velocity
        rb.velocity = direction * speed;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            // Apply a bounce force when the fireball hits the ground
            rb.velocity = new Vector2(rb.velocity.x, bounceForce);
        }
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }
}
