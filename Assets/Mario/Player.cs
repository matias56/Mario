using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MarioSprite smallR;
    public MarioSprite bigR;

    public MarioSprite fireR;

    public CapsuleCollider2D capsuleCollider { get; private set; }

    private MarioSprite activeRenderer;

    private DeathAnimation dA;

    public bool big => bigR.enabled;
    public bool small => smallR.enabled;

    public bool fire => fireR.enabled;
    public bool dead => dA.enabled;

    private GameManager gm;

    public bool starpower { get; private set; }

    public Transform fireSpawn;

    public GameObject fireBall;
    public bool isCool;

    public Mario m;

    private AudioSource s;
    public AudioClip get;
    public AudioClip coin;
    public AudioClip up;
    public AudioClip shrink;
    public AudioClip flag;
    public AudioClip die;
    public AudioClip fireS;
    public AudioSource overall;
    public AudioClip ground;
    public AudioClip under;
    public AudioClip water;
    public AudioClip castle;
    public AudioClip star;
    public bool isG;
    public bool isU;
    public bool isW;
    public bool isC;

    public AudioClip groundHurry;
    public AudioClip underHurry;
    public AudioClip waterHurry;
    public AudioClip castleHurry;
    public AudioClip starHurry;

    public float musicStartDelay = 2f;
    public bool timeMusic = false;

    private void Awake()
    {
        s = GetComponent<AudioSource>();
        dA = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        gm = FindObjectOfType<GameManager>();
        m = GetComponent<Mario>();


        Invoke("PlayMusic", musicStartDelay);

        activeRenderer = smallR;

        
    }

    

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && fire && !isCool)
        {
            LaunchFire();
        }

        if (m.isCrouching || small)
        {
            capsuleCollider.size = new Vector2(1f, 1f);
            capsuleCollider.offset = new Vector2(0f, 0f);
        }
        else if(!m.isCrouching || !small)
        {
            capsuleCollider.size = new Vector2(1f, 2f);
            capsuleCollider.offset = new Vector2(0f, 0.5f);
        }

        float moveInput = Input.GetAxis("Horizontal");

        if (moveInput > 0)
        {
            fireSpawn.localScale = new Vector3(1, 1, 1); // Facing right
        }
        else if (moveInput < 0)
        {
            fireSpawn.localScale = new Vector3(-1, 1, 1); // Facing left
        }
    }

    private void FixedUpdate()
    {

      

    }

    private void LateUpdate()
    {
       
    }

    private IEnumerator CoolDown()
    {
        isCool = true;
        yield return new WaitForSeconds(3f);
        isCool = false;
    }

    public void Hit()
    {
        if(big || fire)
        {
            Shrink();
        } else
        {
            Death();
        }
    }

    public void Grow()
    {

        s.clip = get;
        s.Play();
        smallR.enabled = false;
        bigR.enabled = true;
        activeRenderer = bigR;

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        StartCoroutine(ScaleAnimation());


    }

    public void GoFire()
    {

        s.clip = get;
        s.Play();

        fireR.enabled = true;
        activeRenderer = fireR;

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        StartCoroutine(ScaleAnimation());
    }

    public void Shrink ()
    {
        s.clip = shrink;
        s.Play();
        smallR.enabled = true;
        bigR.enabled = false;
        fireR.enabled = false;
        activeRenderer = smallR;

        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                smallR.enabled = !smallR.enabled;
                bigR.enabled = !smallR.enabled;
            }

            yield return null;
        }

        smallR.enabled = false;
        bigR.enabled = false;
        activeRenderer.enabled = true;
    }

    private void Death()
    {
        Destroy(overall);
        s.clip = die;
        s.Play();
        fireR.enabled = false;
        smallR.enabled = false;
        bigR.enabled = false;
        dA.enabled = true;

       gm.ResetLevel(3f);
    }

    public void DeathTime()
    {
        Destroy(overall);
        s.clip = die;
        s.Play();
        fireR.enabled = false;
        smallR.enabled = false;
        bigR.enabled = false;
        dA.enabled = true;
        timeMusic = true;
        
        gm.ResetLevel(3f);
    }

    public void Starpower()
    {
        s.clip = get;
        s.Play();
        StartCoroutine(StarpowerAnimation());
    }

    private IEnumerator StarpowerAnimation()
    {
        starpower = true;

        if(!gm.hurryUpPlayed)
        {
            overall.clip = star;
            overall.Play();
        }
        else
        {
            overall.clip = starHurry;
            overall.Play();
        }
       

        float elapsed = 0f;
        float duration = 10f;

        while (elapsed < duration)
        {
            

            elapsed += Time.deltaTime;

           
            if (Time.frameCount % 4 == 0)
            {
               

                activeRenderer.spr.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;
        }

        activeRenderer.spr.color = Color.white;
        starpower = false;

        if(!gm.hurryUpPlayed)
        {
            overall.clip = ground;
            overall.Play();
        }
        else
        {
            overall.clip = groundHurry;
            overall.Play();
        }
        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            s.clip = coin;
            s.Play();
        }


        if (other.gameObject.CompareTag("1UP"))
        {
            s.clip = up;
            s.Play();
        }

        if(other.gameObject.CompareTag("Flag"))
        {
            Destroy(overall);
           
            
            s.clip = flag;
        }

        if(other.gameObject.CompareTag("GoUnder") && !gm.hurryUpPlayed)
        {
           
            overall.clip = under;
            overall.Play();
        }
        else if(other.gameObject.CompareTag("GoUnder") && gm.hurryUpPlayed)
        {
            overall.clip = underHurry;
            overall.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GoUnder") && !gm.hurryUpPlayed)
        {
           
            overall.clip = ground;
            overall.Play();
        }
        else if (other.gameObject.CompareTag("GoUnder") && gm.hurryUpPlayed)
        {
            overall.clip = groundHurry;
            overall.Play();
        }
    }

    private void PlayMusic()
    {

        if (isU == true)
        {

            overall.clip = under;
            overall.Play();
        }
        else if (isW == true)
        {
            overall.clip = water;
            overall.Play();
        }
        else if (isC == true)
        {

            overall.clip = castle;
            overall.Play();
        }
        else if (isG == true)
        {

            overall.clip = ground;
            overall.Play();
        }
    }

    public void PlayHurry()
    {
        if (isG)
        {
            overall.clip = groundHurry;
            overall.Play();
        }
        else if (isU)
        {
            overall.clip = underHurry;
            overall.Play();
        }
        else if (isW)
        {
            overall.clip = waterHurry;
            overall.Play();
        }
        else if (isC)
        {
            overall.clip = castleHurry;
            overall.Play();
        }
    }

    public void LaunchFire()
    {
        s.clip = fireS;
        s.Play();
        GameObject fBall = Instantiate(fireBall, fireSpawn.position, Quaternion.identity);
        FireBall fireballScript = fBall.GetComponent<FireBall>();
        Vector2 direction = fireSpawn.localScale.x > 0 ? Vector2.right : Vector2.left;
        fireballScript.SetDirection(direction);
        StartCoroutine(CoolDown());
    }
}
