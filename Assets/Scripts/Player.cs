using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayersInput actions;
    [SerializeField] private float speed, jumpForce, teleport;

    public bool jumpStart, jumpDouble;
    private bool dash;
    private float direction;

    public Collision2D colisor;

    private Rigidbody2D _rigidbody2D;

    private Animator anim;

    void Awake()
    {
        actions = new PlayersInput();

        actions.Player.Movement.performed += ctx => Movement(ctx.ReadValue<Vector2>());
        actions.Player.Movement.canceled += _ => Movement(Vector2.zero);

        actions.Player.Jump.performed += _ => Jump();
        
        actions.Player.SpeedRun.performed += _ => SpeedRun();
        
        actions.Player.Teleport.performed += _ => Teleport();
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    void Start()
    {
        TryGetComponent(out _rigidbody2D);
        anim = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(direction * speed, _rigidbody2D.velocity.y);
    }

    private void Movement(Vector2 valueDirection)
    {

        direction = valueDirection.x;

        if (direction > 0)
        {
            if (dash == false)
            {
                anim.SetBool("move", true);
            }
            else
            {
                anim.SetBool("move", true);
                anim.SetBool("dash", true);
            }

            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (direction < 0)
        {
            if (dash == false)
            {
                anim.SetBool("move", true);
            }
            else
            {
                anim.SetBool("move", true);
                anim.SetBool("dash", true);
            }

            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if (direction == 0)
        {
            if (dash == false)
            {
                anim.SetBool("move", false);
            }
            else
            {
                anim.SetBool("dash", false);
                anim.SetBool("move", false);
            }
        }
    }

    private void Jump()
    {
        if (!jumpStart)
        {
            SoundController.Instance.PlaySound(1);
            _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpDouble = true;
            anim.SetBool("jump", true);
        }
        else
        {
            if (jumpDouble)
            {
                SoundController.Instance.PlaySound(1);
                _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpDouble = false;
                anim.SetBool("doubleJump", true);
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
            GameController.instance.CallGameOverScreen();
            Destroy(gameObject);
        }
    }

    void SpeedRun()
    {
        if (speed <= 8) //Dash
        {
            speed = 50f;
            dash = true;
        } 
        else
        {
            speed = 8f;
            dash = false;
            anim.SetBool("dash", false);
            anim.SetBool("move", false);
        }
    }

    void Teleport(){
        if (direction > 0)
        {
            float posPlayer = _rigidbody2D.position.x + teleport;
            if (posPlayer <= 14.34)
            {
                _rigidbody2D.position = new Vector2(posPlayer, _rigidbody2D.position.y);
            }
            else
            {
                posPlayer = _rigidbody2D.position.x;
                _rigidbody2D.position = new Vector2(posPlayer, _rigidbody2D.position.y);
            }
        }
        
        if (direction < 0)
        {
            float posPlayer = _rigidbody2D.position.x - teleport;
            if (posPlayer >= -14.34)
            {
                _rigidbody2D.position = new Vector2(posPlayer, _rigidbody2D.position.y);
            }
            else
            {
                posPlayer = _rigidbody2D.position.x;
                _rigidbody2D.position = new Vector2(posPlayer, _rigidbody2D.position.y);
            }
        }
        /*if (teleportBool == true)
        {
            teleportBool = false;
        }
        else if (teleportBool == false)
        {
            teleportBool = true;
        }*/
    }

}
