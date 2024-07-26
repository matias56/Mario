using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 vel;
    public Vector2 dir = Vector2.left;
    public float speed = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        rb.WakeUp();
    }

    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
        rb.Sleep();
    }

    private void FixedUpdate()
    {
        vel.x = dir.x * speed;
        vel.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + vel * Time.fixedDeltaTime);

        if(rb.Raycast(dir))
        {
            dir = -dir;
        }

        if(rb.Raycast(Vector2.down))
        {
            vel.y = Mathf.Max(vel.y, 0f);
        }
    }
}
