using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Pickup();
        } 
    }

    public void Pickup()
    {
        anim.SetTrigger("Pickup");
    }

    private void Disapp()
    {
        Destroy(this.gameObject);
    }

}
