using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    public GameObject item;
    public Sprite emptyBlock;
    public int maxHits = -1;
    private bool animating;
    public bool isAnim;
    public bool isDestroyable;
    public Player m;
    public GameManager gm;
    public AudioClip bump;
    private AudioSource s;
    
    public GameObject[] bricks;

    // Start is called before the first frame update
    void Start()
    {
        s = GetComponent<AudioSource>();

        m = FindObjectOfType<Player>();
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!animating && maxHits != 0 && other.gameObject.CompareTag("Mario"))
        {
            if (other.transform.DotTest(transform, Vector2.up))
            {
                Hit();
            }
        }
    }

    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true; // show if hidden

        maxHits--;

        if (maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;
        }

        if (item != null)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }

        StartCoroutine(Animate());

        if(isAnim)
        {
            GetComponent<AnimSprite>().enabled = false;
        }

        if(isDestroyable && m.big || isDestroyable && m.fire)
        {
           
            gm.score += 100;

            Instantiate(bricks[3], transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (m.small)
        {
            
            
            s.clip = bump;
            s.Play();
        }
    }

    private IEnumerator Animate()
    {
        animating = true;

        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f;

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        animating = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
    }
}
