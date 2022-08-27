using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScr : MonoBehaviour
{
    [SerializeField] GameObject checkGroundObj;
    [SerializeField] bool onGround;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        onGround = Physics.CheckSphere(checkGroundObj.transform.position, 0.15f, LayerMask.GetMask("Plane"));
    }
}
