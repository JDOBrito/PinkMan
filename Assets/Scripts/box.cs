using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    
    public GameObject anim;
    public GameObject myPrefab;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            anim.gameObject.GetComponent<Animator>().Play("PressBox");
            StartCoroutine(crash());
            
        }
    }
    
    IEnumerator crash()
    {
        
        yield return new WaitForSeconds(1f);
        var boxCrashs = Instantiate(myPrefab,anim.transform.parent);
        boxCrashs.transform.position =
            new Vector3(anim.transform.position.x+1, anim.transform.position.y, anim.transform.position.z);
        print(boxCrashs.transform.position);
        Destroy(anim);
    }
    
 
}
