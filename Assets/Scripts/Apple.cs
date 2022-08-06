using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Apple : MonoBehaviour
{
    private Animator AnimApple;
    private CircleCollider2D circle;
    private int score=10;


    // Start is called before the first frame update
    void Start()
    {
        circle=GetComponent<CircleCollider2D>();
        AnimApple = GetComponent<Animator>();

    }


    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.tag=="Player"){

            AnimApple.Play("Apple_Collected");
            circle.enabled=false;
            SoundController.Instance.PlaySound(2);
            GameController.instance.totalScore+=score;
            GameController.instance.UpdateScoreText();
            Destroy(gameObject, 0.2f);

        }
        
    }
    
}
