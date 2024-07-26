using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private GameManager gm;

   


    private void Start()
    {
       
        gm = FindObjectOfType<GameManager>();

        
    }
    public enum Type
    {
        Coin,
        ExtraLife,
        MagicMushroom,
        Starpower,
        Flower
    }

    public Type type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mario"))
        {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        switch (type)
        {
            case Type.Coin:
                
                gm.AddCoin();
                /*s.clip = coin;
                s.Play();*/
                break;

            case Type.ExtraLife:
                
                gm.AddLife();
               /* s.clip = up;
                s.Play();*/
                break;

            case Type.MagicMushroom:
                
                player.GetComponent<Player>().Grow();
               
                break;

            case Type.Starpower:
               
                player.GetComponent<Player>().Starpower();
                
                break;

            case Type.Flower:
               
                player.GetComponent<Player>().GoFire();
              
                break;
        }

        Destroy(gameObject);
    }
}
