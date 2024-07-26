using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flat;
    public GameManager gm;

    private AudioSource s;
  
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
        if(other.gameObject.CompareTag("Mario"))
        {
            Player mario = other.gameObject.GetComponent<Player>();
            if (mario.starpower || other.gameObject.layer == LayerMask.NameToLayer("Fire"))
            {
                Hit();
            }
            else if (other.transform.DotTest(transform, Vector2.down))
            {
                Flatten();
            }
            else
            {
                mario.Hit();
            }
        }
    }

    private void Flatten()
    {

        s.clip = die;
        s.Play();
        gm.score += 100;

        GetComponent<Collider2D>().enabled = false;
        GetComponent<GoombaMove>().enabled = false;
        GetComponent<AnimSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flat;
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Shell") || other.gameObject.layer == LayerMask.NameToLayer("Fire"))
        {
            Hit();
        }
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
}
