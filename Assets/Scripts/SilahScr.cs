using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilahScr : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    GameObject kol;
    GameObject child;
    KolScr kolScr;
    [SerializeField] float AtisHizi, bulletSpeed;
    [SerializeField] AudioClip _audioClip;
    bool canShot = true;


    void Start()
    {
        child = transform.GetChild(0).gameObject;
        kol = transform.parent.gameObject;
        kolScr = kol.GetComponent<KolScr>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && canShot)
        {
            canShot = false;
            InvokeRepeating(nameof(Shot), 0f, AtisHizi);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && !canShot)
        {

            Debug.Log("D");
            if (IsInvoking(nameof(Shot)))
            {
                CancelInvoke(nameof(Shot));
                Invoke(nameof(CancelShot), AtisHizi);
            }
            
        }
    }
    void Shot()
    {
        GameObject mermi = Instantiate(bullet, child.transform.position, Quaternion.identity);
        mermi.transform.eulerAngles = transform.eulerAngles;

        AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position);

        Rigidbody mermiRb = mermi.GetComponent<Rigidbody>();
        mermiRb.velocity = transform.up * bulletSpeed;
    }
    void CancelShot()
    {
        canShot = true;
    }
}
