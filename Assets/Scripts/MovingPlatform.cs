using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float leftCap;
    public float rightCap;
    float speed = 3f;
    bool movingRight = true;

    private void Update()
    {
        if (CompareTag("Platform"))
        {
            if (transform.position.x < leftCap)
            {
                movingRight = true;
            }
            else if (transform.position.x > rightCap)
            {
                movingRight = false;
            }

            if (movingRight)
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            }
        }
        else if (CompareTag("Elevator"))
        {
            if (transform.position.y < leftCap)
            {
                movingRight = true;
            }
            else if (transform.position.y > rightCap)
            {
                movingRight = false;
            }

            if (movingRight)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
            }
        }
    }
}