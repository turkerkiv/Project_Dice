using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossColliderScr : MonoBehaviour
{
    GameObject parent;
    Animator anim;
    void Start()
    {
        parent = transform.parent.gameObject;
        anim = parent.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            anim.speed = 1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.speed = 0;

        }
    }
}
