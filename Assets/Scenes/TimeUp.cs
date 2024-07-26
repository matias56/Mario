using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUp : MonoBehaviour
{
    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.fTime <= 0)
        {
            this.gameObject.SetActive(true);
        }
    }
}
