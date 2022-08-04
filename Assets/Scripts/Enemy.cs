using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;

    public float speed;

    public Transform rightCol, leftCol, headPoint;
    public LayerMask layer;
    public CircleCollider2D circleCollider2D;
    public BoxCollider2D boxCollider2D;

    private bool colliding;
    void Start()
    {
         rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);

        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);

        if (colliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            float height = col.contacts[0].point.y - headPoint.position.y;

            if (height > 0)
            {
                speed = 0;
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                anim.SetTrigger("enemyDie");
                Destroy(gameObject,0.33f);
                circleCollider2D.enabled = false;
                rig.bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }
    


}
