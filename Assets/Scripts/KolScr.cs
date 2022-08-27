using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KolScr : MonoBehaviour
{
    Vector3 mousePos;
    float scaleX,diff;
    GameObject myParent;
    private void Start()
    {
        myParent = transform.parent.gameObject;
    }
    private void FixedUpdate()
    {
        mousePos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        transform.LookAt(mousePos);
        //mousePos = Input.mousePosition;
        //mousePos.Normalize();
        //float rotationZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        //scaleX = myParent.transform.localScale.x;
        
        //if (scaleX == -1)
        //{
        //    diff = 0;
        //}
        //else
        //{
        //    diff = 90f;
        //}
        //transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ+90f);
    }
    public Vector3 GetMousePoint()
    {
        return mousePos;
    }
}
