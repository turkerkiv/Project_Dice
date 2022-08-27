using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScr : MonoBehaviour
{
    [SerializeField] int _damage = 10;
    ParticleSystem ps;
    [SerializeField] GameObject particleEff;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        Invoke(nameof(Destroy), 7f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyBehaviour>().GetHit(_damage);
        }
        Destroy(gameObject);
    }

    void Destroy()
    {
        particleEff.SetActive(true);
        particleEff.transform.parent = null;
        Destroy(gameObject);
    }
}
