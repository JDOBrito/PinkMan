using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed, jumpForce;

    public bool jumpStart, jumpDouble;

    public Collision2D colisor;

    private Rigidbody2D _rigidbody2D;

    private Animator anim;
    
    void Start()
    {
        TryGetComponent(out _rigidbody2D);
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        Movement();
        Jump();
    }

    private void Movement()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), y: 0f, z: 0f);
        _rigidbody2D.transform.position += move * (Time.deltaTime * speed);

        if (Input.GetAxis("Horizontal") > 0)
        {
            anim.SetBool("move", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        
        if (Input.GetAxis("Horizontal") < 0)
        {
            anim.SetBool("move", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        
        if (Input.GetAxis("Horizontal") == 0)
        {
            anim.SetBool("move", false);
        }
        
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!jumpStart)
            {
                _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpDouble = true;
                anim.SetBool("jump", true);
            }
            else
            {
                if (jumpDouble)
                {
                    _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    jumpDouble = false;
                    anim.SetBool("doubleJump", true);
                }
            }
            
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        colisor = collision;
        if (collision.gameObject.layer == 8)
        {
            jumpStart = false;
            anim.SetBool("jump", false);
            anim.SetBool("doubleJump", false);
        }

        if (collision.transform.position.y < 0)
        {
            die(7, "hit", true);
        }

        if (collision.gameObject.layer == 9)
        {
            die(9, "hit", true);
        }
        
        if (collision.gameObject.layer == 10)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            die(10, "hit", true);
        }
        
        if (collision.gameObject.layer == 6)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            die(6, "hit", true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            jumpStart = true;
        }

        if (collision.transform.position.y < 0)
        {
            die(6, "hit", false);
            die(7, "hit", false);
            die(10, "hit", false);
        }
        
        if (collision.gameObject.layer == 9)
        {
            die(9, "hit", false);
        }
        
        if (collision.gameObject.layer == 10)
        {
            die(10, "hit", false);
        }

    }

    public void die(int layerOpt, string animator, bool option)
    {
        if (colisor.gameObject.layer == layerOpt) //SAW
        {
            anim.SetBool(animator, option);
            //game over ou reiniciar a fase
        }
    }

}
