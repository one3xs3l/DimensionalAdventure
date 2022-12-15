using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
	public Door door;
	public int numKeys;
	KeyHold keyHold;
	public GameObject keys;

	private void Awake()
	{
		keyHold = FindObjectOfType<KeyHold>();
	}

	public void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			if (keyHold.key == numKeys)
			{
				door.Open();
			}
		}

		if (keyHold.key < 0)
		{
			keyHold.key = 0;
		}
	}

	public void OnTriggerExit2D(Collider2D col)
	{
        if (col.tag == "Player" && keyHold.hasKey == true)
        {
            door.Close();
        }
    }
}