using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampolim : MonoBehaviour
{
    public float jumpForce;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            anim.SetTrigger("jumpTramp");
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}
