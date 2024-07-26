using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shell;

    private bool shelled;
    private bool shellMove;

    public float shellSpeed = 12f;

    public GameManager gm;

    private AudioSource s;
    public AudioClip kick;
    public AudioClip die;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        s = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!shelled && other.gameObject.CompareTag("Mario"))
        {
            Player mario = other.gameObject.GetComponent<Player>();

            if (mario.starpower || other.gameObject.layer == LayerMask.NameToLayer("Fire"))
            {
                Hit();
            }          
            else if (other.transform.DotTest(transform, Vector2.down))
            {
                EnterShell();
            }
            else
            {
                mario.Hit();
            }
        }
    }

    private void EnterShell()
    {
        s.clip = die;
        s.Play();
        shelled = true;

        GetComponent<GoombaMove>().enabled = false;
        GetComponent<AnimSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shell;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(shelled && other.gameObject.CompareTag("Mario"))
        {
            if(!shellMove)
            {
                s.clip = kick;
                s.Play();
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                Player p = other.GetComponent<Player>();
                if (p.starpower || other.gameObject.layer == LayerMask.NameToLayer("Fire"))
                {
                    Hit();
                }
                else
                {
                    p.Hit();
                }
            }
        }
        else if(!shelled && other.gameObject.layer == LayerMask.NameToLayer("Shell") || other.gameObject.layer == LayerMask.NameToLayer("Fire"))
        {
            Hit();
        }
    }

    private void PushShell(Vector2 dir)
    {
        shellMove = true;

        GetComponent<Rigidbody2D>().isKinematic = false;

        GoombaMove gMove = GetComponent<GoombaMove>();
        gMove.dir = dir.normalized;

        gMove.speed = shellSpeed;
        gMove.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void Hit()
    {
        s.clip = die;
        s.Play();
        gm.score += 100;
        GetComponent<AnimSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

    private void OnBecameInvisible()
    {
        if(shellMove)
        {
            Destroy(gameObject);
        }
    }
}
