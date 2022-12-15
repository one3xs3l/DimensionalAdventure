using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int levelCoin;
    public GameObject Checkmark;

    private void Start()
    {
        Checkmark.SetActive(false);
    }

    private void Update()
    {
        if (PlayerController.coin < levelCoin)
        {
            Checkmark.SetActive(false);
        }
        else if (PlayerController.coin >= levelCoin)
        {
            Checkmark.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerController.coin >= levelCoin)
        {
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();
            controller.PlayerDeath();
            UnlockLevel();
            StartCoroutine(Wait(1.0f));
        }
    }

    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void UnlockLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (currentLevel >= PlayerPrefs.GetInt("levels"))
        {
            PlayerPrefs.SetInt("levels", currentLevel + 1);
        }
    }
}