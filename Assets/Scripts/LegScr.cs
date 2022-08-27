using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegScr : MonoBehaviour
{
    GameObject myParent;
    Rigidbody rb;
    Animator anim;
    void Start()
    { 
        anim = GetComponent<Animator>();
        myParent = transform.parent.parent.gameObject;
        rb = myParent.GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (Mathf.Abs(rb.velocity.x) > 1f)
        {
            anim.speed = 0.7f;
        }
        else
        { anim.speed = 0;}
    }
}
