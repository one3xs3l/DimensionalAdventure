using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyHold : MonoBehaviour
{
	public bool hold;
	public bool hasKey = false;
	public float key;

	[SerializeField] private AudioSource keySound;
	[SerializeField] private TextMeshProUGUI keyText;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Key"))
		{
			key += 1;
			if (key >= 1)
			{
				hasKey = true;
				hold = true;
			}

			if (key <= 0)
			{
				key = 0;
			}

			keyText.text = key.ToString();
			keySound.Play();
			Destroy(other.gameObject);
		}
	}
}