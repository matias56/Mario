using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MtoF : MonoBehaviour
{
    private Player p;
    public GameObject flower;
    // Start is called before the first frame update
    void Start()
    {
        p = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(p.big || p.fire)
        {
            Instantiate(flower, this.transform.position, this.transform.rotation);
            
            Destroy(this.gameObject);
        }
    }
}
