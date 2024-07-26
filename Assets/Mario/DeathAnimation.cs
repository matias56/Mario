using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    public SpriteRenderer spr;
    public Sprite deadAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        UpdateSprite();
        DisablePhysics();
        StartCoroutine(Animate());
    }

    private void UpdateSprite()
    {
        spr.enabled = true;
        spr.sortingOrder = 10;

        if(deadAnim != null)
        {
            spr.sprite = deadAnim;
        }
       
    }

    private void DisablePhysics()
    {
        Collider2D[] cols = GetComponents<Collider2D>();

        foreach (Collider2D col in cols)
        {
            col.enabled = false;
        }

        GetComponent<Rigidbody2D>().isKinematic = true;

        Mario mar = GetComponent<Mario>();
        GoombaMove enemy = GetComponent<GoombaMove>();

        if(mar != null)
        {
            mar.enabled = false;
        }

        if (enemy != null)
        {
            enemy.enabled = false;
        }
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;
        float duration = 3f;

        float jumpVel = 10f;
        float gravity = -36f;

        Vector3 vel = Vector3.up * jumpVel;

        while(elapsed < duration)
        {
            transform.position += vel * Time.deltaTime;
            vel.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
