using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    
    private enum State { idle, running, jumping, falling, hurt }
    private State state = State.idle;

    private float moveInput;
    private bool facingRight = true;
    public float normalSpeed;
    private bool ctrlActive;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float hurtForce = 10f;
    //[SerializeField] private int health;
    //[SerializeField] private int numberOfLives;
    //[SerializeField] private Image[] lives;
    //[SerializeField] private Sprite fullLive;
    //[SerializeField] private Sprite emptyLive;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource coinSound;
    [SerializeField] private GameObject ScreenDeath;
    [SerializeField] public static int coin = 0;
    [SerializeField] public TextMeshProUGUI coinText;

    private void Start()
    {
        coin = 0;
        speed = 0f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        anim.SetTrigger("appear");
        ctrlActive = false;
        StartCoroutine(PlayerRespawn());
        ScreenDeath.SetActive(false);
    }

    private void Update()
    {
        if (ctrlActive == true)
        {
            if (state != State.hurt)
            {
                Movement();
            }

            //HealthPlayer();
            AnimationState();
            anim.SetInteger("state", (int)state);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            coinSound.Play();
            coin += 1;
            coinText.text = coin.ToString();
        }

        if (collision.tag == "Spike" || collision.tag == "MoveSpike" || collision.tag == "Pendulum")
        {
            //health = 0;
            PlayerDeath();
            StartCoroutine(Wait(1.0f));
        }
    }

    private IEnumerator PlayerRespawn()
    {
        yield return new WaitForSeconds(.7f);
        ctrlActive = true;
    }

    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(1.0f);
        ScreenDeath.SetActive(true);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if (state == State.falling)
            {
                enemy.JumpedOn();
                JumpEnemy();
            }
            else
            {
                PlayerDeath();
                coll.enabled = false;
                rb.bodyType = RigidbodyType2D.Kinematic;
                StartCoroutine(Wait(1.0f));
                //state = State.hurt;
                //health -= 1;
                //if (health <= 0)
                //{
                //    PlayerDeath();
                //    StartCoroutine(Wait(1.0f));
                //}
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }

        if (other.gameObject.tag == "Spear")
        {
            //health = 0;
            PlayerDeath();
            coll.enabled = false;
            rb.bodyType = RigidbodyType2D.Kinematic;
            StartCoroutine(Wait(1.0f));
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Platform")
        {
            this.transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            this.transform.parent = null;
        }
    }

    private void Movement()
    {
        //moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
        ////Движение влево
        //if (moveInput < 0)
        //{
        //    rb.velocity = new Vector2(speed * moveInput, rb.velocity.y);
        //    transform.localScale = new Vector2(-1, 1);
        //}
        ////Движение вправо
        //else if (moveInput > 0)
        //{
        //    rb.velocity = new Vector2(speed * moveInput, rb.velocity.y);
        //    transform.localScale = new Vector2(1, 1);
        //}

        ////Прыжок
        //if (Input.GetButtonDown("Jump"))
        //{
        //    RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, 1.3f, ground);
        //    if (hit.collider != null)
        //        Jump();
        //}
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    public void OnLeftButtonDown()
    {
        if (speed >= 0f)
        {
            speed = -normalSpeed;
            moveInput = -1;
        }
    }

    public void OnRightButtonDown()
    {
        if (speed <= 0f)
        {
            speed = normalSpeed;
            moveInput = 1;
        }
    }

    public void OnButtonUp()
    {
        speed = 0f;
    }

    public void Jump()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, 1.3f, ground);
        if (hit.collider != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.jumping;
            jumpSound.Play();
        }
    }

    public void PlayerDeath()
    {
        anim.SetTrigger("death");
        //ctrlActive = false;
        //coll.enabled = false;
        //rb.bodyType = RigidbodyType2D.Kinematic;
        normalSpeed = 0;
    }

    public void JumpEnemy()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
        jumpSound.Play();
    }

    public void HealthPlayer()
    {
        //if (health > numberOfLives)
        //{
        //    health = numberOfLives;
        //}

        //for (int i = 0; i < lives.Length; i++)
        //{
        //    if (i < health)
        //    {
        //        lives[i].sprite = fullLive;
        //    }
        //    else
        //    {
        //        lives[i].sprite = emptyLive;
        //    }

        //    if (i < numberOfLives)
        //    {
        //        lives[i].enabled = true;
        //    }
        //    else
        //    {
        //        lives[i].enabled = false;
        //    }
        //}
    }

    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < 2f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
}