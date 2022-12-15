using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	Animator anim;
    Collider2D col;

    private void Start()
    {
		anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }

	public void Open()
    {
		anim.SetBool("open", true);
    }

	public void Close()
    {
        Destroy(this.gameObject);
	}

    public void Enable()
    {
        col.enabled = true;
    }

    public void Disable()
    {
        col.enabled = false;
    }
}