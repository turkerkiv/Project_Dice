using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallScr : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float fallSpeed;


    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.GetMask("Plane"))
        {
            rb.velocity = new Vector2(0f,0f);
        }
    }
    public void doFall()
    {
        rb.velocity = new Vector2(0f, fallSpeed);
        gameObject.layer = LayerMask.NameToLayer("Plane");
    }
}
