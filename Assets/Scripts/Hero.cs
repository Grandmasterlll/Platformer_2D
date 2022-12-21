using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    idle,
    run,
    jump 
}

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int lives = 5;
    [SerializeField] private float jumpForce = 15f;
    private bool isGrounded = false;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    private States States
    {
        get { return (States)animator.GetInteger("state"); }
        set { animator.SetInteger("state",(int)value); }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if(isGrounded == true)
        {
            States = States.idle;
        }

        if (Input.GetButton("Horizontal"))
        {
            Run();
        }

        //isGrounded==true &&
        if (isGrounded==true && Input.GetButton("Jump"))
        {
            Jump();
        }
    }

    private void Run()
    {
        if (isGrounded == true)
        {
            States = States.run;
        }

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime );
        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll( transform.position, 0.3f);
        isGrounded = colliders.Length > 1;

        if (isGrounded == true)
        {
            States = States.jump;
        }
    }
}
