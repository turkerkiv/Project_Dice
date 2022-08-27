using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereEnemyScr : MonoBehaviour
{
    Rigidbody myParent;
    void Start()
    {
        myParent = transform.parent.GetComponent<Rigidbody>(); ;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myParent.useGravity = true;
            Invoke(nameof(DoDestroy), 7f);
        }
    }
    void DoDestroy()
    {
        Destroy(gameObject);
    }
}
