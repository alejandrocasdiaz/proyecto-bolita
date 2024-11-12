using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    enum Direction { Left = -1, None = 0,  Right = 1};
    Direction currentDirection = Direction.None;

    public float speed;

    public float jumpForce;
    public float maxJumpingTime = 1f;
    bool isJumping;
    bool isChargingJump;
    float jumpTimer = 0;
    float defaultGravity;

    Rigidbody2D rb2D;
    Colisiones colisiones;
    private bool isFacingRight = true;
    public Sprite normalSprite;
    public Sprite jumpSprite;
    public Sprite preJumpSprite;
    private SpriteRenderer spriteRenderer;

    public float maxJumpForce;
    public float minJumpForce;
    public float chargeSpeed;

    private float jumpChargeTime = 0f;

    public Sprite deadSprite;

    bool playerAlive = true;
    bool reachedGoal = false;

    public PauseManager pauseManager;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        colisiones = GetComponent<Colisiones>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        defaultGravity = rb2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {

        if(pauseManager.onPause == true || reachedGoal)
        {
            return;
        }

        if (playerAlive == true)
        {
            if (colisiones.Grounded() && !isJumping)
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    currentDirection = Direction.None;
                    if (isFacingRight)
                    {
                        Flip();
                    }
                }
                else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    currentDirection = Direction.None;
                    if (!isFacingRight)
                    {
                        Flip();
                    }
                }
                else
                {
                    currentDirection = Direction.None;
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    isChargingJump = true;
                    jumpChargeTime += Time.deltaTime * chargeSpeed;
                    jumpChargeTime = Mathf.Clamp(jumpChargeTime, 0f, maxJumpForce);
                    spriteRenderer.sprite = preJumpSprite;
                }
                if (Input.GetKeyUp(KeyCode.Space) && isChargingJump)
                {
                    Jump();
                    isChargingJump = false;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    currentDirection = Direction.Left;
                    if (isFacingRight)
                    {
                        Flip();
                    }
                }
                else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    currentDirection = Direction.Right;
                    if (!isFacingRight)
                    {
                        Flip();
                    }
                }
                else
                {
                    currentDirection = Direction.None;
                }
            }

            if (isJumping)
            {
                if (rb2D.velocity.y < 0f)
                {
                    rb2D.gravityScale = defaultGravity;
                    if (colisiones.Grounded())
                    {
                        isJumping = false;
                        jumpChargeTime = 0f;
                        spriteRenderer.sprite = normalSprite;
                    }
                }
                else if (rb2D.velocity.y > 0f)
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        jumpTimer += Time.deltaTime;
                    }
                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        if (jumpTimer < maxJumpingTime)
                        {
                            rb2D.gravityScale = defaultGravity * 3f;
                        }
                    }
                }
            }
        }

        else
        {
            return;
        }
    }

    private void FixedUpdate()
    {
        if (playerAlive && !reachedGoal)
        {
            Vector2 velocity = new Vector2((int)currentDirection * speed, rb2D.velocity.y);
            rb2D.velocity = velocity;
        }
    }

    void Jump()
    {
        if (colisiones.Grounded() && !isJumping)
        {
            isJumping = true;
            Vector2 force = new Vector2(0, Mathf.Lerp(minJumpForce, maxJumpForce, jumpChargeTime / maxJumpForce));
            rb2D.AddForce(force, ForceMode2D.Impulse);
            spriteRenderer.sprite = jumpSprite;
            jumpChargeTime = 0f;
        }
        
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    void MoveRigth()
    {
        Vector2 velocity = new Vector2(1f, 0f);
        rb2D.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            Debug.Log("El personaje ha tocado los pinchos");
            Die();
        }

        if (collision.gameObject.CompareTag("Killzone"))
        {
            Debug.Log("El personaje se ha caído");
            Die();
        }

        if (collision.gameObject.CompareTag("Corner"))
        {
            Debug.Log("El personaje ha llegado a la meta");
            reachedGoal = true;
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            pauseManager.NextLevel();
        }
    }

    public void Die()
    {
        if (playerAlive)
        {
            playerAlive = false;
            Debug.Log("El personaje ha muerto");
            spriteRenderer.sprite = deadSprite;
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);

            if (pauseManager != null)
            {
                pauseManager.ShowRetryMenu();
            }
        }
    }
}
