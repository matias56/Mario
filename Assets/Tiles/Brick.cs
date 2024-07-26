using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public AudioClip des;
    private AudioSource s;
    // Start is called before the first frame update
    void Start()
    {
        s = GetComponent<AudioSource>();
        s.clip = des;
        s.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
