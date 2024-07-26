using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioSprite : MonoBehaviour
{

    public SpriteRenderer spr { get; private set; }
    private Mario mar;

    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public AnimSprite run;
    public Sprite crouch;
    public Sprite shoot;
    public Player p;
    // Start is called before the first frame update
    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        mar = GetComponentInParent<Mario>();
        p = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        spr.enabled = true;
    }

    private void OnDisable()
    {
        spr.enabled = false;
        run.enabled = false;
    }

    private void LateUpdate()
    {

        run.enabled = mar.running;
        if(mar.jumping)
        {
            spr.sprite = jump;
        }
        else if(mar.isCrouching)
        {
            spr.sprite = crouch;
        }
        else if(mar.sliding)
        {
            spr.sprite = slide;
        } else if(!mar.running)
        {
            spr.sprite = idle;
        }
    }

   
}
