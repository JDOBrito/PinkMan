using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    public GameObject myPrefab;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            if (col.transform.position.y > gameObject.transform.position.y)
            {
                gameObject.GetComponent<Animator>().Play("PressBox");
                StartCoroutine(crash());
            }
    }

    IEnumerator crash()
    {
        yield return new WaitForSeconds(1f);
        var boxCrashs = Instantiate(myPrefab,gameObject.transform.parent);
        boxCrashs.transform.position = new Vector3(
            gameObject.transform.position.x+1, 
            gameObject.transform.position.y, 
            gameObject.transform.position.z);
        Destroy(gameObject);
    }
    
 
}
