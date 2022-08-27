using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallColliderScr : MonoBehaviour
{
    FallScr fallScr;
    void Start()
    {
        fallScr = transform.parent.GetComponent<FallScr>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fallScr.doFall();
            Destroy(gameObject);
        }
    }
}
