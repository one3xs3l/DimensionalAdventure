using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool close = false;
    public float dirUp;
    public float dirDown;

    public GameObject wall;
    Animator anim;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (close == true && wall.transform.position.y < dirUp)
        {
            wall.transform.Translate(Vector2.up * Time.deltaTime * 4);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.tag == "Player" || collision.tag == "Box") && wall.transform.position.y > dirDown)
        {
            anim.SetBool("ButtonOn", true);
            wall.transform.Translate(Vector2.down * Time.deltaTime * 4);
        }
        
        close = false;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetBool("ButtonOn", false);
        close = true;
    }
}