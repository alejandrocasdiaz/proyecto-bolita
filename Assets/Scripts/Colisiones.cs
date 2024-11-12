using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisiones : MonoBehaviour
{

    float defaultGravity;

    Rigidbody2D rb;
    Colisiones colisiones;
    private SpriteRenderer spriteRenderer;

    public bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Grounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

}
