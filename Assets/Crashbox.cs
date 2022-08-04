using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Crashbox : MonoBehaviour
{
    public GameObject[] pieces;

    private void Start()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(Random.onUnitSphere * 400);
        }
        StartCoroutine(clean());
    }
    
    IEnumerator clean()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
