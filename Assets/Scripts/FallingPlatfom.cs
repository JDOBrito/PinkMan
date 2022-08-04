using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TargetJoint2D))]
public class FallingPlatfom : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float m_TimeToFall;
    [SerializeField]
    private Animator m_Animator;
    private TargetJoint2D m_TargetJoint;

  

    void Start()
    {
        m_TargetJoint = GetComponent<TargetJoint2D>();
        m_Animator = GetComponent<Animator>();
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(m_TimeToFall);
        m_Animator.SetTrigger("Off");
        m_TargetJoint.enabled = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Fall());
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
