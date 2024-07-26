using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{

    private GameManager gm;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mario"))
        {
            other.gameObject.SetActive(false);
            gm.ResetLevel(3f);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
