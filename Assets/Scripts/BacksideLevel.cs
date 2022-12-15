using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacksideLevel : MonoBehaviour
{
    public GameObject BacksideUI;

    private void Start()
    {
        BacksideUI.SetActive(false);
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
        BacksideUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void WindowOff()
    {
        BacksideUI.SetActive(false);
        Time.timeScale = 1;
    }
}
