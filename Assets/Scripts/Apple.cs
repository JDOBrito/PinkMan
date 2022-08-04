using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public GameObject colllected;
    private SpriteRenderer sr;
    private CircleCollider2D circle;
    


    // Start is called before the first frame update
    void Start()
    {
        sr=GetComponent<SpriteRenderer>();
        circle=GetComponent<CircleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.tag=="Player"){

            sr.enabled=false;
            circle.enabled=false;
            colllected.SetActive(true);
            Destroy(gameObject, 0.2f);

        }
        
    }


}
