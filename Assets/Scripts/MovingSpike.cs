using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpike : MonoBehaviour
{
    public float leftCap;
    public float rightCap;
    float speed = 7f;
    bool movingRight = true;

    private void Update()
    {
        if (CompareTag("MoveSpike"))
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
    }
}
